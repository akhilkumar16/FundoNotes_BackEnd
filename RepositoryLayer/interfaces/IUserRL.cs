using CommonLayer.models;
using RepositoryLayer.entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.interfaces
{
   public interface IUserRL
    {
        public User Registration(UserRegmodel userRegmodel);
    }
}
