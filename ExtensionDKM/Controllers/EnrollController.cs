using ExtensionDKM.BUS;
using ExtensionDKM.DAL;
using ExtensionDKM.DTO;
using ExtensionDKM.DTOs;
using ExtensionDKM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ExtensionDKM.Controllers
{

    [Authorize(Roles = "Student")]
    public class EnrollController:Controller
    {
        private readonly EnrollServices _enrollService;

        public EnrollController(EnrollServices enrollService)
        {
            _enrollService = enrollService;
        }
        public async Task<IActionResult> Index()
        {
            int userId = int.Parse(User.FindFirst("UserId")?.Value);
 
            var classrooms= await _enrollService.GetAssignCourses(userId);

            return View(classrooms);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleEnroll(int classroomId, bool isChecked)
        {
            int _userId = int.Parse(User.FindFirstValue("UserId"));
            int statusCode= await _enrollService.ToggleEnroll(classroomId:classroomId,isChecked:isChecked, userId:_userId);
            switch (statusCode)
            { 
                case 200:
                    return Ok();
                case 404:
                    return BadRequest();
                default: return StatusCode(statusCode);
            }
        }

    }
}
