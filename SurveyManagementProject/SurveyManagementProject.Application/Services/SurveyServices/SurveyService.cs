using SurveyManagementProject.Application.Abstractions.IRepositories;
using SurveyManagementProject.Application.Abstractions.IServises;
using SurveyManagementProject.Domain.Entities.DTOs;
using SurveyManagementProject.Domain.Entities.Exceptions;
using SurveyManagementProject.Domain.Entities.Models;

namespace SurveyManagementProject.Application.Services.SurveyServices
{
    public class SurveyService : ISurveyServise
    {
        private readonly ISurveyRepository _SurveyRepository;

        public SurveyService(ISurveyRepository SurveyRepository)
        {
            _SurveyRepository = SurveyRepository;
        }

        public async Task<string> Create(SurveyDTO SurveyDTO)
        {
            var Title = await _SurveyRepository.GetByAny(x => x.SurveyTitle == SurveyDTO.SurveyTitle);
            if (Title == null)
            {
                var survey = new Survey()
                {
                    SurveyTitle = SurveyDTO.SurveyTitle,
                    SurveyAnswer = SurveyDTO.SurveyAnswer,
                    SurveyQuestion = SurveyDTO.SurveyQuestion,
                    Author = SurveyDTO.Author
                };
                await _SurveyRepository.Create(survey);
                return "Created!";
            }
            throw new SurveyNotFoundException("This title already exists");
        }

        public async Task<string> Delete(int id)
        {
            var result = await _SurveyRepository.Delete(x => x.SurveyId == id);
            if (result)
            {
                return "Deleted";
            }
            else
            {
                throw new SurveyNotFoundException("Survey Not Found");
            }
        }

        public async Task<IEnumerable<Survey>> GetAll()
        {
            var Surveys = await _SurveyRepository.GetAll();

            var result = Surveys.Select(model => new Survey
            {
                SurveyId = model.SurveyId,
                SurveyTitle = model.SurveyTitle,
                SurveyAnswer = model.SurveyAnswer,
                SurveyQuestion = model.SurveyQuestion,
                Author = model.Author
            });

            return result;
        }



        public async Task<Survey> GetById(int Id)
        {
            var result = await _SurveyRepository.GetByAny(x => x.SurveyId == Id);
            if (result != null)
            {
                return result;
            }
            throw new SurveyNotFoundException("Survey not found!");
        }

        public async Task<Survey> GetByTitle(string name)
        {
            var result = await _SurveyRepository.GetByAny(d => d.SurveyTitle == name);
            if (result != null)
            {
                return result;
            }
            throw new SurveyNotFoundException("Survey not found!");
        }

        public async Task<string> Update(int Id, SurveyDTO SurveyDTO)
        {
            var res = await _SurveyRepository.GetAll();
            var id = res.Any(x => x.SurveyId == Id);
            var name = res.Any(x => x.SurveyTitle == SurveyDTO.SurveyTitle);
            if (!id)
            {
                if (!name)
                {
                    var old = await _SurveyRepository.GetByAny(x => x.SurveyId == Id);

                    if (old == null) return "Failed";
                    old.SurveyTitle = SurveyDTO.SurveyTitle;
                    old.SurveyAnswer = SurveyDTO.SurveyAnswer;
                    old.SurveyQuestion = SurveyDTO.SurveyQuestion;
                    old.Author = SurveyDTO.Author;


                    await _SurveyRepository.Update(old);
                    return "Updated";

                }
                return "Such Title already exists";
            }
            throw new SurveyNotFoundException("Survey Not Found");
        }
    }
}
