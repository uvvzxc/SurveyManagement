using SurveyManagementProject.Domain.Entities.DTOs;
using SurveyManagementProject.Domain.Entities.Models;

namespace SurveyManagementProject.Application.Abstractions.IServices
{
    public interface IAuthService
    {
        public Task<ResponceLogin> GenerateToken(RequestLogin request);
        public Task<string> SignUp(UserDTO request);
    }
}