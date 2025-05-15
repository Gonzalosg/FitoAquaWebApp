using AutoMapper;
using FitoAquaWebApp.DTOs;
using FitoAquaWebApp.Services;
using FitoAquaWebApp.Utils;
using Microsoft.AspNetCore.Mvc;

namespace FitoAquaWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialesController : ControllerBase
    {
        private readonly IMaterialService _materialService;
        private readonly IMapper _mapper;

        public MaterialesController(IMaterialService materialService, IMapper mapper)
        {
            _materialService = materialService;
            _mapper = mapper;
        }

        [HttpGet("Ausencia/all")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var result = await _materialService.GetAllAsync();
                return Ok(new ResultService { Data = result, Result = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("Material/{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _materialService.GetByIdAsync(id);
            return Ok(new ResultService { Data = result, Result = true });
        }

        [HttpPost("Material")]
        public async Task<IActionResult> AddOrUpdateAsync([FromBody] MaterialDto input)
        {
            try
            {
                var result = await _materialService.AddOrUpdateAsync(input);
                return Ok(new ResultService { Data = result, Result = true });
            }
            catch (Exception ex)
            {
                var deepest = ex;
                while (deepest.InnerException != null)
                    deepest = deepest.InnerException;
                return StatusCode(500, $"Error interno del servidor: {deepest.Message}");
            }
        }

        [HttpDelete("DeleteMaterial/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await _materialService.DeleteAsync(id);
                return Ok(new ResultService { Data = id, Result = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}