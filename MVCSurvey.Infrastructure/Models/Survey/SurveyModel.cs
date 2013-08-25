using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCSurvey.Infrastructure.Models.Survey
{
  public class SurveyModel
  {

      public SurveyModel()
      {
          SurveyParameters = new List<SurveyParameter>();
          SurveyInstances = new List<SurveyInstance>();
          Active = false;
      }

      [Key]
      public long SurveyID { get; set; }
      public string Name { get; set; }
      public DateTime DateCreated { get; set; }
      public bool Active { get; set; }
      public virtual ICollection<SurveyParameter> SurveyParameters { get; private set; }
      public virtual ICollection<SurveyInstance> SurveyInstances { get; private set; } 
  }
}