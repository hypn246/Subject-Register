using ExtensionDKM.DAL;
using ExtensionDKM.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ExtensionDKM.BUS
{
    public class EnrollServices
    {
        private readonly EnrollRepository _enrollRepository;

        public EnrollServices(EnrollRepository enrollRepository)
        {
            _enrollRepository = enrollRepository;
        }
        public async Task<List<SubjectDTO>> GetAssignCourses(int userId)
        {

            return await _enrollRepository.GetAssignCourses(userId);
        }
        public async Task<int> ToggleEnroll(int classroomId, bool isChecked,int userId)
        {
            return await _enrollRepository.ToggleEnroll(classroomId, isChecked, userId);
        }
    }
}
