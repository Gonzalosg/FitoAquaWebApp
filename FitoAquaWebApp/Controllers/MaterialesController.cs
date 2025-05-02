using FitoAquaWebApp.Models;
using FitoAquaWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace FitoAquaWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialesController : ControllerBase
    {
        private readonly IMaterialService _materialService;

        public MaterialesController(IMaterialService materialService)
        {
            _materialService = materialService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Material>>> GetAll() =>
            Ok(await _materialService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<Material>> GetById(int id)
        {
            var material = await _materialService.GetByIdAsync(id);
            return material == null ? NotFound() : Ok(material);
        }

        [HttpPost]
        public async Task<ActionResult<Material>> Create(Material material)
        {
            var created = await _materialService.AddAsync(material);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Material material)
        {
            if (id != material.Id) return BadRequest();
            var result = await _materialService.UpdateAsync(material);
            return result ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _materialService.DeleteAsync(id);
            return result ? NoContent() : NotFound();
        }
    }
}
