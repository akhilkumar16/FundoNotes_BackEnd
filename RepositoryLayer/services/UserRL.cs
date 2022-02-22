using CommonLayer.models;
using RepositoryLayer.context;
using RepositoryLayer.entities;
using RepositoryLayer.interfaces;
using System;
using System.Collections.Generic;
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
    }
}
