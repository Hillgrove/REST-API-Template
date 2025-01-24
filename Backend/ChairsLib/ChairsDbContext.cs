using ChairsLib.Models;
using Microsoft.EntityFrameworkCore;

namespace ChairsLib
{
    public class ChairsDbContext : DbContext
    {
        public DbSet<Chair> Chairs { get; set; }

        public ChairsDbContext(DbContextOptions<ChairsDbContext> options)
            : base(options) { }
    }
}