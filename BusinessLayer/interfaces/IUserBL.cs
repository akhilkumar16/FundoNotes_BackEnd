using CommonLayer.models;
using RepositoryLayer.entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.interfaces
{
    public interface IUserBL
    {
        public User Registration(UserRegmodel userRegmodel);
        //public string Login(UserLoginmodel userLogin);
        public LoginResponseModel UserLogin(UserLoginmodel info);

    }
}
