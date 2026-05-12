    using ExtensionDKM.Models;
using Microsoft.EntityFrameworkCore;

namespace ExtensionDKM.DAL
{
    public class ClassroomRepository 
    {
        private readonly MyDBContext _context;

        public ClassroomRepository(MyDBContext context)
        {
            _context = context;
        }

        public async Task<List<Classroom>> GetAllAsync()
        {
            return await _context.Classrooms
                .Include(c => c.Course)
                .Include(c => c.Lecturer)
                .Include(c => c.Room)
                .ToListAsync();
        }

        public async Task<Classroom?> GetByIdAsync(int id)
        {
            return await _context.Classrooms
                .Include(c => c.Course)
                .Include(c => c.Lecturer)
                .Include(c => c.Room)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Classroom> CreateAsync(Classroom classroom)
        {
            _context.Classrooms.Add(classroom);
            await _context.SaveChangesAsync();
            return classroom;
        }

        public async Task<Classroom> UpdateAsync(Classroom classroom)
        {
            _context.Classrooms.Update(classroom);
            await _context.SaveChangesAsync();
            return classroom;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var classroom = await _context.Classrooms.FindAsync(id);
            if (classroom == null)
                return false;

            _context.Classrooms.Remove(classroom);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
