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
    public class EFSurveyParameterRepository : ISurveyParameterRepository 
    {

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger
            (typeof(EFSurveyParameterRepository));

        private EFContext db = new EFContext();

        public IQueryable<SurveyParameter> GetAll()
        {
            try
            {
                return db.SurveyParameters;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        public SurveyParameter Find(long id)
        {
            try
            {
                return db.SurveyParameters.Find(id);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        public void Delete(SurveyParameter surveyParameter)
        {
            try
            {
                db.Entry(surveyParameter).State = EntityState.Deleted;
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
                db.Entry(db.SurveyParameters.Find(id)).State = EntityState.Deleted;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
        }

        public long Save(SurveyParameter surveyParameter)
        {
            try
            {
                db.SurveyParameters.Add(surveyParameter);
                db.SaveChanges();

                return surveyParameter.SurveyParameterID;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return -1;
            }
        }

        public void Update(SurveyParameter surveyParameter)
        {
            try
            {
                db.Entry(surveyParameter).State = EntityState.Modified;
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