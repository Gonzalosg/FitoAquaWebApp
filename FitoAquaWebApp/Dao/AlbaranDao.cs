// DAO/AlbaranDao.cs
using FitoAquaWebApp.Data;
using FitoAquaWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FitoAquaWebApp.DAO
{
    public interface IAlbaranDao
    {
        Task<List<Albaran>> GetAllAsync();
        Task<Albaran?> GetByIdAsync(int id);
        Task<Albaran> AddAsync(Albaran albaran);
        Task<bool> UpdateAsync(Albaran albaran);
        Task<bool> DeleteAsync(int id);
    }

    public class AlbaranDao : IAlbaranDao
    {
        private readonly AppDbContext _context;

        public AlbaranDao(AppDbContext context)
        {
            _context = context;
        }

        // Obtener todos los albaranes con sus detalles
        public async Task<List<Albaran>> GetAllAsync()
        {
            try
            {
                return await _context.Albaranes
                    .Include(a => a.Obra)
                    .Include(a => a.Empleado)
                    .Include(a => a.Detalles)
                        .ThenInclude(d => d.Material)
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los albaranes", ex);
            }
        }

        // Obtener un albarán por ID con sus detalles
        public async Task<Albaran?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Albaranes
                    .Include(a => a.Obra)
                    .Include(a => a.Empleado)
                    .Include(a => a.Detalles)
                        .ThenInclude(d => d.Material)
                    .FirstOrDefaultAsync(a => a.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el albarán con ID {id}", ex);
            }
        }

        // Crear un nuevo albarán
        public async Task<Albaran> AddAsync(Albaran albaran)
        {
            try
            {
                _context.Albaranes.Add(albaran);
                await _context.SaveChangesAsync();
                return albaran;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar el albarán", ex);
            }
        }

        // Actualizar un albarán y sus detalles
        public async Task<bool> UpdateAsync(Albaran albaran)
        {
            try
            {
                var existing = await _context.Albaranes
                    .Include(a => a.Detalles)
                    .FirstOrDefaultAsync(a => a.Id == albaran.Id);

                if (existing == null)
                    return false;

                // Actualizar detalles del albarán
                existing.FechaCreacion = albaran.FechaCreacion;
                existing.MesReferencia = albaran.MesReferencia;
                existing.ObraId = albaran.ObraId;
                existing.EmpleadoId = albaran.EmpleadoId;

                // Actualizar detalles (se elimina y se vuelve a agregar)
                existing.Detalles.Clear();
                foreach (var detalle in albaran.Detalles)
                {
                    existing.Detalles.Add(detalle);
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar el albarán con ID {albaran.Id}", ex);
            }
        }

        // Eliminar un albarán y sus detalles
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var albaran = await _context.Albaranes
                    .Include(a => a.Detalles)
                    .FirstOrDefaultAsync(a => a.Id == id);

                if (albaran == null)
                    return false;

                _context.Albaranes.Remove(albaran);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar el albarán con ID {id}", ex);
            }
        }
    }
}
