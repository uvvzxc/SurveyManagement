using Microsoft.EntityFrameworkCore;
using SurveyManagementProject.Domain.Entities.Models;

namespace SurveyManagementProject.Infastructure.Persistience
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        => Database.Migrate();

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Survey> Surveys { get; set; }

    }
}