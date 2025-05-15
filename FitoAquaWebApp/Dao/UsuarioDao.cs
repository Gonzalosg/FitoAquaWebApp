using FitoAquaWebApp.Data;
using FitoAquaWebApp.Exceptions;
using FitoAquaWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FitoAquaWebApp.DAO
{
    public interface IUsuarioDao
    {
        Task<List<Usuario>> GetAllAsync();
        Task<Usuario?> GetByIdAsync(int id);
        Task<Usuario> AddAsync(Usuario usuario);
        Task UpdateAsync(Usuario usuario);
        Task DeleteAsync(int id);

        Task<Usuario?> GetByEmailAsync(string email);
    }

    public class UsuarioDao : IUsuarioDao
    {
        private readonly AppDbContext _context;



        public UsuarioDao(AppDbContext context)
        {
            _context = context;
        }


        public async Task<Usuario?> GetByEmailAsync(string email)
        {
            try
            {
                return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            }
            catch (Exception ex)
            {
                throw new DaoException($"Error al buscar el usuario por email: {email}", ex);
            }
        }


        public async Task<List<Usuario>> GetAllAsync()
        {
            try
            {
                return await _context.Usuarios.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new DaoException("Error al obtener listado de Usuarios", ex);
            }
        }

        public async Task<Usuario?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Usuarios.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new DaoException($"Error al obtener Usuario con id = {id}", ex);
            }
        }

        public async Task<Usuario> AddAsync(Usuario input)
        {
            try
            {
                _context.Usuarios.Add(input);
                await _context.SaveChangesAsync();
                return input;
            }
            catch (Exception ex)
            {
                throw new DaoException("Error al añadir Usuario", ex);
            }
        }

        public async Task UpdateAsync(Usuario input)
        {
            try
            {
                _context.Usuarios.Update(input);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new DaoException($"Error al actualizar Usuario con id {input.Id}", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var input = await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
                if (input != null)
                {
                    _context.Usuarios.Remove(input);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new DaoException($"Error al eliminar Usuario con id = {id}", ex);
            }
        }
    }
}
