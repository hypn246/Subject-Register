using ExtensionDKM.Models;

namespace ExtensionDKM.DAL
{
    public interface ICourseRepository
    {
        Task<List<Course>> GetAllAsync();
        Task<Course?> GetByIdAsync(int id);
        Task<Course> CreateAsync(Course course);
        Task<Course> UpdateAsync(Course course);
        Task<bool> DeleteAsync(int id);
        Task<List<Course>> GetByCourseIdAsync(int courseId);
    }
}
