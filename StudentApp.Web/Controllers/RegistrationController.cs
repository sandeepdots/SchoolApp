using SchoolApp.Service.RegistrationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolApp.Web.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly IRegistrationServices _registerationService;
        public RegistrationController(IRegistrationServices userRegisterService)
        {
            this._registerationService = userRegisterService;
        }
        public ActionResult Index()
        {
            UserRegisterVierwModel userModel = new UserRegisterVierwModel();
            var enumList = Enum.GetValues(typeof(UserEnum));
            foreach (var item in enumList)
            {
                userModel.Role.Add(new SelectListItem
                {
                    Value = ((int)item).ToString(),   //Getting Rolename in dropdown from UserEnum
                    Text = item.GetDescription()
                });
            }

            //    [HttpGet]
            //public ActionResult Registration()

            //{
            //    return View();
        }

      
    }
