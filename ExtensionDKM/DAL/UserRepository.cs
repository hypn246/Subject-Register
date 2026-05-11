using ExtensionDKM.Models;
using Microsoft.EntityFrameworkCore;

namespace ExtensionDKM.DAL
{
    public class UserRepository
    {
        private readonly MyDBContext _context;

        public UserRepository(MyDBContext context)
        {
            _context = context;
        }
        public async Task<User?> FindById(int id)
        {
            return await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
        }

    }
}
