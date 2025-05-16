using Insurance1CompAppServices.Repositories.LocalRepositories.Ef.Models;
using Microsoft.EntityFrameworkCore;

namespace Insurance1CompAppServices.Repositories.LocalRepositories.Ef
{
    public class AppDbContext : DbContext
    {
        public DbSet<GwpRecord> GwpRecords { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}
