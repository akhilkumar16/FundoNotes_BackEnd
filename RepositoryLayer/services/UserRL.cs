using CommonLayer.models;
using RepositoryLayer.context;
using RepositoryLayer.entities;
using RepositoryLayer.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.services
{
    public class UserRL : IUserRL
    {
        private readonly FundoContext fundoContext;
        public UserRL(FundoContext fundoContext)
        {
            this.fundoContext = fundoContext;
        }
        /// <summary>
        /// Registration
        /// </summary>
        /// <param name="userRegmodel"></param>
        /// <returns></returns>
        public User Registration(UserRegmodel userRegmodel)
        {
            try
            {
                User newUser = new User();
                newUser.FristName = userRegmodel.FirstName;
                newUser.LastName = userRegmodel.LastName;
                newUser.Email = userRegmodel.Email;
                newUser.Password = userRegmodel.Password;
                fundoContext.UserTables.Add(newUser);
                int result = fundoContext.SaveChanges();
                if (result > 0)
                {
                    return newUser;
                }
                else
                    return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        public string Login(UserLoginmodel userLogin)
        {
            var LoginResult = this.fundoContext.UserTables.Where(X => X.Email == userLogin.Email && X.Password == userLogin.Password).FirstOrDefault();
            if (LoginResult != null)
            {
                return LoginResult.Email;
            }
            else
                return null;
        }
        /// <summary>
        /// All Registerd Login Data
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public LoginDataModel UserLogin(LoginDataModel info)
        {
            try
            {
                var Enteredlogin = this.fundoContext.UserTables.Where(X => X.Email == info.Email && X.Password == info.Password).FirstOrDefault();
                if (Enteredlogin !=null)
                {
                    LoginDataModel data = new LoginDataModel();
                    data.Id = Enteredlogin.Id;
                    data.FirstName = Enteredlogin.FristName;
                    data.LastName = Enteredlogin.LastName;
                    data.Email = Enteredlogin.Email;
                    data.Password = Enteredlogin.Password;
                    return data;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
