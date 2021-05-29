using EmployeeManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.DataAccess.EntityFramework
{
    public static class ModelBuilderExtensions
    {
        public static void SeedEmployeeData(this ModelBuilder modelBuilder)
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
