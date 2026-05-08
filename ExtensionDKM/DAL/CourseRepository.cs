using ExtensionDKM.Models;
using Microsoft.EntityFrameworkCore;

namespace ExtensionDKM.DAL
{
    public class CourseRepository 
    {
        private readonly MyDBContext _context;

        public CourseRepository(MyDBContext context)
        {
            _context = context;
        }

        public async Task<List<Course>> GetAllAsync()
        {
            return await _context.Courses
                .Include(c => c.Major)
                .Include(c => c.PreviousCourses)
                    .ThenInclude(x => x.PreviousCourse)
                .Include(c => c.RequirementCourses)
                    .ThenInclude(x => x.RequirementCourse)
                .AsSplitQuery()
                .ToListAsync();
        }

        public async Task<Course?> GetByIdAsync(int id)
        {
            return await _context.Courses
                .Include(c => c.Major)
                .Include(c => c.PreviousCourses)
                    .ThenInclude(x => x.PreviousCourse)
                .Include(c => c.RequirementCourses)
                    .ThenInclude(x => x.RequirementCourse)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Course> CreateAsync(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            return course;
        }

        public async Task<Course> UpdateAsync(Course course)
        {
            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
            return course;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
                return false;

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Course>> GetByCourseIdAsync(int courseId)
        {
            return await _context.Courses
                .Where(c => c.Id == courseId)
                .Include(c => c.Major)
                .Include(c => c.PreviousCourses)
                    .ThenInclude(x => x.PreviousCourse)
                .Include(c => c.RequirementCourses)
                    .ThenInclude(x => x.RequirementCourse)
                .ToListAsync();
        }
    }
}
