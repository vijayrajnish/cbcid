using CBCID_APPLICATION.Models;
using Microsoft.EntityFrameworkCore;

namespace CBCID_APPLICATION.Data
{
    public class ApplicationDBContext: DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        public DbSet<M_Employee> Employees { get; set; }
        public DbSet<Utility_Users> Login_Users { get; set; }
        public DbSet<UserRole> Roles { get; set; }
        public DbSet<DET_CRIME_FEMALE_CHILDREN> FMT1_DET_CRIME_FEMALE_CHILDREN { get; set; }
        public DbSet<Gavaha> TBL_GAVAHA { get; set; }

    }
}
