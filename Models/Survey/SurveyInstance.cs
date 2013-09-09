using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCSurvey.Infrastructure.Models.Survey
{
  public class SurveyInstance
  {

      public SurveyInstance()
      {
          KeyValues = new List<SurveyKeyValue>();
      }

      [Key]
      public long SurveyInstanceID { get; set; }
      public DateTime DateTaken { get; set; }
      public virtual ICollection<SurveyKeyValue> KeyValues { get; private set; }
      public virtual int UserId { get; private set; }
  }
}