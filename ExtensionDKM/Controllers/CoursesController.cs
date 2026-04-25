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
                    .ThenInclude(x => x.PreviousCourse)

                .Include(c => c.RequirementCourses)
                    .ThenInclude(x => x.RequirementCourse)

                .AsSplitQuery()
                .ToListAsync();

            return View(courses);
        }
        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var course = await _context.Courses
                .Include(c => c.Major)

                .Include(c => c.PreviousCourses)
                    .ThenInclude(x => x.PreviousCourse)

                .Include(c => c.RequirementCourses)
                    .ThenInclude(x => x.RequirementCourse)

                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
                return NotFound();

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
            List<int> PreviousCourseIds, List<int> RequirementCourseIds)
        {
            if (!ModelState.IsValid)
            {
                return View(course);
            }
            try
            {
                _context.Courses.Add(course);
                await _context.SaveChangesAsync();

                // add previous courses
                if (PreviousCourseIds != null)
                {
                    foreach (var pid in PreviousCourseIds.Where(x => x != course.Id).Distinct())
                    {
                        _context.CoursePrevious.Add(new CoursePrevious
                        {
                            CourseId = course.Id,
                            PreviousCourseId = pid
                        });
                    }
                }

                // add required courses
                if (RequirementCourseIds != null)
                {
                    foreach (var rid in RequirementCourseIds.Where(x => x != course.Id).Distinct())
                    {
                        _context.CourseRequirement.Add(new CourseRequirement
                        {
                            CourseId = course.Id,
                            RequirementCourseId = rid
                        });
                    }
                }

                _context.Add(course);
                await _context.SaveChangesAsync();

            }
            catch { }

            return RedirectToAction("Index");

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
                Selected = course.PreviousCourses.Any(pc => pc.PreviousCourseId == c.Id)
            }).ToList();

            ViewBag.RequirementCourses = allCourses.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name,
                Selected = course.RequirementCourses.Any(rc => rc.RequirementCourseId == c.Id)
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
                    // Remove old relations
                    _context.CoursePrevious.RemoveRange(courseReq.PreviousCourses);
                    _context.CourseRequirement.RemoveRange(courseReq.RequirementCourses);

                    // Add Previous Courses
                    if (PreviousCourseIds != null)
                    {
                        foreach (var pid in PreviousCourseIds.Where(x => x != id).Distinct())
                        {
                            _context.CoursePrevious.Add(new CoursePrevious
                            {
                                CourseId = id,
                                PreviousCourseId = pid
                            });
                        }
                    }

                    // Add Required Courses
                    if (RequirementCourseIds != null)
                    {
                        foreach (var rid in RequirementCourseIds.Where(x => x != id).Distinct())
                        {
                            _context.CourseRequirement.Add(new CourseRequirement
                            {
                                CourseId = id,
                                RequirementCourseId = rid
                            });
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
            bool isUsed = await _context.CoursePrevious.AnyAsync(x => x.PreviousCourseId == id) || await _context.CourseRequirement.AnyAsync(x => x.RequirementCourseId == id);
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
