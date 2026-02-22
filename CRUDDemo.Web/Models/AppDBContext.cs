using Microsoft.EntityFrameworkCore;

namespace CRUDDemo.Web.Models
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        public DbSet<Person> People { get; set; }
    }
}
