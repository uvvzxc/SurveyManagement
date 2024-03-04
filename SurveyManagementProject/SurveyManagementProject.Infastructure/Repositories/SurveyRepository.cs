using SurveyManagementProject.Application.Abstractions.IRepositories;
using SurveyManagementProject.Domain.Entities.Models;
using SurveyManagementProject.Infastructure.Persistience;
using SurveyManagementProject.Infastructure.Repositories.BaseRepositories;

namespace SurveyManagementProject.Infastructure.Repositories
{
    public class SurveyRepository : BaseRepository<Survey>, ISurveyRepository
    {
        public SurveyRepository(ApplicationDBContext context) : base(context) { }
    }
}
