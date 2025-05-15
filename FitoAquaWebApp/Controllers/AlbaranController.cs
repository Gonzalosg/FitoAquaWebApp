// Controllers/AlbaranController.cs
using FitoAquaWebApp.Dto;
using FitoAquaWebApp.Services;
using FitoAquaWebApp.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitoAquaWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlbaranController : ControllerBase
    {
        private readonly IAlbaranService _albaranService;

        public AlbaranController(IAlbaranService albaranService)
        {
            _albaranService = albaranService;
        }

        // GET: api/Albaran
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _albaranService.GetAllAsync();
                return Ok(new ResultService { Data = result, Result = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // GET: api/Albaran/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _albaranService.GetByIdAsync(id);
                if (result == null)
                    return NotFound(new ResultService { Result = false, Message = "Albarán no encontrado" });

                return Ok(new ResultService { Data = result, Result = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // POST: api/Albaran
        [HttpPost]
        public async Task<IActionResult> AddOrUpdate([FromBody] AlbaranDto input)
        {
            try
            {
                var result = await _albaranService.AddOrUpdateAsync(input);
                return Ok(new ResultService { Data = result, Result = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // DELETE: api/Albaran/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _albaranService.DeleteAsync(id);
                if (!result)
                    return NotFound(new ResultService { Result = false, Message = "Albarán no encontrado" });

                return Ok(new ResultService { Data = $"Albarán {id} eliminado", Result = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}
