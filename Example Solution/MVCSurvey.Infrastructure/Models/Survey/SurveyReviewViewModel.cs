using System.Collections.Generic;

namespace MVCSurvey.Infrastructure.Models.Survey
{
    public class SurveyReviewViewModel
    {
        public SurveyModel Survey { get; set; }
        public Dictionary<int, string> UserNames { get; set; }
    }
}