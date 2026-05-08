using ExtensionDKM.DAL;
using ExtensionDKM.Models;

namespace ExtensionDKM.BUS
{
    public class MajorService 
    {
        private readonly MajorRepository _majorRepository;

        public MajorService(MajorRepository majorRepository)
        {
            _majorRepository = majorRepository;
        }

        public async Task<List<Major>> GetAllMajorsAsync()
        {
            return await _majorRepository.GetAllAsync();
        }

        public async Task<Major?> GetMajorByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Major ID must be greater than 0");

            return await _majorRepository.GetByIdAsync(id);
        }

        public async Task<Major> CreateMajorAsync(Major major)
        {
            if (major == null)
                throw new ArgumentNullException(nameof(major));

            if (string.IsNullOrWhiteSpace(major.Name))
                throw new ArgumentException("Major name is required");

            return await _majorRepository.CreateAsync(major);
        }

        public async Task<Major> UpdateMajorAsync(Major major)
        {
            if (major == null)
                throw new ArgumentNullException(nameof(major));

            if (string.IsNullOrWhiteSpace(major.Name))
                throw new ArgumentException("Major name is required");

            return await _majorRepository.UpdateAsync(major);
        }

        public async Task<bool> DeleteMajorAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Major ID must be greater than 0");

            return await _majorRepository.DeleteAsync(id);
        }
    }
}
