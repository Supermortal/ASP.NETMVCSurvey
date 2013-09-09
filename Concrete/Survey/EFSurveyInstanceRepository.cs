using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MVCSurvey.Infrastructure.Abstract.Survey;
using MVCSurvey.Infrastructure.Models;
using MVCSurvey.Infrastructure.Models.Survey;

namespace MVCSurvey.Infrastructure.Concrete.Survey
{
    public class EFSurveyInstanceRepository : ISurveyInstanceRepository 
    {

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger
            (typeof(EFSurveyInstanceRepository));

        private EFContext db = new EFContext();

        public IQueryable<SurveyInstance> GetAll()
        {
            try
            {
                return db.SurveyInstances;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        public SurveyInstance Find(long id)
        {
            try
            {
                return db.SurveyInstances.Find(id);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        public void Delete(SurveyInstance surveyInstance)
        {
            try
            {
                db.Entry(surveyInstance).State = EntityState.Deleted;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
        }

        public void Delete(long id)
        {
            try
            {
                db.Entry(db.SurveyInstances.Find(id)).State = EntityState.Deleted;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
        }

        public long Save(SurveyInstance surveyInstance)
        {
            try
            {
                db.SurveyInstances.Add(surveyInstance);
                db.SaveChanges();

                return surveyInstance.SurveyInstanceID;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return -1;
            }
        }

        public void Update(SurveyInstance surveyInstance)
        {
            try
            {
                db.Entry(surveyInstance).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
        }

        public void SetContext(object context)
        {
            db = (EFContext) context;
        }

    }
}