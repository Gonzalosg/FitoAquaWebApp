using FitoAquaWebApp.Data;
using FitoAquaWebApp.Exceptions;
using FitoAquaWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FitoAquaWebApp.DAO
{
    public interface IUsuarioObraDao
    {
        Task<UsuarioObra> AddAsync(UsuarioObra usuarioObra);
        Task<List<UsuarioObra>> GetAllAsync();
        Task<List<Obra>> GetObrasByUsuarioIdAsync(int usuarioId);

        Task<List<Usuario>> GetEmpleadosByObraIdAsync(int obraId);
    }

    public class UsuarioObraDao : IUsuarioObraDao
    {
        private readonly AppDbContext _context;

        public UsuarioObraDao(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UsuarioObra> AddAsync(UsuarioObra usuarioObra)
        {
            try
            {
                _context.UsuarioObras.Add(usuarioObra);
                await _context.SaveChangesAsync();
                return usuarioObra;
            }
            catch (Exception ex)
            {
                throw new DaoException("Error al asignar usuario a obra", ex);
            }
        }

        public async Task<List<Usuario>> GetEmpleadosByObraIdAsync(int obraId)
        {
            try
            {
               return await _context.UsuarioObras
              .Where(uo => uo.ObraId == obraId && uo.Usuario.Rol == RolUsuario.Empleado)
              .Select(uo => uo.Usuario)
              .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new DaoException("Error al obtener asignaciones de usuarios a obras", ex);
            }

           
        }

        public async Task<List<UsuarioObra>> GetAllAsync()
        {
            try
            {
                return await _context.UsuarioObras
                    .Include(uo => uo.Usuario)
                    .Include(uo => uo.Obra)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new DaoException("Error al obtener asignaciones de usuarios a obras", ex);
            }
        }

        public async Task<List<Obra>> GetObrasByUsuarioIdAsync(int usuarioId)
        {
            try
            {
                return await _context.UsuarioObras
                    .Where(uo => uo.UsuarioId == usuarioId)
                    .Include(uo => uo.Obra)
                    .Select(uo => uo.Obra)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new DaoException("Error al obtener obras asignadas al usuario", ex);
            }
        }
    }
}
