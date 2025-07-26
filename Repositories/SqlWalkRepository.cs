using Microsoft.EntityFrameworkCore;
using NzWalks.API.Data;
using NzWalks.API.Model.Domain;

namespace NzWalks.API.Repositories
{
    public class SqlWalkRepository : IWalkRepository
    {
        private readonly NzWalksDbContext _dbContext;
        public SqlWalkRepository(NzWalksDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
            await  _dbContext.Walks.AddAsync(walk);
            await _dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> DeleteByIdAsync(Guid id)
        {
           var walkModal = await _dbContext.Walks.FirstOrDefaultAsync(x => x.Id==id);

            if (walkModal == null)
                return null;

             _dbContext.Walks.Remove(walkModal);
            await _dbContext.SaveChangesAsync();
            return walkModal;
        }

        public async Task<List<Walk>> GetAllAsync()
        {
            return await _dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Walks.FindAsync(id);
        }

        public async Task<Walk?> UpdateByIdAsync(Guid id , Walk walk)
        {
            var existingWalk = await _dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (existingWalk == null)
                return null;

            existingWalk.Name = walk.Name;
            existingWalk.Description = walk.Description;
            existingWalk.LengthInKm = walk.LengthInKm;
            existingWalk.WalkImageUrl = walk.WalkImageUrl;
            existingWalk.DifficultyId = walk.DifficultyId;
            existingWalk.RegionId = walk.RegionId;

            await _dbContext.SaveChangesAsync();

            return existingWalk;
        }
    }
}
