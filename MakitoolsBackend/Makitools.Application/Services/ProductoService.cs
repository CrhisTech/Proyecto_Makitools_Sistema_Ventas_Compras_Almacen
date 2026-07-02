

using Makitools.Application.DTOs.Requests;
using Makitools.Application.DTOs.Responses;
using Makitools.Application.Interfaces;
using Makitools.Domain.Entities.Almacen;
using Makitools.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Makitools.Application.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _productoRepository;
        private readonly IWebHostEnvironment _env;

        public ProductoService (IProductoRepository productoRepository, IWebHostEnvironment env)
        {
            _productoRepository = productoRepository;
            _env = env;
        }

        public async Task<ProductoResponseDto> ActualizarProductoAsync(int id, ProductoUpdateRequestDto dto)
        {
            var productoExistente = await _productoRepository.ObtenerProductoPorIdAsync(id);
            if(productoExistente == null || productoExistente.Activo == false)
            {
                throw new KeyNotFoundException($"El producto con ID {id} no existe o esta inactivo.");
            }

            var lstProductos = await _productoRepository.ObtenerProductosActivosAsync();
            bool skuDuplicado = lstProductos.Any(p =>
            p.Sku != null &&
            p.Sku.Equals(dto.Sku, StringComparison.OrdinalIgnoreCase) &&
            p.IdProducto != id);

            if (skuDuplicado) throw new ArgumentException($"El codigo SKU '{dto.Sku}' ya esta siendo usado por otro producto");

            if(dto.Imagen !=null && dto.Imagen.Length > 0)
            {
                string carpetaDestino = Path.Combine(_env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), "images", "productos");
                if (!Directory.Exists(carpetaDestino)) Directory.CreateDirectory(carpetaDestino);

                string extension = Path.GetExtension(dto.Imagen.FileName);
                string nombreArchivo = $"{dto.Sku.ToLower()}_{Guid.NewGuid().ToString().Substring(0, 5)}{extension}";
                string rutaFisicaCompleta = Path.Combine(carpetaDestino, nombreArchivo);
                using (var stream = new FileStream(rutaFisicaCompleta, FileMode.Create))
                {
                    await dto.Imagen.CopyToAsync(stream);
                }
                productoExistente.RutaImagen = $"/images/productos/{nombreArchivo}";
            }
            productoExistente.Sku = dto.Sku;
            productoExistente.Nombre = dto.Nombre;
            productoExistente.Descripcion = dto.Descripcion;
            productoExistente.IdCategoria = dto.IdCategoria;
            productoExistente.IdMarca = dto.IdMarca;
            productoExistente.PrecioVenta = dto.PrecioVenta;

            await _productoRepository.ActualizarProductoAsync(productoExistente);

            var productoActualizado = await _productoRepository.ObtenerProductoPorIdAsync(id);

            return new ProductoResponseDto
            {
                IdProducto = productoActualizado!.IdProducto,
                SKU = productoActualizado.Sku,
                Nombre = productoActualizado.Nombre,
                Descripcion = productoActualizado.Descripcion,
                PrecioVenta = productoActualizado.PrecioVenta,
                Stock = (int)productoActualizado.Stock,
                RutaImagen = productoActualizado.RutaImagen,
                Categoria = productoActualizado.IdCategoriaNavigation?.Descripcion,
                Marca = productoActualizado.IdMarcaNavigation?.Descripcion,
                Activo = productoActualizado.Activo
            };
        }

        public async Task<ProductoResponseDto?> BuscarProductoPorIdAsync(int id)
        {
            var producto = await _productoRepository.ObtenerProductoPorIdAsync(id);
            if (producto == null)
            {
                throw new KeyNotFoundException($"El producto con id: {id} no fue encontrado.");
            }
            return new ProductoResponseDto
            {
                IdProducto = producto.IdProducto,
                SKU = producto.Sku,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Categoria = producto.IdCategoriaNavigation.Descripcion,
                Marca = producto.IdMarcaNavigation.Descripcion,
                PrecioVenta = producto.PrecioVenta,
                RutaImagen = producto.RutaImagen,
                Activo = producto.Activo,
            };
        }

        public async Task<ProductoResponseDto> CrearProductoAsync(ProductoCreateRequestDto dto)
        {
            var productosExistentes = await _productoRepository.ObtenerProductosActivosAsync();
            bool skuDuplicado = productosExistentes.Any(p => p.Sku.Equals(dto.Sku, StringComparison.OrdinalIgnoreCase));
            if (skuDuplicado) throw new ArgumentException($"El codigo SKU '{dto.Sku}' ya esta registrado.");

            string rutaRelativaImagen = "";
            if(dto.Imagen != null && dto.Imagen.Length > 0)
            {
                string carpetaDestino = Path.Combine(_env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), "images", "productos");
                if (!Directory.Exists(carpetaDestino)) Directory.CreateDirectory(carpetaDestino);
                string extension = Path.GetExtension(dto.Imagen.FileName);
                string nombreArchivo = $"{dto.Sku.ToLower()}_{Guid.NewGuid().ToString().Substring(0, 5)}{extension}";
                string rutaFisicaCompleta = Path.Combine(carpetaDestino, nombreArchivo);

                using (var  stream = new FileStream(rutaFisicaCompleta, FileMode.Create))
                {
                    await dto.Imagen.CopyToAsync(stream);
                }
                rutaRelativaImagen = $"/images/productos/{nombreArchivo}";
            }


            var nuevoProducto = new Producto
            {
                Sku = dto.Sku,
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                IdCategoria = dto.IdCategoria,
                IdMarca = dto.IdMarca,
                PrecioVenta = dto.PrecioVenta,
                Stock = 0,
                CostoPromedio = 0,
                RutaImagen = rutaRelativaImagen,
                Activo = true,
                FechaRegistro = DateTime.Now,
            };
            await _productoRepository.GuardarProductoAsync(nuevoProducto);

            var productoGuardado = await _productoRepository.ObtenerProductoPorIdAsync(nuevoProducto.IdProducto);

            return new ProductoResponseDto
            {
                IdProducto = productoGuardado!.IdProducto,
                SKU = productoGuardado.Sku,
                Nombre = productoGuardado.Nombre,
                Descripcion = productoGuardado.Descripcion,
                Categoria = productoGuardado.IdCategoriaNavigation.Descripcion,
                Marca = productoGuardado.IdMarcaNavigation.Descripcion,
                PrecioVenta = productoGuardado.PrecioVenta,
                RutaImagen = productoGuardado.RutaImagen,
                Activo = productoGuardado.Activo,
            };
        }

        public async Task<bool> EliminarProductoAsync(int id)
        {
            var producto = await _productoRepository.ObtenerProductoPorIdAsync(id);
            if(producto == null)
            {
                throw new KeyNotFoundException($"El producto con ID {id} no existe.");
            }
            producto.Activo = false;
            await _productoRepository.ActualizarProductoAsync(producto);
            return true;
        }

        public async Task<IEnumerable<ProductoResponseDto>> ListarProductosAsync()
        {
            var productos = await _productoRepository.ObtenerProductosActivosAsync();
            var productosDto = productos.Select(p => new ProductoResponseDto
            {
                IdProducto = p.IdProducto,
                SKU = p.Sku,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                Categoria = p.IdCategoriaNavigation.Descripcion,
                Marca = p.IdMarcaNavigation.Descripcion,
                PrecioVenta = p.PrecioVenta,
                RutaImagen = p.RutaImagen,
                Activo = p.Activo,
            });
            return productosDto;
        }
    }
}
