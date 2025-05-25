using FitoAquaWebApp.DTOs;
using FitoAquaWebApp.Services;
using FitoAquaWebApp.Utils;
using Microsoft.AspNetCore.Mvc;

namespace FitoAquaWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public LoginController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
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
