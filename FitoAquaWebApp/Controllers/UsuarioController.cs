using AutoMapper;
using FitoAquaWebApp.DTOs;
using FitoAquaWebApp.Services;
using FitoAquaWebApp.Utils;
using Microsoft.AspNetCore.Mvc;

namespace FitoAquaWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;

        public UsuariosController(IUsuarioService usuarioService, IMapper mapper)
        {
            _usuarioService = usuarioService;
            _mapper = mapper;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var result = await _usuarioService.GetAllAsync();
                return Ok(new ResultService() { Data = result, Result = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var result = await _usuarioService.GetByIdAsync(id);
                return Ok(new ResultService() { Data = result, Result = true    });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPost("save")]
        public async Task<IActionResult> AddOrUpdateAsync([FromBody] UsuarioDto input)
        {
            try
            {
                var result = await _usuarioService.AddOrUpdateAsync(input);
                return Ok(new ResultService() { Data = result, Result = true });
            }
            catch (Exception ex)
            {
                var deepest = ex;
                while (deepest.InnerException != null)
                    deepest = deepest.InnerException;

                return StatusCode(500, $"Error interno del servidor: {deepest.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await _usuarioService.DeleteAsync(id);
                return Ok(new ResultService() { Data = id, Result = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto input)
        {
            try
            {
                var result = await _usuarioService.LoginAsync(input);
                if (result == null)
                    return Unauthorized(new ResultService { Result = false, Message = "Credenciales inválidas" });

                return Ok(new ResultService { Result = true, Data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

    }
}
