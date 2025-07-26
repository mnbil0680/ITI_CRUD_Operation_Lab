using CRUD_Operation.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD_Operation.Database
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-GFSDRRT; Initial Catalog=CRUD_ITI; Integrated Security=True; Encrypt=False; Trust Server Certificate=True");
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Training> Trainings { get; set;}

    }
}
