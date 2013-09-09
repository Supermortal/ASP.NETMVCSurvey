using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCSurvey.Infrastructure.Models.Survey
{
  public class SurveyParameter
  {

      public SurveyParameter()
      {
          SurveyParameterMembers = new List<SurveyParameterMember>();

          SurveyParamTypes = new string[] { "Text", "Number", "Decimal Number", "Currency", "Date" };
          ParamRequiredChoices = new string[] { "True", "False" };
      }

      [Key]
      public long SurveyParameterID { get; set; }
      public string Type { get; set; }
      public string Key { get; set; }
      public bool Required { get; set; }
      public virtual ICollection<SurveyParameterMember> SurveyParameterMembers { get; private set; }

      public string[] SurveyParamTypes { get; set; }
      public string[] ParamRequiredChoices { get; set; }
  }
}