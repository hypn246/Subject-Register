using ExtensionDKM.Data;
using ExtensionDKM.DTOs;
using ExtensionDKM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ExtensionDKM.Controllers
{

    [Authorize(Roles = "Student")]
    public class EnrollController:Controller
    {
        private readonly MyDBContext _context;
        public EnrollController(MyDBContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            int userId = int.Parse(User.FindFirstValue("UserId"));

            var assignedClassroomIds = await _context.Assigns.Where(a => a.StudentId == userId)
                .Select(a => a.ClassroomId)
                .ToListAsync();

            var classrooms = await _context.Classrooms.Select(c => new ClassrommDTO
                {
                    Id = c.Id,
                    Time = c.Time,
                    SchoolYear = c.SchoolYear,
                    Semester = c.Semester,
                    SS = c.SS,

                    CourseName = c.Course.Name,
                    LecturerName = c.Lecturer.Name,

                    PreviousCourses = c.Course.PreviousCourses
                        .Select(x => x.PreviousCourse.Name)
                        .ToList(),

                    RequirementCourses = c.Course.RequirementCourses
                        .Select(x => x.RequirementCourse.Name)
                        .ToList(),

                    IsAssigned = assignedClassroomIds.Contains(c.Id),
                    AssignedCount = _context.Assigns.Count(a => a.ClassroomId == c.Id),
                })
                .ToListAsync();

            return View(classrooms);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleEnroll(int classroomId, bool isChecked)
        {
            int userId = int.Parse(User.FindFirstValue("UserId"));
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
            {
                return BadRequest("User not found");
            }

            var existing = await _context.Assigns.FirstOrDefaultAsync(x => x.StudentId == userId && x.ClassroomId == classroomId);
            System.Diagnostics.Debug.WriteLine(existing);//

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
            return Ok();
        }

    }
}
