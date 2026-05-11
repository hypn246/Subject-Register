using ExtensionDKM.DAL;
using ExtensionDKM.Models;

namespace ExtensionDKM.BUS
{
    public class ClassroomService 
    {
        private readonly ClassroomRepository _classroomRepository;

        public ClassroomService(ClassroomRepository classroomRepository)
        {
            _classroomRepository = classroomRepository;
        }

        public async Task<List<Classroom>> GetAllClassroomsAsync()
        {
            return await _classroomRepository.GetAllAsync();
        }

        public async Task<Classroom?> GetClassroomByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Classroom ID must be greater than 0");

            return await _classroomRepository.GetByIdAsync(id);
        }

        public async Task<Classroom> CreateClassroomAsync(Classroom classroom)
        {
            //if (classroom == null)
            //    throw new ArgumentNullException(nameof(classroom));
            ArgumentNullException.ThrowIfNull(classroom);

            return await _classroomRepository.CreateAsync(classroom);
        }

        public async Task<Classroom> UpdateClassroomAsync(Classroom classroom)
        {
            //if (classroom == null)
            //    throw new ArgumentNullException(nameof(classroom));
            ArgumentNullException.ThrowIfNull(classroom);

            return await _classroomRepository.UpdateAsync(classroom);
        }

        public async Task<bool> DeleteClassroomAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Classroom ID must be greater than 0");

            return await _classroomRepository.DeleteAsync(id);
        }
    }
}
