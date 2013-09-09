using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVCSurvey.Infrastructure.Models.Survey;

namespace MVCSurvey.Infrastructure.Abstract.Survey
{
    public interface ISurveyService
    {
        IQueryable<SurveyModel> GetAllSurveys();
        void CreateSurvey(string surveyName, SurveyParameter[] surveyParameters);
        void DeleteSurvey(long surveyModelId);
        SurveyModel FindSurvey(long surveyModelId);
        void EditSurvey(long surveyModelId, SurveyParameter[] surveyParameters);
        IQueryable<SurveyModel> GetActiveSurveys();
        SurveyInstance CreateSurveyInstance(SurveyKeyValue[] keyValues, long surveyModelId);
        SurveyReviewViewModel CreateSurveyReviewViewModel(long surveyModelId, Dictionary<int, string> userIdToNames);
        SurveyInstanceReviewViewModel CreateSurveyInstanceReviewViewModel(long surveyInstanceId, string userName, string surveyName, long surveyModelId);
        void ActivateSurvey(bool active, long surveyModelId);
        void DeleteSurveyInstance(long surveyInstanceId);
        IEnumerable<int> GetDistinctUserIdsFromInstances(long surveyModelId);
    }
}
