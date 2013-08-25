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
    public class EFSurveyParameterMemberRepository : ISurveyParameterMemberRepository
    {

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger
            (typeof(EFSurveyParameterMemberRepository));

        private EFContext db = new EFContext();

        public IQueryable<SurveyParameterMember> GetAll()
        {
            try
            {
                return db.SurveyParameterMembers;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        public SurveyParameterMember Find(long id)
        {
            try
            {
                return db.SurveyParameterMembers.Find(id);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        public void Delete(SurveyParameterMember surveyParameterMember)
        {
            try
            {
                db.Entry(surveyParameterMember).State = EntityState.Deleted;
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
                db.Entry(db.SurveyParameterMembers.Find(id)).State = EntityState.Deleted;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
        }

        public long Save(SurveyParameterMember surveyParameterMember)
        {
            try
            {
                db.SurveyParameterMembers.Add(surveyParameterMember);
                db.SaveChanges();

                return surveyParameterMember.SurverParamterMemberID;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return -1;
            }
        }

        public void Update(SurveyParameterMember surveyParameterMember)
        {
            try
            {
                db.Entry(surveyParameterMember).State = EntityState.Modified;
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