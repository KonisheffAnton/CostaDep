using Costa.Entities;
using Microsoft.EntityFrameworkCore;

namespace Costa.Data
{
    public class ApplicationContext : DbContext
    {

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
           
    }

        public DbSet<Employee> Employee { get; set; }

        public DbSet<Department> Department { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("Empoyee");
        }
    }

}


