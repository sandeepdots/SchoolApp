using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolApp.Web.Controllers
{
    public class RegistrationController : Controller
    {
        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

      
    }
}