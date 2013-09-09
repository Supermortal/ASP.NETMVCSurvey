using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCSurvey.Infrastructure.Abstract.Survey
{
    public interface ISurveyParameterMemberRepository
    {
        IQueryable<Models.Survey.SurveyParameterMember> GetAll();
        Models.Survey.SurveyParameterMember Find(long id);
        void Delete(Models.Survey.SurveyParameterMember surveyParameterMember);
        void Delete(long id);
        long Save(Models.Survey.SurveyParameterMember surveyParameterMember);
        void Update(Models.Survey.SurveyParameterMember surveyParameterMember);
        void SetContext(object context);
    }
}
