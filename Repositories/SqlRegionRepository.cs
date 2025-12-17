using Microsoft.EntityFrameworkCore;
using NzWalks.API.Data;
using NzWalks.API.Model.Domain;

namespace NzWalks.API.Repositories
{
    public class SqlRegionRepository : IRegionRepository
    {
        private readonly NzWalksDbContext _dbContext;

        public SqlRegionRepository(NzWalksDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await _dbContext.Regions.ToListAsync();
        }


        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Regions.FindAsync(id);
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await _dbContext.Regions.AddAsync(region);
            await _dbContext.SaveChangesAsync();

            return region;
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var RegionModal = await _dbContext.Regions.FindAsync(id);

            if (RegionModal == null)
                return null;

            RegionModal.Name = region.Name;
            RegionModal.RegionImageUrl = region.RegionImageUrl;
            RegionModal.Code = region.Code;

            await _dbContext.SaveChangesAsync();

            return RegionModal;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var existingData = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (existingData == null)
                return null;

            _dbContext.Regions.Remove(existingData);
            await _dbContext.SaveChangesAsync();

            return existingData;
        }
    }
}
