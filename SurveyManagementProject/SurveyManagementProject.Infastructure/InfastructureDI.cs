using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SurveyManagementProject.Application.Abstractions.IRepositories;
using SurveyManagementProject.Infastructure.Persistience;
using SurveyManagementProject.Infastructure.Repositories;

namespace SurveyManagementProject.Infastructure
{
    public static class InfastructureDI
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDBContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISurveyRepository, SurveyRepository>();

            return services;
        }
    }
}