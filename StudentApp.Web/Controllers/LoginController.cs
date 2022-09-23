using SchoolApp.Core;
using SchoolApp.Data;
using SchoolApp.Service.RegistrationService;
using SchoolApp.Web.Code.Serialization;
using SchoolApp.Web.Model;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SchoolApp.Web.Controllers
{
    public class LoginController : BaseController
    {
        #region [ SERVICE IMPLEMENTATION ]
        private readonly IRegistrationServices _registrationService;
        #endregion [ SERVICE IMPLEMENTATION ]

        #region [ CONSTRUCTOR ]
        public LoginController(IRegistrationServices registrationService)
        {
            _registrationService = registrationService;

        }
        #endregion [ CONSTRUCTOR ]

        #region [ SIGNIN ]

        [ChildActionOnly]
        public ActionResult SignIn()
        {
            LoginViewModel model = new LoginViewModel();
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult LoginUser(string Email, string Password)
        {
            LoginViewModel model = new LoginViewModel();
            model.Email = Email;
            model.Password = Password;
            var Redirecturl = "";

            if (ModelState.IsValid)
            {
                var user = _registrationService.GetUserRegisterRole(model.Email);

                if (user != null && !PasswordEncryption.PasswordsMatch(user.Password, model.Password, user.SaltKey))
                {
                    //Redirect user as per user role
                    string actionName = string.Empty, controllerName = string.Empty;
                    if (user.RoleId != null && user.RoleId>0)
                    {
                        if (user.RoleId == (byte)UserRoles.Admin)
                        {
                            actionName = "dashboard";
                            controllerName = "admin";


                        }
                        else if (user.RoleId == (byte)UserRoles.Faculty)
                        {

                            {
                                actionName = "dashboard";
                                controllerName = "faculty";

                            }

                        }
                        else if (user.RoleId == (byte)UserRoles.Student)
                        {

                            {
                                actionName = "dashboard";
                                controllerName = "student";

                            }

                        }

                        if (Redirecturl != "")
                            return NewtonSoftJsonResult(new RequestOutcome<dynamic> { ReturnParameter = user.UserId.ToString(), RedirectUrl = Redirecturl });
                        else
                            return NewtonSoftJsonResult(new RequestOutcome<dynamic> { ReturnParameter = user.UserId.ToString(), RedirectUrl = @Url.Action(actionName, controllerName) });
                    }
                    else
                    {
                        if (Redirecturl != "")
                            return NewtonSoftJsonResult(new RequestOutcome<dynamic> { ReturnParameter = user.UserId.ToString(), RedirectUrl = Redirecturl });
                        else
                            return NewtonSoftJsonResult(new RequestOutcome<dynamic> { ReturnParameter = user.UserId.ToString(), RedirectUrl = @Url.Action("signn", "login") });
                    }

                    //}


                }
                else
                {
                    ShowErrorMessage("Error", "Email id or password is incorrect.", false, true);
                }
            }
            return NewtonSoftJsonResult(new RequestOutcome<dynamic> { ReturnParameter = null, RedirectUrl = @Url.Action("signn", "login") });


        }


        /// <summary>
        /// Sign In User
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult LoginUserFinalCall(string Email, string Password)
        {
            LoginViewModel model = new LoginViewModel();
            model.Email = Email;
            model.Password = Password;
            var Redirecturl = "";

            if (CurrentUser != null)
            {
                RemoveAuthentication();
            }

            if (ModelState.IsValid)
            {
                var user = _registrationService.GetUserRegisterRole(model.Email);

                if (user != null && !PasswordEncryption.PasswordsMatch(user.Password, model.Password, user.SaltKey))
                {
                    //Redirect user as per user role
                    string actionName = string.Empty, controllerName = string.Empty;
                    if (user.RoleId != null && user.RoleId>0)
                    {
                        if (user.RoleId == (byte)UserRoles.Admin)
                        {
                            actionName = "dashboard";
                            controllerName = "admin";


                        }
                        else if (user.RoleId == (byte)UserRoles.Faculty)
                        {

                            {
                                actionName = "dashboard";
                                controllerName = "faculty";

                            }

                        }
                        else if (user.RoleId == (byte)UserRoles.Student)
                        {

                            {
                                actionName = "dashboard";
                                controllerName = "student";

                            }

                        }

                        if (Redirecturl != "")
                            return NewtonSoftJsonResult(new RequestOutcome<dynamic> { Message = user.UserId.ToString(), RedirectUrl = Redirecturl });
                        else
                            return NewtonSoftJsonResult(new RequestOutcome<dynamic> { Message = user.UserId.ToString(), RedirectUrl = @Url.Action(actionName, controllerName) });
                    }
                    else
                    {
                        if (Redirecturl != "")
                            return NewtonSoftJsonResult(new RequestOutcome<dynamic> { RedirectUrl = Redirecturl });
                        else
                            return NewtonSoftJsonResult(new RequestOutcome<dynamic> { RedirectUrl = @Url.Action("signIn", "login") });
                    }
                }
                else
                {
                    ShowErrorMessage("Error", "Email id or password is incorrect.", false);
                }
            }


            if (Redirecturl != "")
                return NewtonSoftJsonResult(new RequestOutcome<dynamic> { RedirectUrl = Redirecturl });
            else
                return NewtonSoftJsonResult(new RequestOutcome<dynamic> { RedirectUrl = @Url.Action("signIn", "login") });
        }

        #endregion [ SIGNIN ]

    }
}
        
    
