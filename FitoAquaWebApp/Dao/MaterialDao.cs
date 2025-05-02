using FitoAquaWebApp.Data;
using FitoAquaWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FitoAquaWebApp.DAO
{
    public interface IMaterialDao
    {
        Task<List<Material>> GetAllAsync();
        Task<Material?> GetByIdAsync(int id);
        Task<Material> AddAsync(Material material);
        Task<bool> UpdateAsync(Material material);
        Task<bool> DeleteAsync(int id);
    }

    public class MaterialDao : IMaterialDao
    {
        private readonly AppDbContext _context;

        public MaterialDao(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Material>> GetAllAsync()
        {
            return await _context.Materiales.ToListAsync();
        }

        public async Task<Material?> GetByIdAsync(int id)
        {
            return await _context.Materiales.FindAsync(id);
        }

        public async Task<Material> AddAsync(Material material)
        {
            _context.Materiales.Add(material);
            await _context.SaveChangesAsync();
            return material;
        }

        public async Task<bool> UpdateAsync(Material material)
        {
            if (!_context.Materiales.Any(m => m.Id == material.Id))
                return false;

            _context.Materiales.Update(material);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var material = await _context.Materiales.FindAsync(id);
            if (material == null) return false;

            _context.Materiales.Remove(material);
            await _context.SaveChangesAsync();
            return true;
        }
        }
    }
