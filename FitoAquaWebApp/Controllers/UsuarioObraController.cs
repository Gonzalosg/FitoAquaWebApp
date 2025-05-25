using FitoAquaWebApp.DTOs;
using FitoAquaWebApp.Services;
using FitoAquaWebApp.Utils;
using Microsoft.AspNetCore.Mvc;

namespace FitoAquaWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioObraController : ControllerBase
    {
        private readonly IUsuarioObraService _service;

        public UsuarioObraController(IUsuarioObraService service)
        {
            _service = service;
        }

        // POST: api/UsuarioObra
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] UsuarioObraDto input)
        {
            try
            {
                var result = await _service.AddAsync(input);
                return Ok(new ResultService { Data = result, Result = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al asignar usuario a obra: {ex.Message}");
            }
        }

        // GET: api/UsuarioObra
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _service.GetAllAsync();
                return Ok(new ResultService { Data = result, Result = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener asignaciones: {ex.Message}");
            }
        }

        // GET: api/UsuarioObra/usuario/5
        [HttpGet("usuario/{usuarioId}")]
        public async Task<IActionResult> GetObrasByUsuario(int usuarioId)
        {
            try
            {
                var result = await _service.GetObrasByUsuarioIdAsync(usuarioId);
                return Ok(new ResultService { Data = result, Result = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener obras del usuario: {ex.Message}");
            }
        }

        [HttpGet("empleados/obra/{obraId}")]
        public async Task<IActionResult> GetEmpleadosByObraId(int obraId)
        {
            try
            {
                var result = await _service.GetEmpleadosByObraIdAsync(obraId);
                return Ok(new ResultService { Data = result, Result = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

    }
}
