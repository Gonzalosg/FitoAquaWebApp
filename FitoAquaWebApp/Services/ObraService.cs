using FitoAquaWebApp.DAO;
using FitoAquaWebApp.Models;

namespace FitoAquaWebApp.Services
{
    public interface IObraService
    {
        Task<List<Obra>> GetAllAsync();
        Task<Obra?> GetByIdAsync(int id);
        Task<Obra> AddAsync(Obra obra);
        Task<bool> UpdateAsync(Obra obra);
        Task<bool> DeleteAsync(int id);
    }

    public class ObraService : IObraService
    {
        private readonly IObraDao _obraDao;

        public ObraService(IObraDao obraDao)
        {
            _obraDao = obraDao;
        }

        public Task<List<Obra>> GetAllAsync() => _obraDao.GetAllAsync();
        public Task<Obra?> GetByIdAsync(int id) => _obraDao.GetByIdAsync(id);
        public Task<Obra> AddAsync(Obra obra) => _obraDao.AddAsync(obra);
        public Task<bool> UpdateAsync(Obra obra) => _obraDao.UpdateAsync(obra);
        public Task<bool> DeleteAsync(int id) => _obraDao.DeleteAsync(id);
    }
}
 