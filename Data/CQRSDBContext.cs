using Microsoft.EntityFrameworkCore;
using Models;

namespace Data
{
    public class CQRSDBContext : DbContext
    {
        public CQRSDBContext(DbContextOptions<CQRSDBContext> options) : base(options)
        {
        }

        public DbSet<Person> People { get; set; }
        // protected override void OnConfiguring(DbContextOptionsBuilder options) =>
        //     options.UseInMemoryDatabase(databaseName: "CQRSDb");
    }
}

