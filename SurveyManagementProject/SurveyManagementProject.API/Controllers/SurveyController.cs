using Microsoft.AspNetCore.Mvc;
using SurveyManagementProject.Application.Abstractions.IServises;
using SurveyManagementProject.Domain.Entities.DTOs;
using SurveyManagementProject.Domain.Entities.Models;

namespace SurveyManagementProject.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SurveyController : ControllerBase
    {
        private readonly ISurveyServise _surveyServise;
        public SurveyController(ISurveyServise surveyServise)
        {
            _surveyServise = surveyServise;
        }
        [HttpGet]
        public async Task<IEnumerable<Survey>> GetAll()
        {
            var res = await _surveyServise.GetAll();
            return res;
        }
        [HttpGet]
        public async Task<Survey> GetByTitle(string name)
        {
            var res = await _surveyServise.GetByTitle(name);
            return res;
        }
        [HttpGet]
        public async Task<Survey> GetById(int Id)
        {
            var res = await _surveyServise.GetById(Id);
            return res;
        }
        [HttpPost]
        public async Task<string> Create([FromForm]SurveyDTO surveyDTO)
        {
            var res = await _surveyServise.Create(surveyDTO);
            return res;
        }
        [HttpPut]
        public async Task<string> Update(int Id,[FromForm] SurveyDTO surveyDTO)
        {
            var res = await _surveyServise.Update(Id, surveyDTO);
            return res;
        }
        [HttpDelete]
        public async Task<string> Delete(int Id)
        {
            var res = await _surveyServise.Delete(Id);
            return res;
        }

    }
}
