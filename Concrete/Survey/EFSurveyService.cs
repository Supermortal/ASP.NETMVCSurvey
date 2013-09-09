using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCSurvey.Infrastructure.Abstract.Survey;
using MVCSurvey.Infrastructure.Models;
using MVCSurvey.Infrastructure.Models.Survey;

namespace MVCSurvey.Infrastructure.Concrete.Survey
{
    public class EFSurveyService : ISurveyService
    {

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger
            (typeof(EFSurveyService));

        private readonly EFContext db = new EFContext();

        private readonly ISurveyModelRepository _smr;
        private readonly ISurveyInstanceRepository _sir;
        //private ISurveyKeyValueRepository _skvr;
        //private ISurveyParameterMemberRepository _spmr;
        //private ISurveyParameterRepository _spr;

        public EFSurveyService(ISurveyModelRepository smr, ISurveyInstanceRepository sir)//, ISurveyKeyValueRepository skvr, ISurveyParameterMemberRepository spmr, ISurveyParameterRepository spr)
        {
            _smr = smr;
            _sir = sir;
            //_skvr = skvr;
            //_spmr = spmr;
            //_spr = spr;
        }

        public IQueryable<SurveyModel> GetAllSurveys()
        {
            try
            {
                return _smr.GetAll();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        public void CreateSurvey(string surveyName, SurveyParameter[] surveyParameters)
        {
            try
            {
                var survey = new SurveyModel();
                survey.Name = surveyName;
                survey.DateCreated = DateTime.Now;

                foreach (var sp in surveyParameters)
                {
                    survey.SurveyParameters.Add(sp);
                }

                _smr.Save(survey);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
        }

        public void DeleteSurvey(long surveyModelId)
        {
            try
            {
                _smr.Delete(surveyModelId);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
        }

        public SurveyModel FindSurvey(long surveyModelId)
        {
            try
            {
                return _smr.Find(surveyModelId);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        public void EditSurvey(long surveyModelId, SurveyParameter[] surveyParameters)
        {
            try
            {
                var survey = _smr.Find(surveyModelId);

                if (survey != null)
                {
                    db.Database.ExecuteSqlCommand("DELETE FROM SurveyParameters WHERE SurveyModel_SurveyID = {0}",
                                                  new object[] {surveyModelId});

                    db.SaveChanges();

                    survey.SurveyParameters.Clear();
                    foreach (var sp in surveyParameters)
                    {
                        survey.SurveyParameters.Add(sp);
                    }
                }

                _smr.Update(survey);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
        }

        public IQueryable<SurveyModel> GetActiveSurveys()
        {
            try
            {
                return _smr.GetAllActive();
            }
            catch (Exception ex) 
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        public SurveyInstance CreateSurveyInstance(SurveyKeyValue[] keyValues, long surveyModelId)
        {
            try
            {
                var surveyInstance = new SurveyInstance();
                surveyInstance.DateTaken = DateTime.Now;
                foreach (var keyValue in keyValues)
                {
                    surveyInstance.KeyValues.Add(keyValue);
                }

                var survey = _smr.Find(surveyModelId);

                if (survey != null)
                {
                    survey.SurveyInstances.Add(surveyInstance);
                    _smr.Update(survey);
                }

                return surveyInstance;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        public SurveyReviewViewModel CreateSurveyReviewViewModel(long surveyModelId, Dictionary<int, string> userIdToNames)
        {
            try
            {
                var survey = _smr.Find(surveyModelId);

                var surveyVM = new SurveyReviewViewModel();
                surveyVM.Survey = survey;
                surveyVM.UserNames = userIdToNames;

                return surveyVM;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        public SurveyInstanceReviewViewModel CreateSurveyInstanceReviewViewModel(long surveyInstanceId, string userName, string surveyName,
                                                                              long surveyModelId)
        {
            try
            {
                var viewModel = new SurveyInstanceReviewViewModel();

                viewModel.UserName = userName;
                viewModel.SurveyName = surveyName;
                viewModel.SurveyInstance = _sir.Find(surveyInstanceId);
                viewModel.SurveyModelID = surveyModelId;

                return viewModel;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        public void ActivateSurvey(bool active, long surveyModelId)
        {
            try
            {
                var survey = _smr.Find(surveyModelId);
                survey.Active = active;

                _smr.Update(survey);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
        }

        public void DeleteSurveyInstance(long surveyInstanceId)
        {
            try
            {
                var surveyInstance = _sir.Find(surveyInstanceId);

                if (surveyInstance != null)
                {
                    db.Database.ExecuteSqlCommand("DELETE FROM SurveyKeyValues WHERE SurveyInstance_SurveyInstanceID = {0}", new object[] { surveyInstanceId });

                    _sir.Update(surveyInstance);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
        }

        public IEnumerable<int> GetDistinctUserIdsFromInstances(long surveyModelId)
        {
            try
            {
                return _smr.GetDistinctUserIdsFromInstances(surveyModelId);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

    }
}