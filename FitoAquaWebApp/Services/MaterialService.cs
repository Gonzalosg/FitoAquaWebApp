using FitoAquaWebApp.DAO;
using FitoAquaWebApp.Models;

namespace FitoAquaWebApp.Services
{
    public interface IMaterialService
    {
        Task<List<Material>> GetAllAsync();
        Task<Material?> GetByIdAsync(int id);
        Task<Material> AddAsync(Material material);
        Task<bool> UpdateAsync(Material material);
        Task<bool> DeleteAsync(int id);
    }

    public class MaterialService : IMaterialService
    {
        private readonly IMaterialDao _materialDao;

        public MaterialService(IMaterialDao materialDao)
        {
            _materialDao = materialDao;
        }

        public Task<List<Material>> GetAllAsync() => _materialDao.GetAllAsync();
        public Task<Material?> GetByIdAsync(int id) => _materialDao.GetByIdAsync(id);
        public Task<Material> AddAsync(Material material) => _materialDao.AddAsync(material);
        public Task<bool> UpdateAsync(Material material) => _materialDao.UpdateAsync(material);
        public Task<bool> DeleteAsync(int id) => _materialDao.DeleteAsync(id);
    }

}
