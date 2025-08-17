using AutoMapper;
using Employee.BLL.BOs.Requests;
using Employee.BLL.BOs.Response;
using Employee.BLL.DTOs;
using Employee.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.BLL.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() {
            CreateMap<Employees, CreateEmployeeBO>().ReverseMap();
            CreateMap<Employees, EmployeeResponsebo>().ReverseMap();
            CreateMap<EmployeeDto, EmployeeResponsebo>().ReverseMap();
            CreateMap<EmployeeDto, CreateEmployeeBO>().ReverseMap();
            CreateMap<EmployeeDto, Employees>().ReverseMap();
            CreateMap<EmployeeDto, UpdateEmployeeBO>().ReverseMap();

            CreateMap<Department, UpsertDepartmentBO>().ReverseMap();
            CreateMap<DepartmentDto, UpsertDepartmentBO>().ReverseMap();
            CreateMap<DepartmentDto, DepartmentResponseBO>().ReverseMap();
            CreateMap<DepartmentDto, Department>().ReverseMap();
            CreateMap<DepartmentDto, DepartmentNavigationBO>().ReverseMap();
        }
    }
}
