using ExtensionDKM.DAL;
using ExtensionDKM.Models;

namespace ExtensionDKM.BUS
{
    public class RoomService 
    {
        private readonly RoomRepository _roomRepository;

        public RoomService(RoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<List<Room>> GetAllRoomsAsync()
        {
            return await _roomRepository.GetAllAsync();
        }

        public async Task<Room?> GetRoomByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Room ID must be greater than 0");

            return await _roomRepository.GetByIdAsync(id);
        }

        public async Task<Room> CreateRoomAsync(Room room)
        {
            if (room == null)
                throw new ArgumentNullException(nameof(room));

            if (string.IsNullOrWhiteSpace(room.Name))
                throw new ArgumentException("Room name is required");

            return await _roomRepository.CreateAsync(room);
        }

        public async Task<Room> UpdateRoomAsync(Room room)
        {
            if (room == null)
                throw new ArgumentNullException(nameof(room));

            if (string.IsNullOrWhiteSpace(room.Name))
                throw new ArgumentException("Room name is required");

            return await _roomRepository.UpdateAsync(room);
        }

        public async Task<bool> DeleteRoomAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Room ID must be greater than 0");

            return await _roomRepository.DeleteAsync(id);
        }
    }
}
