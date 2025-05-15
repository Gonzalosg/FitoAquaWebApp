// Services/AlbaranService.cs
using AutoMapper;
using FitoAquaWebApp.DAO;
using FitoAquaWebApp.Dto;
using FitoAquaWebApp.Models;

namespace FitoAquaWebApp.Services
{
    public interface IAlbaranService
    {
        Task<List<AlbaranDto>> GetAllAsync();
        Task<AlbaranDto?> GetByIdAsync(int id);
        Task<AlbaranDto> AddOrUpdateAsync(AlbaranDto input);
        Task<bool> DeleteAsync(int id);
    }

    public class AlbaranService : IAlbaranService
    {
        private readonly IAlbaranDao _albaranDao;
        private readonly IMapper _mapper;

        public AlbaranService(IAlbaranDao albaranDao, IMapper mapper)
        {
            _albaranDao = albaranDao;
            _mapper = mapper;
        }

        // Obtener todos los albaranes con sus detalles
        public async Task<List<AlbaranDto>> GetAllAsync()
        {
            var albaranes = await _albaranDao.GetAllAsync();
            return _mapper.Map<List<AlbaranDto>>(albaranes);
        }

        // Obtener un albarán por ID con sus detalles
        public async Task<AlbaranDto?> GetByIdAsync(int id)
        {
            var albaran = await _albaranDao.GetByIdAsync(id);
            return albaran != null ? _mapper.Map<AlbaranDto>(albaran) : null;
        }

        // Crear o actualizar un albarán
        public async Task<AlbaranDto> AddOrUpdateAsync(AlbaranDto input)
        {
            // Mapear DTO a la entidad Albaran
            var albaran = _mapper.Map<Albaran>(input);

            if (input.Id == 0)
            {
                // Crear nuevo albarán
                var creado = await _albaranDao.AddAsync(albaran);
                return _mapper.Map<AlbaranDto>(creado);
            }
            else
            {
                // Actualizar albarán existente
                var actualizado = await _albaranDao.UpdateAsync(albaran);
                if (!actualizado)
                    throw new Exception($"No se pudo actualizar el albarán con ID {input.Id}");

                var actualizadoFinal = await _albaranDao.GetByIdAsync(input.Id);
                return _mapper.Map<AlbaranDto>(actualizadoFinal);
            }
        }

        // Eliminar un albarán y sus detalles
        public async Task<bool> DeleteAsync(int id)
        {
            return await _albaranDao.DeleteAsync(id);
        }
    }
}
