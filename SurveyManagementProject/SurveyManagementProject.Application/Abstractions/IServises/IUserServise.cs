using SurveyManagementProject.Domain.Entities.DTOs;
using SurveyManagementProject.Domain.Entities.Models;

namespace SurveyManagementProject.Application.Abstractions.IServises
{
    public interface IUserService
    {
        public Task<string> Create(UserDTO userDTO);
        public Task<User> GetByName(string name);
        public Task<User> GetById(int Id);
        public Task<User> GetByEmail(string email);
        public Task<IEnumerable<User>> GetAll();
        public Task<string> Delete(int Id);
        public Task<string> Update(int Id, UserDTO userDTO);
        public Task<string> GetPdfPath();
    }
}
