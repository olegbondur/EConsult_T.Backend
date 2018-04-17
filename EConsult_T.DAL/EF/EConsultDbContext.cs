using EConsult_T.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace EConsult_T.DAL.EF
{
    public class EConsultDbContext : DbContext
    {
        public DbSet<DateRange> DateRanges { get; set; }
        public DbSet<User> Users { get; set; }

        public EConsultDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
