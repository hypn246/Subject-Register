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
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name");
            ViewData["LecturerId"] = new SelectList(_context.Set<Lecturer>(), "Id", "Discriminator");
            ViewData["RoomId"] = new SelectList(_context.Room, "Id", "Id");
            return View();
        }

        // POST: Classrooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Time,RoomId,SchoolYear,Semester,SS,LecturerId,CourseId")] Classroom classroom)
        {
            if (ModelState.IsValid)
            {
                _context.Add(classroom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", classroom.CourseId);
            ViewData["LecturerId"] = new SelectList(_context.Set<Lecturer>(), "Id", "Discriminator", classroom.LecturerId);
            ViewData["RoomId"] = new SelectList(_context.Room, "Id", "Id", classroom.RoomId);
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
            ViewData["LecturerId"] = new SelectList(_context.Set<Lecturer>(), "Id", "Discriminator", classroom.LecturerId);
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
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", classroom.CourseId);
            ViewData["LecturerId"] = new SelectList(_context.Set<Lecturer>(), "Id", "Discriminator", classroom.LecturerId);
            ViewData["RoomId"] = new SelectList(_context.Room, "Id", "Id", classroom.RoomId);
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
