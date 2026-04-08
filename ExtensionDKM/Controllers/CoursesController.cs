using ExtensionDKM.Data;
using ExtensionDKM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace ExtensionDKM.Controllers
{
    [Authorize(Roles ="Admin")]
    public class CoursesController : Controller
    {
        private readonly MyDBContext _context;

        public CoursesController(MyDBContext context)
        {
            _context = context;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            var courses = await _context.Courses
                .Include(c => c.Major)
                .Include(c => c.PreviousCourses)
                .ThenInclude(pc => pc.Major) 
                .Include(c => c.RequirementCourses)
                .ThenInclude(rc => rc.Major)
                .ToListAsync();

            return View(courses);
        }
        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.Majors = new SelectList(_context.Majors, "Id", "Name");


            var course = await _context.Courses
                            .Include(c => c.PreviousCourses)
                            .Include(c => c.RequirementCourses)
                            .FirstOrDefaultAsync(c => c.Id == id);
            if (course == null)
            {
                return NotFound();
            }


            // Selected
            var allCourses = await _context.Courses.Where(x => x.Id != id)
                .ToListAsync();
            ViewBag.Courses = allCourses.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();

            ViewBag.PreviousCourses = allCourses.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name,
                Selected = course.PreviousCourses.Any(pc => pc.Id == c.Id)
            }).ToList();

            ViewBag.RequirementCourses = allCourses.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name,
                Selected = course.RequirementCourses.Any(rc => rc.Id == c.Id)
            }).ToList();

            return View(course);
        }

        // GET: Courses/Create
        public  async Task<IActionResult> Create()
        {
            //ViewBag.Courses = _context.Courses.ToList();// List<Course> => foreach
            //ViewBag.Majors = _context.Majors.ToList();// List<Course> => foreach
            ViewBag.Courses = await _context.Courses
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                })
                .ToListAsync();

            ViewBag.Majors = await _context.Majors
                .Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.Name
                })
                .ToListAsync();

            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Credit,Tuition, MajorId")] Course course, 
            List<int> PreviousCourseIds,List<int> RequirementCourseIds)
        {
            if (ModelState.IsValid)
            {
                if (PreviousCourseIds != null && PreviousCourseIds.Any())
                {
                    course.PreviousCourses = await _context.Courses
                        .Where(c => PreviousCourseIds.Contains(c.Id))
                        .ToListAsync();
                }

                if (RequirementCourseIds != null && RequirementCourseIds.Any())
                {
                    course.RequirementCourses = await _context.Courses
                        .Where(c => RequirementCourseIds.Contains(c.Id))
                        .ToListAsync();
                }

                _context.Add(course);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");

            }
            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.Majors = new SelectList(_context.Majors, "Id", "Name");


            var course = await _context.Courses
                            .Include(c => c.PreviousCourses)
                            .Include(c => c.RequirementCourses)
                            .FirstOrDefaultAsync(c => c.Id == id);
            if (course == null)
            {
                return NotFound();
            }


            // Selected
            var allCourses = await _context.Courses.Where(x=>x.Id!=id)
                .ToListAsync();
            ViewBag.Courses = allCourses.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();

            ViewBag.PreviousCourses = allCourses.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name,
                Selected = course.PreviousCourses.Any(pc => pc.Id == c.Id)
            }).ToList();

            ViewBag.RequirementCourses = allCourses.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name,
                Selected = course.RequirementCourses.Any(rc => rc.Id == c.Id)
            }).ToList();

            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Credit,Tuition,MajorId")] Course course,
             List<int> PreviousCourseIds, List<int> RequirementCourseIds)
        {
            if (id != course.Id)
                return NotFound();

            var courseReq = await _context.Courses
                .Include(c => c.PreviousCourses)
                .Include(c => c.RequirementCourses)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (courseReq == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    courseReq.Name = course.Name;
                    courseReq.Credit = course.Credit;
                    courseReq.Tuition = course.Tuition;
                    courseReq.MajorId = course.MajorId;
                    if (PreviousCourseIds != null && PreviousCourseIds.Any())
                    {
                        var previousCourses = await _context.Courses
                            .Where(c => PreviousCourseIds.Contains(c.Id))
                            .ToListAsync();

                        //reset rel
                        PreviousCourseIds = PreviousCourseIds?.Where(x => x != id).ToList();
                        RequirementCourseIds = RequirementCourseIds?.Where(x => x != id).ToList();

                        // Replace PreviousCourses
                        courseReq.PreviousCourses = await _context.Courses
                            .Where(c => PreviousCourseIds.Contains(c.Id))
                            .ToListAsync();

                        // Replace RequirementCourses
                        courseReq.RequirementCourses = await _context.Courses
                            .Where(c => RequirementCourseIds.Contains(c.Id))
                            .ToListAsync();
                    }
                    if (RequirementCourseIds != null && RequirementCourseIds.Any())
                    {
                        var requirementCourses = await _context.Courses
                            .Where(c => RequirementCourseIds.Contains(c.Id))
                            .ToListAsync();

                        foreach (var rc in requirementCourses)
                        {
                            courseReq.RequirementCourses.Add(rc);
                        }
                    }

                    await _context.SaveChangesAsync(); 
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(course);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool isUsed = await _context.Courses.AnyAsync(c =>
                   c.PreviousCourses.Any(pc => pc.Id == id) ||
                   c.RequirementCourses.Any(rc => rc.Id == id)
               );

            if (isUsed)
            {
                TempData["Error"] = "Cannot delete this course because it is used in other courses.";
                return RedirectToAction("Delete",id);
            }
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }
    }
}
