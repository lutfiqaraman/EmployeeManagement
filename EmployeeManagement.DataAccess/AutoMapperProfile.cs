using AutoMapper;
using EmployeeManagement.DataAccess.Repositories.Employees.Dto;
using EmployeeManagement.Domain.Models;

namespace EmployeeManagement.DataAccess
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeDto, Employee>();
            CreateMap<CreateEditEmployeeDto, Employee>();
            CreateMap<Employee, CreateEditEmployeeDto>();
        }
    }
}
