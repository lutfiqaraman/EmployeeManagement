using EmployeeManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.DataAccess.Repositories.Employees.Dto
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Department Department { get; set; }

        public class MapProfile: AutoMapper.Profile
        {
            public MapProfile()
            {
                CreateMap<Employee, EmployeeDto>();
                CreateMap<EmployeeDto, Employee>();
            }
        }
    }
}
