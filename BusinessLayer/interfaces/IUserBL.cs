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
        public LoginResponseModel UserLogin(UserLoginmodel info);

        public string ForgotPassword(string Email);

        public bool ResetPassword(string Email, string Password, string ConfirmPassword);

    }
}
