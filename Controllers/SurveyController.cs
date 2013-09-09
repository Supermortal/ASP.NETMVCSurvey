using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using MVCSurvey.Infrastructure.Abstract.Survey;
using MVCSurvey.Infrastructure.Models;
using MVCSurvey.Infrastructure.Models.Survey;
using WebMatrix.WebData;

namespace MVCSurvey.Web.Controllers
{
    public class SurveyController : Controller
    {

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger
            (typeof(SurveyController));

        private ISurveyService db;
        private EFContext context = new EFContext();

        public SurveyController(ISurveyService ss)
        {
            db = ss;
        }

        //
        // GET: /Survey/

        public ActionResult Index()
        {
            try
            {
                return View(db.GetAllSurveys());
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return HttpNotFound();
            }
        }

        public ActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult Create(string surveyName, SurveyParameter[] surveyParameters)
        {
            try
            {
                db.CreateSurvey(surveyName, surveyParameters);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return new JsonResult() { Data = new { Status = "Error", Message = ex.Message } };
            }

            return new JsonResult() { Data = new { Status = "Ok"}};
        }

        public ActionResult Delete(int surveyModelId)
        {
            try
            {
                db.DeleteSurvey(surveyModelId);

                if (Request.UrlReferrer != null)
                    return Redirect(Request.UrlReferrer.AbsoluteUri);
                else
                    return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return HttpNotFound();
            }
        }

        public ActionResult Edit(long surveyModelId = 0)
        {
            try
            {
                var survey = db.FindSurvey(surveyModelId);

                if (survey == null)
                    return HttpNotFound();

                return View(survey);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult Edit(int surveyModelId, SurveyParameter[] surveyParameters)
        {
            try
            {
                db.EditSurvey(surveyModelId, surveyParameters);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return new JsonResult() { Data = new { Status = "Error", Message = ex.Message } };
            }

            return new JsonResult() { Data = new { Status = "Ok" } };
        }

        public ActionResult TakeASurvey()
        {
            try
            {
                var surveys = db.GetActiveSurveys();

                if (Request.UrlReferrer != null)
                    ViewBag.ReferrerUrl = Request.UrlReferrer.AbsoluteUri;

                return View(surveys);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return HttpNotFound();
            }
        }

        public ActionResult Take(int surveyModelId)
        {
            try
            {
                var survey = db.FindSurvey(surveyModelId);
                ViewBag.ReferrerUrl = Request.UrlReferrer.AbsoluteUri;

                return View(survey);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return HttpNotFound();
            }
        }

        [HttpPost]
        public JsonResult Take(SurveyKeyValue[] keyValues, long surveyModelId)
        {
            try
            {
                //TODO Allow Administrators to set a user id manually

                var surveyInstance = db.CreateSurveyInstance(keyValues, surveyModelId);

                return new JsonResult() {Data = new {Result = "OK"}};
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return new JsonResult() { Data = new { Result = "OK", Message = ex.Message } };
            }
        }

        public ActionResult Review(long surveyModelId)
        {
            try
            {
                var userIds = db.GetDistinctUserIdsFromInstances(surveyModelId);
                var userNames = new Dictionary<int, string>();
                foreach (var userId in userIds)
                {
                    var user = context.UserProfiles.Find(userId);

                    if (user != null)
                        userNames.Add(userId, user.UserName);
                }

                var surveyVM = db.CreateSurveyReviewViewModel(surveyModelId, userNames);
                
                return View(surveyVM);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return HttpNotFound();
            }
        }

        public ActionResult ReviewInstance(long surveyInstanceId, string userName, string surveyName, long surveyModelId)
        {
            try
            {
                var viewModel = db.CreateSurveyInstanceReviewViewModel(surveyInstanceId, userName, surveyName,
                                                                       surveyModelId);

                return View(viewModel);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return HttpNotFound();
            }
        }

        public ActionResult ReviewAllInstances(long surveyModelId)
        {
            try
            {
                var userIds = db.GetDistinctUserIdsFromInstances(surveyModelId);
                var userNames = new Dictionary<int, string>();
                foreach (var userId in userIds)
                {
                    var user = context.UserProfiles.Find(userId);

                    if (user != null)
                        userNames.Add(userId, user.UserName);
                }

                var surveyVM = db.CreateSurveyReviewViewModel(surveyModelId, userNames);

                return View(surveyVM);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return HttpNotFound();
            }
        }

        public ActionResult Activate(long surveyModelId)
        {
            try
            {
                db.ActivateSurvey(!db.FindSurvey(surveyModelId).Active, surveyModelId);

                return Redirect(Request.UrlReferrer.AbsoluteUri);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return HttpNotFound();
            }
        }

        public ActionResult DeleteInstance(long surveyInstanceId)
        {
            try
            {
                db.DeleteSurveyInstance(surveyInstanceId);

                return Redirect(Request.UrlReferrer.AbsoluteUri);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return HttpNotFound();
            }
        }

    }
}
