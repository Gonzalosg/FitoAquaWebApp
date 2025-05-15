using AutoMapper;
using FitoAquaWebApp.DTOs;
using FitoAquaWebApp.Models;
using FitoAquaWebApp.Services;
using FitoAquaWebApp.Utils;
using Microsoft.AspNetCore.Mvc;

namespace FitoAquaWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ObrasController : ControllerBase
    {
        private readonly IObraService _obraService;
        private readonly IMapper _mapper;

        public ObrasController(IObraService obraService, IMapper mapper)
        {
            _obraService = obraService;
            _mapper = mapper;

        }


        [HttpGet ("Obra/all")]
        public async Task<IActionResult>GetAllAsync()
        {
            try
            {
                var result = await _obraService.GetAllAsync();

                return Ok(new ResultService() { Data = result, Result = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("Obra/{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _obraService.GetByIdAsync(id);
            return Ok(new ResultService() { Data = result, Result = true });
        }



        [HttpPost("Obra")]
        public async Task<IActionResult> AddOrUpdateAsync([FromBody] ObraDto input)
        {
            try
            {
                var result = await _obraService.AddOrUpdateAsync(input);
                return Ok(new ResultService() { Data = result, Result = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }




        [HttpDelete("DeleteObra/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _obraService.DeleteAsync(id);

                var output = new ResultService() { Data = id, Result = true };
                return Ok(output);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
            
        }
    }
}
