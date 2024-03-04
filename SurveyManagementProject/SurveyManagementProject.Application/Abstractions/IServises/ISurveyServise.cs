using SurveyManagementProject.Domain.Entities.DTOs;
using SurveyManagementProject.Domain.Entities.Models;

namespace SurveyManagementProject.Application.Abstractions.IServises
{
    public interface ISurveyServise
    {
        public Task<string> Create(SurveyDTO surveyDTO);
        public Task<Survey> GetByTitle(string name);
        public Task<Survey> GetById(int Id);
        public Task<IEnumerable<Survey>> GetAll();
        public Task<string> Delete(int id);
        public Task<string> Update(int Id, SurveyDTO surveyDTO);
    }
}
