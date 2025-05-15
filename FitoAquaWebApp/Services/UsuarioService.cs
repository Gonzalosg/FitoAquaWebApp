using AutoMapper;
using FitoAquaWebApp.DAO;
using FitoAquaWebApp.DTOs;
using FitoAquaWebApp.Models;
using FitoAquaWebApp.Utils;
using Microsoft.AspNetCore.Identity;

namespace FitoAquaWebApp.Services
{
    public interface IUsuarioService
    {
        Task<List<UsuarioDto>> GetAllAsync();
        Task<UsuarioDto> GetByIdAsync(int id);
        Task<UsuarioDto> AddOrUpdateAsync(UsuarioDto input);
        Task DeleteAsync(int id);
        Task<LoginResponseDto?> LoginAsync(LoginRequestDto input);
    }

    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioDao _usuarioDao;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<Usuario> _passwordHasher;
        private readonly JwtService _jwtService;


        public UsuarioService(IUsuarioDao usuarioDao, IMapper mapper, IPasswordHasher<Usuario> passwordHasher, JwtService jwtService)
        {
            _usuarioDao = usuarioDao;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }

        #region Get Methods

        public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto input)
        {
            try
            {
                var usuario = await _usuarioDao.GetByEmailAsync(input.Email);
                if (usuario == null) return null;

                var resultado = _passwordHasher.VerifyHashedPassword(usuario, usuario.ContraseniaHash, input.Contrasenia);

                if (resultado == PasswordVerificationResult.Success)
                {
                    var dto = _mapper.Map<UsuarioDto>(usuario);
                    var token = _jwtService.GenerateToken(usuario);
                    return new LoginResponseDto
                    {
                        Usuario = dto,
                        Token = token
                    };
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar iniciar sesión", ex);
            }
        }

        public async Task<List<UsuarioDto>> GetAllAsync()
        {
            try
            {
                var data = await _usuarioDao.GetAllAsync();
                return _mapper.Map<List<UsuarioDto>>(data);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los usuarios", ex);
            }
        }

        public async Task<UsuarioDto> GetByIdAsync(int id)
        {
            try
            {
                var usuario = await _usuarioDao.GetByIdAsync(id);
                return _mapper.Map<UsuarioDto>(usuario);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el usuario con id {id}", ex);
            }
        }

        #endregion

        #region Post/Put/Delete

        public async Task<UsuarioDto> AddOrUpdateAsync(UsuarioDto input)
        {
            try
            {
                bool isNew = input.Id == 0;
                var entity = _mapper.Map<Usuario>(input);

                if (!string.IsNullOrWhiteSpace(input.Password))
                {
                    entity.ContraseniaHash = _passwordHasher.HashPassword(entity, input.Password);
                }

                if (isNew)
                {
                    var created = await _usuarioDao.AddAsync(entity);
                    entity = created;
                }
                else
                {
                    await _usuarioDao.UpdateAsync(entity);
                }

                var updated = await _usuarioDao.GetByIdAsync(entity.Id);
                return _mapper.Map<UsuarioDto>(updated);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar o actualizar el usuario", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                await _usuarioDao.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar el usuario con id {id}", ex);
            }
        }

        #endregion
    }
}
