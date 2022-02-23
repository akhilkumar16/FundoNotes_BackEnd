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
        /// userLogin for Login 
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        //public string Login(UserLoginmodel userLogin);
        /// <summary>
        /// UserLogin for All login details
        /// </summary>
        /// <param name="user1"></param>
        /// <returns></returns>
        public LoginResponseModel UserLogin(UserLoginmodel info);

    }
}
