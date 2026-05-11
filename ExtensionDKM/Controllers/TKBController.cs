using ExtensionDKM.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
namespace ExtensionDKM.Controllers
{
    public class TKBController:Controller
    {
        private readonly MyDBContext _context;

        public TKBController(MyDBContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
