using SchoolApp.Data;
using System;
using System.Collections.Generic;

namespace SchoolApp.Service.RegistrationService
{
  public  interface IRegistrationServices
    {
        bool GetUser(string userName, String password);
       
        UserRegistration SavePresenter(UserRegistration user);
        UserRegistration GetUserRegisterbyId(Guid Id);
        UserRegistration UpdateuserRegister(UserRegistration userRegister);
        bool isuserexist(string userName);
        UserRegistration GetUserRegisterRole(string userName);
        List<UserRegistration> GetAllUser(Guid Id);
        string GetUserName(Guid Id);
        Guid GetUserIdByEmail(string email);
    }
}

    

