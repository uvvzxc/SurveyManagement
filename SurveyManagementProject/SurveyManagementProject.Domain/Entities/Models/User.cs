using System.ComponentModel.DataAnnotations;

namespace SurveyManagementProject.Domain.Entities.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [MinLength(6)]
        public string Password { get; set; }
        public string Role { get; set; }
        public int Score { get; set; }
    }
}
