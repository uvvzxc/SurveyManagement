namespace SurveyManagementProject.Domain.Entities.Exceptions
{
    public class SurveyNotFoundException : Exception
    {
        public SurveyNotFoundException(string message) : base(message) { }

    }
}
