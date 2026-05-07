using ExtensionDKM.DAL;
using ExtensionDKM.Models;

namespace ExtensionDKM.BUS
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<List<Course>> GetAllCoursesAsync()
        {
            return await _courseRepository.GetAllAsync();
        }

        public async Task<Course?> GetCourseByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Course ID must be greater than 0");

            return await _courseRepository.GetByIdAsync(id);
        }

        public async Task<Course> CreateCourseAsync(Course course)
        {
            if (course == null)
                throw new ArgumentNullException(nameof(course));

            if (string.IsNullOrWhiteSpace(course.Name))
                throw new ArgumentException("Course name is required");

            return await _courseRepository.CreateAsync(course);
        }

        public async Task<Course> UpdateCourseAsync(Course course)
        {
            if (course == null)
                throw new ArgumentNullException(nameof(course));

            if (string.IsNullOrWhiteSpace(course.Name))
                throw new ArgumentException("Course name is required");

            return await _courseRepository.UpdateAsync(course);
        }

        public async Task<bool> DeleteCourseAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Course ID must be greater than 0");

            return await _courseRepository.DeleteAsync(id);
        }
    }
}
