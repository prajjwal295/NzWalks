using Microsoft.EntityFrameworkCore;
using NzWalks.API.Model.Domain;
namespace NzWalks.API.Data
{
    public class NzWalksDbContext : DbContext
    {
        public NzWalksDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        // all of this property represent the database colllection 
        // when we run migration , this will create the tables
        // prop --> shortcut for creating property dbset

        public DbSet<Difficulty> Difficulties { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<Walk> Walks { get; set; }
    }
}
