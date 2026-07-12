using Makitools.Application.DTOs.Requests;
using Makitools.Application.DTOs.Responses;
using Makitools.Application.Interfaces;
using Makitools.Domain.Entities.Almacen;
using Makitools.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Makitools.Application.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;
        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<CategoriaResponseDto?> BuscarCategoriaPorIdAsync(int id)
        {
            var categoria = await _categoriaRepository.ObtenerCategoriaPorIdAsync(id);
            if (categoria == null)
            {
                throw new KeyNotFoundException($"La Categoria con id: {id} no fue encontrada.");
            }
            return new CategoriaResponseDto
            {
                IdCategoria = categoria.IdCategoria,
                Descripcion = categoria.Descripcion,
                Activo = categoria.Activo,
                FechaRegistro = categoria.FechaRegistro
            };
        }

        public async Task<CategoriaResponseDto> CrearCategoriaAsync(CategoriaCreateRequestDto dto)
        {
            var nuevaCategoria = new Categoria
            {
                Descripcion = dto.Descripcion,
                Activo = true,
                FechaRegistro = DateTime.Now
            };

            await _categoriaRepository.GuardarCategoriaAsync(nuevaCategoria);

            var categoriaGuardada = await _categoriaRepository.ObtenerCategoriaPorIdAsync(nuevaCategoria.IdCategoria);
            return new CategoriaResponseDto
            {
                IdCategoria = categoriaGuardada!.IdCategoria,
                Descripcion = categoriaGuardada.Descripcion,
                Activo = categoriaGuardada.Activo,
                FechaRegistro = categoriaGuardada.FechaRegistro
            };
        }

        public async Task<IEnumerable<CategoriaResponseDto>> ListarCategoriasAsync()
        {
            var categorias = await _categoriaRepository.ObtenerCategoriasActivasAsync();
            var categoriasDto = categorias.Select(m => new CategoriaResponseDto
            {
                IdCategoria = m.IdCategoria,
                Descripcion = m.Descripcion,
                Activo = m.Activo,
                FechaRegistro = m.FechaRegistro
            });
            return categoriasDto;
        }

        public async Task<CategoriaResponseDto> ActualizarCategoriaAsync(int id,CategoriaUpdateRequestDto dto)
        {
            var categoriaExistente = await _categoriaRepository.ObtenerCategoriaPorIdAsync(id);
            if (categoriaExistente == null || categoriaExistente.Activo == false)
                throw new KeyNotFoundException($"La categoria con ID {id} no existe o esta inactiva");


            categoriaExistente.Descripcion = dto.Descripcion;
            await _categoriaRepository.ActualizarCategoriaAsync(categoriaExistente);

            return await BuscarCategoriaPorIdAsync(id)
                ?? throw new Exception("Error al recuperar el usuario actualizado.");
        }

        public async Task<bool> EliminarCategoriaAsync(int id)
        {
            var categoria = await _categoriaRepository.ObtenerCategoriaPorIdAsync(id);
            if (categoria == null)
            {
                throw new KeyNotFoundException($"La categoria con id: {id} no fue encontrado.");
            }
            categoria.Activo = false;
            await _categoriaRepository.ActualizarCategoriaAsync(categoria);
            return true;
        }
    }
}
