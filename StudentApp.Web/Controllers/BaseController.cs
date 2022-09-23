using SchoolApp.Web.Code.Serialization;
using System.Linq;
using System.Web.Mvc;
using SchoolApp.Web.Models;
using SchoolApp.Core;
using System.Web.Security;
using SchoolApp.Web.Model;

namespace SchoolApp.Web.Controllers
{
    public class BaseController : Controller
    {
        public CustomPrincipal CurrentUser
        {
            get { return HttpContext.User as CustomPrincipal; }
        }
        public void RemoveAuthentication()
        {
            FormsAuthentication.SignOut();
        }
        public JsonNetResult NewtonSoftJsonResult(object data)
        {
            return new JsonNetResult
            {
                Data = data,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        /// <summary>
        /// Data
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="messageType"></param>
        /// <param name="isCurrentView"></param>
        private void ShowMessages(string title, string message, MessageType messageType, bool isCurrentView, bool isLogin = false)
        {
            Notification model = new Notification
            {
                Heading = title,
                Message = message,
                Type = messageType
            };

            if (isLogin)
            {
                if (isCurrentView)
                    this.ViewData.AddOrReplace("NotificationModelForLogin", model);
                else
                    this.TempData.AddOrReplace("NotificationModelForLogin", model);
            }
            else
            {
                if (isCurrentView)
                    this.ViewData.AddOrReplace("NotificationModel", model);
                else
                    this.TempData.AddOrReplace("NotificationModel", model);
            }
        }

        protected void ShowErrorMessage(string title, string message, bool isCurrentView = true, bool isLogin = false)
        {
            ShowMessages(title, message, MessageType.Danger, isCurrentView, isLogin);
        }

        protected void ShowSuccessMessage(string title, string message, bool isCurrentView = true)
        {
            ShowMessages(title, message, MessageType.Success, isCurrentView);
        }

        protected void ShowWarningMessage(string title, string message, bool isCurrentView = true)
        {
            ShowMessages(title, message, MessageType.Warning, isCurrentView);
        }

        protected void ShowInfoMessage(string title, string message, bool isCurrentView = true)
        {
            ShowMessages(title, message, MessageType.Info, isCurrentView);
        }

        public PartialViewResult CreateModelStateErrors()
        {
            return PartialView("_ValidationSummary", ModelState.Values.SelectMany(x => x.Errors));
        }
    }
}