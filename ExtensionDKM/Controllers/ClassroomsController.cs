using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExtensionDKM.Data;
using ExtensionDKM.Models;

namespace ExtensionDKM.Controllers
{
    public class ClassroomsController : Controller
    {
        private readonly MyDBContext _context;

        public ClassroomsController(MyDBContext context)
        {
            _context = context;
        }

        // GET: Classrooms
        public async Task<IActionResult> Index()
        {
            var myDBContext = _context.Classrooms.Include(c => c.Course).Include(c => c.Lecturer).Include(c => c.Room);
            return View(await myDBContext.ToListAsync());
        }

        // GET: Classrooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classroom = await _context.Classrooms
                .Include(c => c.Course)
                .Include(c => c.Lecturer)
                .Include(c => c.Room)
                .FirstOrDefaultAsync(m => m.Id == id);
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
            ViewData["RoomId"] = new SelectList(_context.Room, "Id", "Name");
            return View();
        }

        // POST: Classrooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string Semester, [Bind("Id,Time,RoomId,SchoolYear,SS,LecturerId,CourseId")] Classroom classroom)
        {
            var existing = await _context.Classrooms.Where(c => c.LecturerId == classroom.LecturerId)
                .ToListAsync();

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
                _context.Add(classroom);
                await _context.SaveChangesAsync();
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

            var classroom = await _context.Classrooms.FindAsync(id);
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
            // //LINQ
            //var conflict = await (from classrooms in  _context.Classrooms
            //                      where (classrooms.LecturerId == classroom.Id 
            //                      && classrooms.SchoolYear == classroom.SchoolYear
            //                      && classrooms.Semester == classroom.Semester
            //                      && classrooms.Time == classroom.Time)
            //                      select classrooms).FirstOrDefaultAsync();
            //LINQ method
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
                    _context.Update(classroom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassroomExists(classroom.Id))
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

            var classroom = await _context.Classrooms
                .Include(c => c.Course)
                .Include(c => c.Lecturer)
                .Include(c => c.Room)
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var classroom = await _context.Classrooms.FindAsync(id);
            if (classroom != null)
            {
                _context.Classrooms.Remove(classroom);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassroomExists(int id)
        {
            return _context.Classrooms.Any(e => e.Id == id);
        }
    }
}
