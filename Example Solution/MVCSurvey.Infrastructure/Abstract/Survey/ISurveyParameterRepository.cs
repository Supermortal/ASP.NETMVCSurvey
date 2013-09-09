using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCSurvey.Infrastructure.Abstract.Survey
{
    public interface ISurveyParameterRepository
    {
        IQueryable<Models.Survey.SurveyParameter> GetAll();
        Models.Survey.SurveyParameter Find(long id);
        void Delete(Models.Survey.SurveyParameter surveyParameter);
        void Delete(long id);
        long Save(Models.Survey.SurveyParameter surveyParameter);
        void Update(Models.Survey.SurveyParameter surveyParameter);
        void SetContext(object context);
    }
}
