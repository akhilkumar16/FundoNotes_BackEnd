using BusinessLayer.interfaces;
using CommonLayer.models;
using RepositoryLayer.entities;
using RepositoryLayer.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.services
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL userRL; // readonly can only be assigned a value from within the constructor(s) of a class.
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }
        public User Registration(UserRegmodel userRegmodel)
        {
            try
            {
                return userRL.Registration(userRegmodel);
            }
            catch (Exception)
            {
                throw;
            }
        }
        LoginResponseModel IUserBL.UserLogin(UserLoginmodel user)
        {
            try
            {
                return userRL.UserLogin(user);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string ForgotPassword(string Email)
        {
            try
            {
                return userRL.ForgotPassword(Email);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool ResetPassword(String Email, string Password, String ConfirmPassword)
        {
            try
            {
                return userRL.ResetPassword(Email,Password,ConfirmPassword);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
