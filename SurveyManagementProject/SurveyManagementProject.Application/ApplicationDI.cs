using Microsoft.Extensions.DependencyInjection;
using SurveyManagementProject.Application.Abstractions.IServices;
using SurveyManagementProject.Application.Abstractions.IServises;
using SurveyManagementProject.Application.Services.AuthServices;
using SurveyManagementProject.Application.Services.SurveyServices;
using SurveyManagementProject.Application.Services.UserServices;

namespace SurveyManagementProject.Application
{
    public static class ApplicationDI
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISurveyServise, SurveyService>();
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
