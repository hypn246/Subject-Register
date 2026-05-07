using ExtensionDKM.Data;
using ExtensionDKM.Models;
using Microsoft.EntityFrameworkCore;

namespace ExtensionDKM.DAL
{
    public class MajorRepository : IMajorRepository
    {
        private readonly MyDBContext _context;

        public MajorRepository(MyDBContext context)
        {
            _context = context;
        }

        public async Task<List<Major>> GetAllAsync()
        {
            return await _context.Majors
                .Include(m => m.Users)
                .Include(m => m.Courses)
                .ToListAsync();
        }

        public async Task<Major?> GetByIdAsync(int id)
        {
            return await _context.Majors
                .Include(m => m.Users)
                .Include(m => m.Courses)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Major> CreateAsync(Major major)
        {
            _context.Majors.Add(major);
            await _context.SaveChangesAsync();
            return major;
        }

        public async Task<Major> UpdateAsync(Major major)
        {
            _context.Majors.Update(major);
            await _context.SaveChangesAsync();
            return major;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var major = await _context.Majors.FindAsync(id);
            if (major == null)
                return false;

            _context.Majors.Remove(major);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
