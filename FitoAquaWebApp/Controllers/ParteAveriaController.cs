using FitoAquaWebApp.DTOs;
using FitoAquaWebApp.Services;
using FitoAquaWebApp.Utils;
using Microsoft.AspNetCore.Mvc;

namespace FitoAquaWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParteAveriaController : ControllerBase
    {
        private readonly IParteAveriaService _parteAveriaService;

        public ParteAveriaController(IParteAveriaService parteAveriaService)
        {
            _parteAveriaService = parteAveriaService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var result = await _parteAveriaService.GetAllAsync();
                return Ok(new ResultService { Data = result, Result = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener partes de avería: {ex.Message}");
            }
        }

        [HttpPost("save")]
        public async Task<IActionResult> SaveAsync([FromBody] ParteAveriaDto input)
        {
            try
            {
                var result = await _parteAveriaService.AddOrUpdateAsync(input);
                return Ok(new ResultService { Data = result, Result = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al guardar el parte de avería: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await _parteAveriaService.DeleteAsync(id);
                return Ok(new ResultService { Data = id, Result = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar el parte de avería: {ex.Message}");
            }
        }

        [HttpPut("cambiarEstado")]
        public async Task<IActionResult> CambiarEstado(int id, string estado)
        {
            try
            {
                var result = await _parteAveriaService.UpdateEstadoAsync(id, estado);
                if (!result) return NotFound(new ResultService { Result = false, Message = $"No se encontró ParteAveria con ID {id}" });
                return Ok(new ResultService { Data = id, Result = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al cambiar estado: {ex.Message}");
            }
        }
    }
}
