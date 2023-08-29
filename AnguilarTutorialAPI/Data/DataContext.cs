using AnguilarTutorialAPI.Entity;
using Microsoft.EntityFrameworkCore;

namespace AnguilarTutorialAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        
        public virtual DbSet<AppUser> Users { get; set; }
    }
}
