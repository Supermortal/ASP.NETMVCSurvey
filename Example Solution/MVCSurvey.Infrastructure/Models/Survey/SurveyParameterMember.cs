using System.ComponentModel.DataAnnotations;

namespace MVCSurvey.Infrastructure.Models.Survey
{
    public class SurveyParameterMember
    {
        [Key]
        public int SurverParamterMemberID { get; set; }
        public string Type { get; set; }
        public string Key { get; set; }
    }
}