using FitoAquaWebApp.Data;
using FitoAquaWebApp.Exceptions;
using FitoAquaWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FitoAquaWebApp.DAO
{
    public interface IMaterialDao
    {
        Task<List<Material>> GetAllAsync();
        Task<Material?> GetByIdAsync(int id);
        Task<Material> AddAsync(Material material);
        Task UpdateAsync(Material material);
        Task DeleteAsync(int id);
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
            try
            {
                return await _context.Materiales.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new DaoException("Error al obtener listado de Materiales", ex);
            }
        }

        public async Task<Material?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Materiales.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new DaoException($"Error al obtener Material con id = {id}", ex);
            }
        }

        public async Task<Material> AddAsync(Material input)
        {
            try
            {
                _context.Materiales.Add(input);
                await _context.SaveChangesAsync();
                return input;
            }
            catch (Exception ex)
            {
                throw new DaoException("Error al añadir Material", ex);
            }
        }

        public async Task UpdateAsync(Material input)
        {
            try
            {
                _context.Materiales.Update(input);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new DaoException($"Error al actualizar Material con id {input.Id}", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var input = await _context.Materiales.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
                if (input != null)
                {
                    _context.Materiales.Remove(input);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new DaoException($"Error al eliminar Material con id = {id}", ex);
            }
        }
    }
}