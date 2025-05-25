
using FitoAquaWebApp.Data;
using FitoAquaWebApp.Exceptions;
using FitoAquaWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FitoAquaWebApp.DAO
{
    public interface IParteAveriaDao
    {
        Task<List<ParteAveria>> GetAllAsync();
        Task<ParteAveria?> GetByIdAsync(int id);
        Task<ParteAveria> AddAsync(ParteAveria parte);
        Task<bool> UpdateAsync(ParteAveria parte);
        Task<bool> DeleteAsync(int id);

        Task<bool> UpdateEstadoAsync(int id, EstadoAveria nuevoEstado);
    }

    public class ParteAveriaDao : IParteAveriaDao
    {
        private readonly AppDbContext _context;

        public ParteAveriaDao(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ParteAveria>> GetAllAsync()
        {
            try
            {
                return await _context.PartesAveria
                    .Include(p => p.Empleado)
                    .Include(p => p.Obra)
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new DaoException("Error al obtener el listado de partes de avería", ex);
            }
        }

        public async Task<ParteAveria?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.PartesAveria
                    .Include(p => p.Empleado)
                    .Include(p => p.Obra)
                    .FirstOrDefaultAsync(p => p.Id == id);
            }
            catch (Exception ex)
            {
                throw new DaoException($"Error al obtener la parte de avería con id {id}", ex);
            }
        }

        public async Task<ParteAveria> AddAsync(ParteAveria parte)
        {
            try
            {
                _context.PartesAveria.Add(parte);
                await _context.SaveChangesAsync();
                return parte;
            }
            catch (Exception ex)
            {
                throw new DaoException("Error al añadir parte de avería", ex);
            }
        }

        public async Task<bool> UpdateAsync(ParteAveria parte)
        {
            try
            {
                if (!_context.PartesAveria.Any(p => p.Id == parte.Id))
                    return false;

                _context.PartesAveria.Update(parte);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new DaoException($"Error al actualizar parte de avería con id {parte.Id}", ex);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var parte = await _context.PartesAveria.FindAsync(id);
                if (parte == null) return false;

                _context.PartesAveria.Remove(parte);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new DaoException($"Error al eliminar parte de avería con id {id}", ex);
            }
        }

        public async Task<bool> UpdateEstadoAsync(int id, EstadoAveria nuevoEstado)
        {
            try
            {
                var parte = await _context.PartesAveria.FindAsync(id);
                if (parte == null) return false;

                parte.Estado = nuevoEstado;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new DaoException($"Error al cambiar estado de parte de avería con id {id}", ex);
            }
        }

    }
}
