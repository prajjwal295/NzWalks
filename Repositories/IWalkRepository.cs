using NzWalks.API.Model.Domain;

namespace NzWalks.API.Repositories
{
    public interface IWalkRepository 
    {
        public Task<List<Walk>> GetAllAsync();

        public Task<Walk?> GetByIdAsync(Guid id);

        public Task<Walk> CreateAsync(Walk walk);

        public Task<Walk?> DeleteByIdAsync(Guid id);

        public Task<Walk?> UpdateByIdAsync(Guid id , Walk walk);
    }
}
