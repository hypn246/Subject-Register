using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExtensionDKM.BUS;
using ExtensionDKM.Models;
using ExtensionDKM.DAL;

namespace ExtensionDKM.Controllers
{
    public class ClassroomsController : Controller
    {
        private readonly ClassroomService _classroomService;
        private readonly MyDBContext _context;

        public ClassroomsController(ClassroomService classroomService, MyDBContext context)
        {
            _classroomService = classroomService;
            _context = context;
        }

        // GET: Classrooms
        public async Task<IActionResult> Index()
        { 
            var classrooms = await _classroomService.GetAllClassroomsAsync();
            return View(classrooms);
        }

        // GET: Classrooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            
            if (id == null)
            {
                return NotFound();
            }

            var classroom = await _classroomService.GetClassroomByIdAsync(id.Value);
            if (classroom == null)
            {
                return NotFound();
            }

            return View(classroom);
        }

        // GET: Classrooms/Create
        public async Task<IActionResult> Create()
        {
            var lecturers = await _context.Users.Where(u => u.Role == UserRole.Lecturer)
                .ToListAsync();
            ViewData["LecturerId"] = new SelectList(lecturers, "Id", "Name");

            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name");
            ViewBag.Rooms = await _context.Room.ToListAsync();
            return View();
        }

        // POST: Classrooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string Semester, [Bind("Id,Time,RoomId,SchoolYear,SS,LecturerId,CourseId")] Classroom classroom)
        {
            var conflict = await _context.Classrooms.FirstOrDefaultAsync(c =>
                c.LecturerId == classroom.LecturerId &&
                c.SchoolYear == classroom.SchoolYear &&
                c.Semester == classroom.Semester &&
                c.Time == classroom.Time
            );
            if (conflict != null) { 
                if (conflict.CourseId == classroom.CourseId)
                    {
                        ModelState.AddModelError("", "You already have teach this course at this time.");
                    }
                    else
                    {
                        ModelState.AddModelError("", "You already has another course at this time");
                    }
            }

            if (ModelState.IsValid)
            {
                classroom.Semester = int.Parse(Semester);
                await _classroomService.CreateClassroomAsync(classroom);
                return RedirectToAction(nameof(Index));
            }
            return View(classroom);
        }

        // GET: Classrooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classroom = await _classroomService.GetClassroomByIdAsync(id.Value);
            if (classroom == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", classroom.CourseId);
            ViewData["LecturerId"] = new SelectList(_context.Users, "Id", "Name", classroom.LecturerId);
            ViewData["RoomId"] = new SelectList(_context.Room, "Id", "Id", classroom.RoomId);
            return View(classroom);
        }

        // POST: Classrooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Time,RoomId,SchoolYear,Semester,SS,LecturerId,CourseId")] Classroom classroom)
        {
            if (id != classroom.Id)
            {
                return NotFound();
            }
            
            var conflict = await _context.Classrooms.FirstOrDefaultAsync(c =>
                c.LecturerId == classroom.LecturerId &&
                c.SchoolYear == classroom.SchoolYear &&
                c.Semester == classroom.Semester &&
                c.Time == classroom.Time
            );
            if (conflict != null)
            {
                if (conflict.CourseId == classroom.CourseId)
                {
                    ModelState.AddModelError("", "You already have teach this course at this time.");
                }
                else
                {
                    ModelState.AddModelError("", "You already has another course at this time");
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _classroomService.UpdateClassroomAsync(classroom);
                }
                catch (ArgumentException)
                {
                    if (await _classroomService.GetClassroomByIdAsync(classroom.Id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(classroom);
        }

        // GET: Classrooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classroom = await _classroomService.GetClassroomByIdAsync(id.Value);
            if (classroom == null)
            {
                return NotFound();
            }

            return View(classroom);
        }

        // POST: Classrooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _classroomService.DeleteClassroomAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
