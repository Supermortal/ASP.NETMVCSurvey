using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MVCSurvey.Infrastructure.Abstract.Survey;
using MVCSurvey.Infrastructure.Models;
using MVCSurvey.Infrastructure.Models.Survey;

namespace MVCSurvey.Infrastructure.Concrete.Survey
{
  public class EFSurveyModelRepository : ISurveyModelRepository 
  {

    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger
            (typeof(EFSurveyModelRepository));

    private EFContext db = new EFContext();

    public IQueryable<Models.Survey.SurveyModel> GetAll()
    {
      try
      {
          return db.SurveyModels;
      }
      catch (Exception ex)
      {
          Log.Error(ex.Message, ex);
          return null;
      }
    }

    public Models.Survey.SurveyModel Find(long id)
    {
        try
        {
            return db.SurveyModels.Find(id);
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message, ex);
            return null;
        }
    }

    public void Delete(Models.Survey.SurveyModel survey)
    {
        try
        {
            db.Database.ExecuteSqlCommand("DELETE FROM SurveyParameters WHERE SurveyModel_SurveyID = {0}",
                                              new object[] { survey.SurveyID });

            foreach (var surveyInstance in survey.SurveyInstances)
            {
                db.Database.ExecuteSqlCommand("DELETE FROM SurveyKeyValues WHERE SurveyInstance_SurveyInstanceID = {0}",
                                              new object[] { surveyInstance.SurveyInstanceID });
            }


            db.SurveyModels.Remove(survey);
            db.SaveChanges();

            db.Database.ExecuteSqlCommand("DELETE FROM SurveyInstances WHERE SurveyModel_SurveyID IS NULL");
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
            var survey = db.SurveyModels.Find(id);

            if (survey != null)
            {
                db.Database.ExecuteSqlCommand("DELETE FROM SurveyParameters WHERE SurveyModel_SurveyID = {0}",
                                              new object[] { id });

                foreach (var surveyInstance in survey.SurveyInstances)
                {
                    db.Database.ExecuteSqlCommand("DELETE FROM SurveyKeyValues WHERE SurveyInstance_SurveyInstanceID = {0}",
                                                  new object[] { surveyInstance.SurveyInstanceID });
                }


                db.SurveyModels.Remove(survey);
                db.SaveChanges();

                db.Database.ExecuteSqlCommand("DELETE FROM SurveyInstances WHERE SurveyModel_SurveyID IS NULL");
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message, ex);
        }
    }

    public long Save(Models.Survey.SurveyModel survey)
    {
        try
        {
            db.SurveyModels.Add(survey);
            db.SaveChanges();

            return survey.SurveyID;
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message, ex);
            return -1;
        }
    }

    public void Update(Models.Survey.SurveyModel survey)
    {
        try
        {
            db.Entry(survey).State = EntityState.Modified;
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message, ex);
        }
    }

      public IQueryable<SurveyModel> GetAllActive()
      {
          try
          {
              return db.SurveyModels.Where(sm => sm.Active);
          }
          catch (Exception ex)
          {
              Log.Error(ex.Message, ex);
              return null;
          }
      }

      public IEnumerable<int> GetDistinctUserIdsFromInstances(long surveyModelId)
      {
          try
          {
              return db.SurveyModels.Find(surveyModelId).SurveyInstances.Select(si => si.UserId).Distinct();
          }
          catch (Exception ex)
          {
              Log.Error(ex.Message, ex);
              return null;
          }
      }

      public void SetContext(object context)
    {
        db = (EFContext) context;
    }

  }
}