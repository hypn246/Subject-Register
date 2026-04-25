using ExtensionDKM.Data;
using ExtensionDKM.DTOs;
using ExtensionDKM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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
            //var classrooms = await _context.Classrooms
            //    .AsSplitQuery()
            //    .Include(c => c.Course)
            //        .ThenInclude(c => c.PreviousCourses)
            //    .Include(c => c.Course)
            //        .ThenInclude(c => c.RequirementCourses)
            //    .Include(c => c.Lecturer)
            //    .Include(c => c.Room)
            //    .ToListAsync();
            IEnumerable<ClassrommDTO> classrooms = await _context.Classrooms
                .Select(c => new ClassrommDTO
                {
                    Id = c.Id,
                    Time = c.Time,
                    SchoolYear = c.SchoolYear,
                    Semester = c.Semester,
                    SS = c.SS,

                    CourseName = c.Course.Name,
                    LecturerName = c.Lecturer.Name,
                    RoomName = c.Room.Name,

                    PreviousCourses = c.Course.PreviousCourses
                .Select(x => x.PreviousCourse.Name)
                .ToList(),

                    RequirementCourses = c.Course.RequirementCourses
                .Select(x => x.RequirementCourse.Name)
                .ToList()
                })
                .ToListAsync();

            return View(classrooms);
        }
    }
}
