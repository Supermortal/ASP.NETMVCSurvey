namespace MVCSurvey.Infrastructure.Models.Survey
{
    public class SurveyInstanceReviewViewModel
    {
        public string UserName { get; set; }
        public string SurveyName { get; set; }
        public long SurveyModelID { get; set; }
        public SurveyInstance SurveyInstance { get; set; }
    }
}