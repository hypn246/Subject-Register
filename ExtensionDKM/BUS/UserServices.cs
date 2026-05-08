using ExtensionDKM.DAL;
using ExtensionDKM.Models;

namespace ExtensionDKM.BUS
{
    public class UserServices
    {
        private readonly UserRepository _userRepository;

        public UserServices(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> FindById(int userId)
        {
            return await _userRepository.FindById(userId);
        }
    }
}
