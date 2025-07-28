using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NzWalks.API.Data
{
    public class NzWalksAuthDbContext : IdentityDbContext
    {
        //the base(option) basically specifies that we are passing the options from the program.cs 
        public NzWalksAuthDbContext(DbContextOptions<NzWalksAuthDbContext> options ) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var readerRoleId = "32e9755e-f728-4bb7-8c81-80de47475cc8";
            var writerRoleId = "006278d5-0cc5-4f76-90a9-9666e74bc93d";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id=readerRoleId,
                    ConcurrencyStamp = readerRoleId,
                    Name="Reader",
                    NormalizedName  = "Reader".ToUpper()
                },
                 new IdentityRole
                {
                    Id=writerRoleId,
                    ConcurrencyStamp = writerRoleId,
                    Name="Writer",
                    NormalizedName  = "Writer".ToUpper()
                }
            };


            modelBuilder.Entity<IdentityRole>().HasData(roles);
        } 
    }
}
