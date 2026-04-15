using ExtensionDKM.Data;
using Microsoft.AspNetCore.Mvc;
using ExtensionDKM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            var classroom = await _context.Classrooms
                .Include(c => c.Course)
                .Include(c => c.Lecturer)
                .Include(c => c.Room)
                .ToListAsync();
            return View(classroom);
        }
    }
}
