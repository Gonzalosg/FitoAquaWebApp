// Services/ParteAveriaService.cs
using AutoMapper;
using FitoAquaWebApp.DAO;
using FitoAquaWebApp.DTOs;
using FitoAquaWebApp.Models;

namespace FitoAquaWebApp.Services
{
    public interface IParteAveriaService
    {
        Task<List<ParteAveriaDto>> GetAllAsync();
        Task<ParteAveriaDto> GetByIdAsync(int id);
        Task<ParteAveriaDto> AddOrUpdateAsync(ParteAveriaDto dto);
        Task DeleteAsync(int id);

        Task<bool> UpdateEstadoAsync(int id, string nuevoEstado);

    }

    public class ParteAveriaService : IParteAveriaService
    {
        private readonly IParteAveriaDao _dao;
        private readonly IMapper _mapper;

        public ParteAveriaService(IParteAveriaDao dao, IMapper mapper)
        {
            _dao = dao;
            _mapper = mapper;
        }

        public async Task<List<ParteAveriaDto>> GetAllAsync()
        {
            var partes = await _dao.GetAllAsync();
            return _mapper.Map<List<ParteAveriaDto>>(partes);
        }

        public async Task<ParteAveriaDto> GetByIdAsync(int id)
        {
            var parte = await _dao.GetByIdAsync(id);
            return _mapper.Map<ParteAveriaDto>(parte);
        }

        public async Task<ParteAveriaDto> AddOrUpdateAsync(ParteAveriaDto dto)
        {
            if (dto.Id == 0)
            {
                dto.Estado = "Abierta"; // Forzar estado por defecto
                var entity = _mapper.Map<ParteAveria>(dto);
                var created = await _dao.AddAsync(entity);
                return _mapper.Map<ParteAveriaDto>(created);
            }
            else
            {
                var entity = _mapper.Map<ParteAveria>(dto);
                var updated = await _dao.UpdateAsync(entity);
                if (!updated)
                    throw new Exception($"No se encontró ParteAveria con ID {dto.Id} para actualizar");

                var refreshed = await _dao.GetByIdAsync(dto.Id);
                return _mapper.Map<ParteAveriaDto>(refreshed);
            }
        }


        public async Task DeleteAsync(int id)
        {
            var success = await _dao.DeleteAsync(id);
            if (!success) throw new Exception($"No se pudo eliminar ParteAveria con ID {id}");
        }

        public async Task<bool> UpdateEstadoAsync(int id, string nuevoEstado)
        {
            if (!Enum.TryParse<EstadoAveria>(nuevoEstado, true, out var estado))
                throw new Exception($"Estado '{nuevoEstado}' no es válido");

            return await _dao.UpdateEstadoAsync(id, estado);
        }

    }
}
