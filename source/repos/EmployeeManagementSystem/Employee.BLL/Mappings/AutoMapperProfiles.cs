using AutoMapper;
using Employee.Application.DTOs.Requests;
using Employee.Application.DTOs.Response;
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
            CreateMap<Employees , CreateEmployeeRequestDto>().ReverseMap();
            CreateMap<Employees, EmployeeResponseDto>().ReverseMap();
            CreateMap<Department, CreateDepartmentRequestDto>().ReverseMap();
        }
    }
}
