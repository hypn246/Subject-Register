using ExtensionDKM.DAL;
using ExtensionDKM.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExtensionDKM.Controllers
{
    public class TKBController : Controller
    {
        private readonly MyDBContext _context;

        public TKBController(MyDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var schedules = await _context.Classrooms
                .Include(c => c.Course)
                .Include(c => c.Room)
                .Include(c => c.Lecturer)
                .Select(c => new ScheduleDTO
                {
                    CourseId = c.CourseId,
                    CourseName = c.Course!.Name,
                    LecturerName = c.Lecturer!.Name,
                    RoomName = c.Room!.Name,
                    Time = c.Time,
                    SchoolYear = c.SchoolYear,
                    Semester = c.Semester
                })
                .ToListAsync();

            return View(schedules);
        }
    }
}