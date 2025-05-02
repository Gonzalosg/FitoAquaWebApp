using FitoAquaWebApp.Models;
using FitoAquaWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace FitoAquaWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ObrasController : ControllerBase
    {
        private readonly IObraService _obraService;

        public ObrasController(IObraService obraService)
        {
            _obraService = obraService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Obra>>> GetAll() =>
            Ok(await _obraService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<Obra>> GetById(int id)
        {
            var obra = await _obraService.GetByIdAsync(id);
            return obra == null ? NotFound() : Ok(obra);
        }

        [HttpPost]
        public async Task<ActionResult<Obra>> Create(Obra obra)
        {
            var created = await _obraService.AddAsync(obra);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Obra obra)
        {
            if (id != obra.Id) return BadRequest();
            var result = await _obraService.UpdateAsync(obra);
            return result ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _obraService.DeleteAsync(id);
            return result ? NoContent() : NotFound();
        }
    }
}
