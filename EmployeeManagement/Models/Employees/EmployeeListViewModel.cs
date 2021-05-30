using EmployeeManagement.DataAccess.Repositories.Employees.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Presentation.Models.Employees
{
    public class EmployeeListViewModel
    {
        public IReadOnlyList<EmployeeDto> Employees { get; set; }
    }
}
