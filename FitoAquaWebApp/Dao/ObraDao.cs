using FitoAquaWebApp.Data;
using FitoAquaWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FitoAquaWebApp.DAO
{
    public interface IObraDao
    {
        Task<List<Obra>> GetAllAsync();
        Task<Obra?> GetByIdAsync(int id);
        Task<Obra> AddAsync(Obra obra);
        Task<bool> UpdateAsync(Obra obra);
        Task<bool> DeleteAsync(int id);
    }

    public class ObraDao : IObraDao
    {
        private readonly AppDbContext _context;

        public ObraDao(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Obra>> GetAllAsync()
        {
            return await _context.Obras.Include(o => o.Cliente).ToListAsync();
        }

        public async Task<Obra?> GetByIdAsync(int id)
        {
            return await _context.Obras.Include(o => o.Cliente).FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Obra> AddAsync(Obra obra)
        {
            _context.Obras.Add(obra);
            await _context.SaveChangesAsync();
            return obra;
        }

        public async Task<bool> UpdateAsync(Obra obra)
        {
            if (!_context.Obras.Any(o => o.Id == obra.Id))
                return false;

            _context.Obras.Update(obra);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var obra = await _context.Obras.FindAsync(id);
            if (obra == null) return false;

            _context.Obras.Remove(obra);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
