using ExtensionDKM.Data;
using ExtensionDKM.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExtensionDKM.Controllers
{
    public class LoginController : Controller
    {

        private readonly MyDBContext _context;
        public LoginController(MyDBContext context)
        {
            _context = context;
        }

        
        public IActionResult Index([Bind("Username,Password")] User user)
        {
            return View();
        }

    }
}
