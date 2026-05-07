using ExtensionDKM.Models;

namespace ExtensionDKM.BUS
{
    public interface IMajorService
    {
        Task<List<Major>> GetAllMajorsAsync();
        Task<Major?> GetMajorByIdAsync(int id);
        Task<Major> CreateMajorAsync(Major major);
        Task<Major> UpdateMajorAsync(Major major);
        Task<bool> DeleteMajorAsync(int id);
    }
}
