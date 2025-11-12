using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using ParkingHub.Data;
using ParkingHub.Repositories;
using ParkingHub.Services;
using ParkingHub.Validators;

namespace ParkingHub.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddControllers().ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<EmployeeCreateDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<EmployeeUpdateDtoValidator>();

            return services;
        }
    }
}
