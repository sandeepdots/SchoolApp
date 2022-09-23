using SchoolApp.Core;
using SchoolApp.Data;
using SchoolApp.Service.FacultyService;
using SchoolApp.Service.RegistrationService;
using SchoolApp.Service.StudentService;
using SchoolApp.Web.Model;
using System;
using System.Web.Mvc;
using System.Net.Mail;
using SchoolApp.Web.Code;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace SchoolApp.Web.Controllers
{
    public class RegistrationController : BaseController
    {
        private readonly IRegistrationServices _RegistrationServices;

        private readonly IFacultyServices _IFacultyServices;
        private readonly IStudentServices _IStudentServices;

        public RegistrationController(IRegistrationServices RegistrationServices, IFacultyServices IFacultyServices, IStudentServices IStudentServices)
        {
            this._RegistrationServices = RegistrationServices;
            this._IFacultyServices = IFacultyServices;
            this._IStudentServices = IStudentServices;
        }


        /// Get method of User Register

        /// <returns> View Model</returns>
        public ActionResult Addregistration()
        {
            RegistrationViewModel userModel = new RegistrationViewModel();
            var enumList = Enum.GetValues(typeof(UserRoles));
            foreach (var item in enumList)
            {
                userModel.RoleList.Add(new SelectListItem
                {
                    Value = ((int)item).ToString(),   //Getting Rolename in dropdown from UserEnum
                    Text = item.GetDescription()
                });
            }
            // ModelState.Remove("UserName");
            return View(userModel);
        }
        /// <summary>
        /// Post method of user Register
        /// </summary>
        /// <param name="userRegister">taking data from user register page using view model</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Addregistration(RegistrationViewModel registrationViewModel)
        {
            try
            {
                registrationViewModel.UserId = Guid.NewGuid();
                registrationViewModel.UserName = registrationViewModel.FirstName + " " + registrationViewModel.LastName;
                bool isexistuser = _RegistrationServices.isuserexist(registrationViewModel.EmailId);
                if (isexistuser)
                {
                    ShowErrorMessage("Error", String.Format("User already exists by this Email,Try using any other Email"), true);

                }
                else
                {
                    ModelState.Remove("UserId");
                    ModelState.Remove("UserName");
                    if (ModelState.IsValid)
                    {
                     
                        //var keyNew = PasswordEncryptHelper.GeneratePassword(10);
                        var password = PasswordEncryptHelper.base64Encode(registrationViewModel.Password);

                        UserRegistration users = new UserRegistration();
                        users.UserId = Guid.NewGuid();
                        users.FirstName = registrationViewModel.FirstName;
                        users.LastName = registrationViewModel.LastName;
                        users.EmailId = registrationViewModel.EmailId;           //Binding data in model from viewmodel,viewmodel data is comeing after clicking submit button
                        users.Password = password;
                        users.UserName = registrationViewModel.UserName;
                        users.Updatedon = DateTime.Now;
                        users.CreatedDate = DateTime.Now;
                        users.IsActive = true;
                        users.IsDeleted = false;
                        users.RoleId = registrationViewModel.RoleId;
                        users.SaltKey = "1";

                        UserRegistration userRegistration = _RegistrationServices.SavePresenter(users);
                       



                        if (userRegistration != null)
                        {
                            if (users.RoleId == (int)UserRoles.Faculty)
                            {
                                FacultyMaster teamLead = new FacultyMaster();
                                teamLead.Faculty_ID = users.UserId;
                                teamLead.FirstName = users.FirstName;
                                teamLead.LastName = users.LastName;
                                teamLead.DepartmentId = (int)Department.Dotnet;
                                teamLead.Phone = "000000";
                                teamLead.Email = registrationViewModel.EmailId;
                                teamLead.Address = "Null";
                                teamLead.Password = password;
                                teamLead.Salary = 45000;
                                teamLead.DOJ = DateTime.Now;
                                teamLead.active = false;
                                _IFacultyServices.SaveFacultyPresenter(teamLead);
                               


                            }

                            else if (users.RoleId == (int)UserRoles.Student)
                            {
                                //string Id = "A99AC473-DDA1-4D53-94AC-A2138B70B0F7";
                                //Guid guid = Guid.Parse(Id);
                                Student student = new Student();
                                student.RollNo = users.UserId;
                                student.Name = users.FirstName + " " + users.LastName;
                                student.Email = registrationViewModel.EmailId;
                                student.Password = password;
                                student.CreatedDate = DateTime.UtcNow;
                                student.UpdatedDate = DateTime.UtcNow;
                                student.IsActive = false;
                                _IStudentServices.SaveStudentPresenter(student);
                            
                            }
                           

                            ExtensionAndCommonCode method = new ExtensionAndCommonCode();

                            List<string> userEmail = new List<string>();

                            userEmail.Add(registrationViewModel.EmailId);

                            MailSenderModel modelEmail = new MailSenderModel
                            {
                                Subject = "School App: Registration Successfully",
                                Body = "Hi User You have successfully registered you account in School App.",
                                ToEmails = userEmail
                            };
                            var resultdata= await method.SendEmail(modelEmail);
                        }


                        string userRegisterId = userRegistration.UserId.ToString();
                        ShowSuccessMessage("Success", String.Format("Registration Done Sucessfully"), false);
                        return RedirectToAction("Addregistration", new { userRegisterId });

                    }
                }

            }
            catch (Exception ex)
            {
                ShowErrorMessage("Error", ex.Message, true);
                return RedirectToAction("Addregistration", "Registration");
            }
            ShowErrorMessage("Error", String.Format("Error Occurd,Try after some time"), true);
            return RedirectToAction("Addregistration", "Registration");




        }

        public JsonResult IsAlreadySignedUpStudent(string EmailId)
        {
            UserRegistration userRegistration = new UserRegistration();

            using (var context = new SchoolAppEntities())
            {
                userRegistration = context.UserRegistrations.Where(a => a.EmailId.ToLower() == EmailId.ToLower()).FirstOrDefault();
            }


            bool status;
            if (userRegistration != null)
            {
                //Already registered  
                status = false;
            }
            else
            {
                //Available to use  
                status = true;
            }

            return Json(status, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Dashboard()
        {
         
            return View();
        }

      




    }
}












