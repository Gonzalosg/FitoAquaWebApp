using AutoMapper;
using FitoAquaWebApp.DAO;
using FitoAquaWebApp.DTOs;
using FitoAquaWebApp.Models;

namespace FitoAquaWebApp.Services
{
    public interface IMaterialService
    {
        Task<List<MaterialDto>> GetAllAsync();
        Task<MaterialDto> GetByIdAsync(int id);
        Task<MaterialDto> AddOrUpdateAsync(MaterialDto input);
        Task DeleteAsync(int id);
    }

    public class MaterialService : IMaterialService
    {
        private readonly IMaterialDao _materialDao;
        private readonly IMapper _mapper;

        public MaterialService(IMaterialDao materialDao, IMapper mapper)
        {
            _materialDao = materialDao;
            _mapper = mapper;
        }

        public async Task<List<MaterialDto>> GetAllAsync()
        {
            try
            {
                var data = await _materialDao.GetAllAsync();
                return _mapper.Map<List<MaterialDto>>(data);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los materiales", ex);
            }
        }

        public async Task<MaterialDto> GetByIdAsync(int id)
        {
            try
            {
                var result = await _materialDao.GetByIdAsync(id);
                return _mapper.Map<MaterialDto>(result);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el material con id {id}", ex);
            }
        }

        public async Task<MaterialDto> AddOrUpdateAsync(MaterialDto input)
        {
            try
            {
                bool isNew = input.Id == 0;
                var entity = _mapper.Map<Material>(input);

                if (isNew)
                {
                    var created = await _materialDao.AddAsync(entity);
                    entity = created;
                }
                else
                {
                    await _materialDao.UpdateAsync(entity);
                }

                var updated = await _materialDao.GetByIdAsync(entity.Id);
                return _mapper.Map<MaterialDto>(updated);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar o actualizar el material", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                await _materialDao.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar el material con id {id}", ex);
            }
        }
    }
}