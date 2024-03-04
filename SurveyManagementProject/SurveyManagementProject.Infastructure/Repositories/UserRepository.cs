using SurveyManagementProject.Application.Abstractions.IRepositories;
using SurveyManagementProject.Domain.Entities.Models;
using SurveyManagementProject.Infastructure.Persistience;
using SurveyManagementProject.Infastructure.Repositories.BaseRepositories;

namespace SurveyManagementProject.Infastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDBContext context) : base(context) { }
    }
}
