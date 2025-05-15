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
        Task<ObraDto> AddOrUpdateAsync(ObraDto input);
        Task DeleteAsync(int id);
    }

    public class ObraService : IObraService
    {
        private readonly IObraDao _obraDao;
        private readonly IUsuarioDao _usuarioDao; // Aseguramos que el cliente existe
        private readonly IMapper _mapper;

        public ObraService(IObraDao obraDao, IUsuarioDao usuarioDao, IMapper mapper)
        {
            _obraDao = obraDao;
            _usuarioDao = usuarioDao;
            _mapper = mapper;
        }

        public async Task<List<ObraDto>> GetAllAsync()
        {
            var data = await _obraDao.GetAllAsync();
            return _mapper.Map<List<ObraDto>>(data);
        }

        public async Task<ObraDto> GetByIdAsync(int id)
        {
            var obra = await _obraDao.GetByIdAsync(id);
            return _mapper.Map<ObraDto>(obra);
        }

        public async Task<ObraDto> AddOrUpdateAsync(ObraDto input)
        {
            // Verificamos que el cliente existe
            var cliente = await _usuarioDao.GetByIdAsync(input.ClienteId);
            if (cliente == null)
                throw new Exception("El cliente especificado no existe.");

            var entity = _mapper.Map<Obra>(input);

            if (input.Id == 0) // Nuevo
            {
                var created = await _obraDao.AddAsync(entity);
                return _mapper.Map<ObraDto>(created);
            }
            else // Actualizar
            {
                await _obraDao.UpdateAsync(entity);
                var updated = await _obraDao.GetByIdAsync(entity.Id);
                return _mapper.Map<ObraDto>(updated);
            }
        }

        public async Task DeleteAsync(int id)
        {
            await _obraDao.DeleteAsync(id);
        }
    }
}
