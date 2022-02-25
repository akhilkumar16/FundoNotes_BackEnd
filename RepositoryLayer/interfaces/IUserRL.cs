using CommonLayer.models;
using RepositoryLayer.entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.interfaces
{
   public interface IUserRL
    {
        /// <summary>
        /// userRegmodel for Registration
        /// </summary>
        /// <param name="userRegmodel"></param>
        /// <returns></returns>
        public User Registration(UserRegmodel userRegmodel);
       
        /// <summary>
        /// UserLogin for All login details
        /// </summary>
        /// <param name="user1"></param>
        /// <returns></returns>
        public LoginResponseModel UserLogin(UserLoginmodel info);

        public string ForgotPassword(string Email);

        public bool ResetPassword(String Email, string Password, String ConfirmPassword);


    }
}
