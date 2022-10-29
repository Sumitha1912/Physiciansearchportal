using Microsoft.EntityFrameworkCore;
using Physiciansearchportal.DomainModel;

namespace Physiciansearchportal.Data
{
    public class PhysiciansearchDbContext : DbContext
    {
        public PhysiciansearchDbContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<Physician> Physicians { get; set; }

        
    }
}
