using FitoAquaWebApp.Data;
using FitoAquaWebApp.Exceptions;
using FitoAquaWebApp.Models;
using Microsoft.EntityFrameworkCore;


namespace FitoAquaWebApp.DAO
{
    public interface IObraDao
    {
        Task<List<Obra>> GetAllAsync();
        Task<Obra?> GetByIdAsync(int id);
        Task <Obra>AddAsync(Obra obra);
        Task UpdateAsync(Obra obra);
        Task DeleteAsync(int id);
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
            try
            {
                return await _context.Obras.AsNoTracking().Include(o => o.Cliente).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new DaoException("Error al obtener listado de Obras", ex);

            }
        }

        public async Task<Obra?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Obras.Include(o => o.Cliente).FirstOrDefaultAsync(o => o.Id == id);
            }
            catch (Exception ex)
            {
                throw new DaoException($"Error al obtener Obra con id = { id }", ex);

            }
            
        }


        public async Task<Obra> AddAsync(Obra input)
        {
            try
            {
                _context.Obras.Add(input);
                await _context.SaveChangesAsync();
                return input;
            }
            catch (Exception ex)
            {
                throw new DaoException("Error al añadir Obra", ex);
            }
        }


        public async Task UpdateAsync(Obra input)
        {

            try
            {              
                _context.Obras.Update(input);
                await _context.SaveChangesAsync();              
            }
            catch (Exception ex)
            {
                throw new DaoException($"Error al actualizar Obra con id{input.Id}", ex);

            }
          
        }

      

        public async Task DeleteAsync(int id)
        {
            try
            {
                var input = await _context.Obras.AsNoTracking().FirstOrDefaultAsync(o => o.Id == id);

                if(input != null)
                {
                    _context.Obras.Remove(input);
                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                throw new DaoException($"Error al eliminar Obra con id = {id}", ex);

            }
           
        }
    }
}
