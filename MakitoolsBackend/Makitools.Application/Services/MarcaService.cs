using Makitools.Application.DTOs.Requests;
using Makitools.Application.DTOs.Responses;
using Makitools.Application.Interfaces;
using Makitools.Domain.Entities.Almacen;
using Makitools.Infrastructure.Repositories.Interfaces;


namespace Makitools.Application.Services
{
    public class MarcaService : IMarcaService
    {
        private readonly IMarcaRepository _marcaRepository;
        public MarcaService(IMarcaRepository marcaRepository)
        {
            _marcaRepository = marcaRepository;
        }

        public async Task<MarcaResponseDto> ActualizarMarcaAsync(int id, MarcaUpdateRequestDto dto)
        {
            var marcaExistente = await _marcaRepository.ObtenerMarcaPorIdAsync(id);
            if (marcaExistente == null || marcaExistente.Activo == false)
                throw new KeyNotFoundException($"La Marca con id {id} no existe o esta inactiva.");

            marcaExistente.Descripcion = dto.Descripcion;
            await _marcaRepository.ActualizarMarcaAsync(marcaExistente);
            return await BuscarMarcaPorIdAsync(id)
                ?? throw new Exception("Error al recuperar el usuario actualizado");
        }
            

        public async Task<MarcaResponseDto?> BuscarMarcaPorIdAsync(int id)
        {
            var marca = await _marcaRepository.ObtenerMarcaPorIdAsync(id);
            if(marca == null)
            {
                throw new KeyNotFoundException($"La marca con id: {id} no fue encontrada.");
            }
            return new MarcaResponseDto
            {
                IdMarca = marca.IdMarca,
                Descripcion = marca.Descripcion,
                Activo = marca.Activo,
                FechaRegistro = marca.FechaRegistro
            };
        }

        public async Task<MarcaResponseDto> CrearMarcaAsync(MarcaCreateRequestDto dto)
        {
            var nuevaMarca = new Marca
            {
                Descripcion = dto.Descripcion,
                Activo = true,
                FechaRegistro = DateTime.Now
            };

            await _marcaRepository.GuardarMarcaAsync(nuevaMarca);

            var marcaGuardada = await _marcaRepository.ObtenerMarcaPorIdAsync(nuevaMarca.IdMarca);
            return new MarcaResponseDto
            {
                IdMarca = marcaGuardada!.IdMarca,
                Descripcion = marcaGuardada.Descripcion,
                Activo = marcaGuardada.Activo,
                FechaRegistro = marcaGuardada.FechaRegistro
            };
        }

        public async Task<bool> EliminarMarcaAsync(int id)
        {
            var marca = await _marcaRepository.ObtenerMarcaPorIdAsync(id);
            if(marca == null)
            {
                throw new KeyNotFoundException($"La marca con id: {id} no fue encontrado.");
            }
            marca.Activo = false;
            await _marcaRepository.ActualizarMarcaAsync(marca);
            return true;
        }

        public async Task<IEnumerable<MarcaResponseDto>> ListarMarcasAsync()
        {
            var marcas = await _marcaRepository.ObtenerMarcasActivasAsync();
            var marcasDto = marcas.Select(m => new MarcaResponseDto
            {
                IdMarca = m.IdMarca,
                Descripcion = m.Descripcion,
                Activo = m.Activo,
                FechaRegistro = m.FechaRegistro
            });
            return marcasDto;
        }
    }
}
