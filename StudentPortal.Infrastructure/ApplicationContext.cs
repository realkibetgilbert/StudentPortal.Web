using Microsoft.EntityFrameworkCore;
using StudentPortal.MODEL;

namespace StudentPortal.Infrastructure
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }
    }
}
