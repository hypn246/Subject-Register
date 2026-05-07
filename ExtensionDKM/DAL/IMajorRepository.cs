using ExtensionDKM.Models;

namespace ExtensionDKM.DAL
{
    public interface IMajorRepository
    {
        Task<List<Major>> GetAllAsync();
        Task<Major?> GetByIdAsync(int id);
        Task<Major> CreateAsync(Major major);
        Task<Major> UpdateAsync(Major major);
        Task<bool> DeleteAsync(int id);
    }
}
