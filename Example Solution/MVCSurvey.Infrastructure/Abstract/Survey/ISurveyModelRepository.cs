using System.Collections.Generic;
using System.Linq;
using MVCSurvey.Infrastructure.Models.Survey;

namespace MVCSurvey.Infrastructure.Abstract.Survey
{
    public interface ISurveyModelRepository
    {
        IQueryable<SurveyModel> GetAll();
        SurveyModel Find(long id);
        void Delete(SurveyModel survey);
        void Delete(long id);
        long Save(SurveyModel survey);
        void Update(SurveyModel survey);
        IQueryable<SurveyModel> GetAllActive();
        IEnumerable<int> GetDistinctUserIdsFromInstances(long surveyModelId);
        void SetContext(object context);
    }
}
