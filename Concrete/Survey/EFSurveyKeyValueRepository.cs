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
    public class EFSurveyKeyValueRepository : ISurveyKeyValueRepository 
    {

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger
            (typeof(EFSurveyKeyValueRepository));

        private EFContext db = new EFContext();

        public IQueryable<SurveyKeyValue> GetAll()
        {
            try
            {
                return db.SurveyKeyValues;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        public SurveyKeyValue Find(long id)
        {
            try
            {
                return db.SurveyKeyValues.Find(id);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        public void Delete(SurveyKeyValue surveyKeyValue)
        {
            try
            {
                db.Entry(surveyKeyValue).State = EntityState.Deleted;
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
                db.Entry(db.SurveyKeyValues.Find(id)).State = EntityState.Deleted;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
        }

        public long Save(SurveyKeyValue surveyKeyValue)
        {
            try
            {
                db.SurveyKeyValues.Add(surveyKeyValue);
                db.SaveChanges();

                return surveyKeyValue.SurveyKeyValueID;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return -1;
            }
        }

        public void Update(SurveyKeyValue surveyKeyValue)
        {
            try
            {
                db.Entry(surveyKeyValue).State = EntityState.Modified;
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