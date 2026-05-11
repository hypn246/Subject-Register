using ExtensionDKM.DTO;
using ExtensionDKM.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExtensionDKM.DAL
{
    public class EnrollRepository
    {
        private readonly MyDBContext _context;

        public EnrollRepository(MyDBContext context)
        {
            _context = context;
        }

        //GET
        public async Task<List<SubjectDTO>> GetAssignCourses(int userId)
        {
            var assignedClassroomIds = await _context.Assigns.Where(a => a.StudentId == userId)
                .Select(a => a.ClassroomId)
                .ToListAsync();

            return await _context.Classrooms.Select(c => new SubjectDTO
            {
                Id = c.Id,
                Time = c.Time,
                SchoolYear = c.SchoolYear,
                Semester = c.Semester,

                SS = c.Assignments.Count,
                Capacity = c.Room.Capacity ?? 0,

                CourseId = c.CourseId,
                CourseName = c.Course.Name,
                LecturerName = c.Lecturer.Name,


                PreviousCourses = c.Course.PreviousCourses
                        .Select(x => x.PreviousCourse.Name)
                        .ToList(),

                RequirementCourses = c.Course.RequirementCourses
                        .Select(x => x.RequirementCourse.Name)
                        .ToList(),

                IsAssigned = assignedClassroomIds.Contains(c.Id),

            }).ToListAsync();
        }

        //POST
        public async Task<int> ToggleEnroll(int classroomId, bool isChecked, int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
            {
                return (404);
            }

            var existing = await _context.Assigns.FirstOrDefaultAsync(x => x.StudentId == userId && x.ClassroomId == classroomId);

            if (isChecked)
            {
                if (existing == null)
                {
                    _context.Assigns.Add(new Assign
                    {
                        StudentId = userId,
                        ClassroomId = classroomId,
                    });
                }
            }
            else
            {
                if (existing != null)
                    _context.Assigns.Remove(existing);
            }

            await _context.SaveChangesAsync();
            return (200);
        }
    }
}
