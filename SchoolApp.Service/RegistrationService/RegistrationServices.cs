using SchoolApp.Data;
using SchoolApp.Repo;
using System;
using System.Collections.Generic;
using System.Linq;


namespace SchoolApp.Service.RegistrationService
{
    
        public class RegistrationServices : IRegistrationServices
    {

            private readonly IRepository<UserRegistration> _repoUserRegistration;

            public RegistrationServices(IRepository<UserRegistration> repoUserRegistration)
            {
            _repoUserRegistration = repoUserRegistration;
            }

            public bool GetUser(string email, string password)
            {
                var db = _repoUserRegistration.Query().Filter(x => x.EmailId == email && x.Password == password).Get().Any();
                return db;
            }

            public bool isuserexist(string email)
            {
                var db = _repoUserRegistration.Query().Filter(x => x.EmailId == email).Get().Any();
                return db;
            }
            public UserRegistration SavePresenter(UserRegistration user)
            {
            _repoUserRegistration.Insert(user);
                return user;
            }

            public UserRegistration GetUserRegisterbyId(Guid Id)
            {
                return _repoUserRegistration.FindById(Id);
            }

            public UserRegistration UpdateuserRegister(UserRegistration userRegister)
            {

            _repoUserRegistration.Update(userRegister);
                return userRegister;

            }

            public UserRegistration GetUserRegisterRole(string email)
            {
                return _repoUserRegistration.Query().Filter(x => x.EmailId == email).Get().FirstOrDefault();
            }

            public List<UserRegistration> GetAllUser(Guid Id)
            {
            return _repoUserRegistration.Query().Filter(x => x.UserId != Id).Get().ToList();
            }

            public string GetUserName(Guid Id)
            {
                return _repoUserRegistration.Query().Filter(x => x.UserId == Id).Get().FirstOrDefault().FirstName;
            }

            public List<UserRegistration> GetSenderName()
            {
                return _repoUserRegistration.Query().Get().ToList();
            }
            public Guid GetUserIdByEmail(string emailId)
            {
                return _repoUserRegistration.Query().Filter(x => x.EmailId == emailId).Get().FirstOrDefault().UserId;
            }

    }
    }



