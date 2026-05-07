using ExtensionDKM.Models;

namespace ExtensionDKM.DAL
{
    public interface IClassroomRepository
    {
        Task<List<Classroom>> GetAllAsync();
        Task<Classroom?> GetByIdAsync(int id);
        Task<Classroom> CreateAsync(Classroom classroom);
        Task<Classroom> UpdateAsync(Classroom classroom);
        Task<bool> DeleteAsync(int id);
    }
}
