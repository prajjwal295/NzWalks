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

        //applying filter and sorting feature here 
        public async Task<List<Walk>> GetAllAsync(int pageNumber, int pageSize , bool isAscending = true, string? sortBy = null, string? filterOn = null , string? filterQuery=null)
        {
            // we fetch the queryable object first
            var walks = _dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();

            //apply the filter here
            if(String.IsNullOrEmpty(filterOn)==false && String.IsNullOrEmpty(filterQuery) == false)
            {
                if(filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
            }

            //apply filter feature

            if(string.IsNullOrEmpty(sortBy)==false)
            {
                if(sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x=>x.Name);  
                }

                if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }
            }

            // skip results
            var skipCount = (pageNumber - 1) * pageSize;

            //use skip and take for the paginations
            walks = walks.Skip(skipCount).Take(pageSize);

            return await walks.ToListAsync();

            //return await _dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
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
