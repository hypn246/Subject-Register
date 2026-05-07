using ExtensionDKM.Data;
using ExtensionDKM.Models;
using Microsoft.EntityFrameworkCore;

namespace ExtensionDKM.DAL
{
    public class RoomRepository : IRoomRepository
    {
        private readonly MyDBContext _context;

        public RoomRepository(MyDBContext context)
        {
            _context = context;
        }

        public async Task<List<Room>> GetAllAsync()
        {
            return await _context.Room
                .Include(r => r.Classrooms)
                .ToListAsync();
        }

        public async Task<Room?> GetByIdAsync(int id)
        {
            return await _context.Room
                .Include(r => r.Classrooms)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Room> CreateAsync(Room room)
        {
            _context.Room.Add(room);
            await _context.SaveChangesAsync();
            return room;
        }

        public async Task<Room> UpdateAsync(Room room)
        {
            _context.Room.Update(room);
            await _context.SaveChangesAsync();
            return room;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var room = await _context.Room.FindAsync(id);
            if (room == null)
                return false;

            _context.Room.Remove(room);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
