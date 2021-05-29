using EmployeeManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.DataAccess.EntityFramework
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData
                (
                    new Employee() { Id = 1, Name = "Mary", Department = Department.HR, Email = "mary@mockemp.com" },
                    new Employee() { Id = 2, Name = "Jade", Department = Department.IT, Email = "jade@mockemp.com" },
                    new Employee() { Id = 3, Name = "John", Department = Department.IT, Email = "john@mockemp.com" },
                    new Employee() { Id = 4, Name = "Ryan", Department = Department.Financial, Email = "ryan@mockemp.com" }
                );
        }
    }
}
