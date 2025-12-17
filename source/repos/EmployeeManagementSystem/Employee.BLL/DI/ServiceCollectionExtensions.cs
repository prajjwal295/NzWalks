using Employee.BLL.Interfaces;
using Employee.BLL.Mappings;
using Employee.BLL.Services;
using Employee.Core.Utilities;
using Employee.Dal.Interfaces;
using Employee.Dal.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.BLL.DI
{
    public static class ServiceCollectionExtensions
    {
        // created a IServiceCollection extension method to register all the services in the application
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            // the type of Method will tell DI that it is a generic repository and it open implementation of the IGenericRepository interface
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IJwtTokenRepository, JwtTokenGenerator>();
            services.AddAutoMapper(typeof(AutoMapperProfiles));
            return services;
        }
    }
}
