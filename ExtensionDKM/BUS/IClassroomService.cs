using ExtensionDKM.Models;

namespace ExtensionDKM.BUS
{
    public interface IClassroomService
    {
        Task<List<Classroom>> GetAllClassroomsAsync();
        Task<Classroom?> GetClassroomByIdAsync(int id);
        Task<Classroom> CreateClassroomAsync(Classroom classroom);
        Task<Classroom> UpdateClassroomAsync(Classroom classroom);
        Task<bool> DeleteClassroomAsync(int id);
    }
}
