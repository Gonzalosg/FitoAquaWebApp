using AutoMapper;
using FitoAquaWebApp.DAO;
using FitoAquaWebApp.DTOs;
using FitoAquaWebApp.Models;

namespace FitoAquaWebApp.Services
{
    public interface IObraService
    {
        Task<List<ObraDto>> GetAllAsync();
        Task<ObraDto> GetByIdAsync(int id);
        Task <ObraDto>AddOrUpdateAsync(ObraDto input);
        Task DeleteAsync(int id);
    }

    public class ObraService : IObraService
    {
        private readonly IObraDao _obraDao;
        private readonly IMapper _mapper;

        public ObraService(IObraDao obraDao, IMapper mapper)
        {
            _obraDao = obraDao;
            _mapper = mapper;
        }

        #region GetMethods

        public async Task<List<ObraDto>> GetAllAsync()
        {
            try
            {
                var data = await _obraDao.GetAllAsync();
                return _mapper.Map<List<ObraDto>>(data);
            }
            catch (Exception ex)
            {              
                throw new Exception("Error al obtener las obras", ex);
            }
        }
        public async Task<ObraDto> GetByIdAsync(int id)
        {
            try
            {
                ObraDto result = _mapper.Map<ObraDto>(await _obraDao.GetByIdAsync(id));

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener la obra con id {id}", ex);
            }
        }

        #endregion


        #region Post/Put/delete methods
        public async Task<ObraDto> AddOrUpdateAsync(ObraDto input)
        {
            try
            {
                bool isNew = input.Id == 0;
                var entity = _mapper.Map<Obra>(input);

                if (isNew)
                {
                    var created = await _obraDao.AddAsync(entity);
                    entity = created;
                }
                else
                {
                    await _obraDao.UpdateAsync(entity);
                }
     
                var updated = await _obraDao.GetByIdAsync(entity.Id);
                return _mapper.Map<ObraDto>(updated);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al agregar o actualizar la obra", ex);
            }
        }



        public async Task DeleteAsync(int id)
        {
            try
            {
                await _obraDao.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar la obra con id {id}", ex);
            }
        }
    }
    #endregion
}
