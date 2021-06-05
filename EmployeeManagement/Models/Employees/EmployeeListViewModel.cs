using EmployeeManagement.DataAccess.Repositories.Employees.Dto;
using System.Collections.Generic;

namespace EmployeeManagement.Presentation.Models.Employees
{
    public class EmployeeListViewModel
    {
        public IReadOnlyList<EmployeeDto> Employees { get; set; }
    }
}
