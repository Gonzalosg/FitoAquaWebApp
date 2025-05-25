using AutoMapper;
using FitoAquaWebApp.DAO;
using FitoAquaWebApp.DTOs;
using FitoAquaWebApp.Models;

namespace FitoAquaWebApp.Services
{
    public interface IUsuarioObraService
    {
        Task<UsuarioObraDto> AddAsync(UsuarioObraDto input);
        Task<List<UsuarioObraDto>> GetAllAsync();
        Task<List<ObraDto>> GetObrasByUsuarioIdAsync(int usuarioId);

        Task<List<UsuarioDto>> GetEmpleadosByObraIdAsync(int obraId);
    }

    public class UsuarioObraService : IUsuarioObraService
    {
        private readonly IUsuarioObraDao _dao;
        private readonly IMapper _mapper;

        public UsuarioObraService(IUsuarioObraDao dao, IMapper mapper)
        {
            _dao = dao;
            _mapper = mapper;
        }

        public async Task<List<UsuarioDto>> GetEmpleadosByObraIdAsync(int obraId)
        {
            var empleados = await _dao.GetEmpleadosByObraIdAsync(obraId);
            return _mapper.Map<List<UsuarioDto>>(empleados);
        }


        public async Task<UsuarioObraDto> AddAsync(UsuarioObraDto input)
        {
            var entity = _mapper.Map<UsuarioObra>(input);
            var result = await _dao.AddAsync(entity);
            return _mapper.Map<UsuarioObraDto>(result);
        }

        public async Task<List<UsuarioObraDto>> GetAllAsync()
        {
            var list = await _dao.GetAllAsync();
            return _mapper.Map<List<UsuarioObraDto>>(list);
        }

        public async Task<List<ObraDto>> GetObrasByUsuarioIdAsync(int usuarioId)
        {
            var obras = await _dao.GetObrasByUsuarioIdAsync(usuarioId);
            return _mapper.Map<List<ObraDto>>(obras);
        }
    }
}
