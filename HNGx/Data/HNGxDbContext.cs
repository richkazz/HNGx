using HNGx.Models;
using Microsoft.EntityFrameworkCore;

namespace HNGx.Data
{
    public class HNGxDbContext : DbContext
    {
        public HNGxDbContext(DbContextOptions<HNGxDbContext> options) : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
