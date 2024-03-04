namespace SurveyManagementProject.Domain.Entities.Models
{
    public class Survey
    {
        public int SurveyId { get; set; }
        public string SurveyTitle { get; set; }
        public string SurveyQuestion { get; set; }
        public string SurveyAnswer { get; set; }
        public string Author { get; set; }

    }
}
