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
        private readonly IUserRL userRL;
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

        string IUserBL.Login(UserLoginmodel userLogin)
        {
            try
            {
                return userRL.Login(userLogin);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
