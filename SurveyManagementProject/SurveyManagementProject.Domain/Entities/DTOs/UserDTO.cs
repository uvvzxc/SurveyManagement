using System.ComponentModel.DataAnnotations;

namespace SurveyManagementProject.Domain.Entities.DTOs
{
    public class UserDTO
    {
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [MinLength(6)]
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
