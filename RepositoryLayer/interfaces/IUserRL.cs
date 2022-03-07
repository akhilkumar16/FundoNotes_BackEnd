using CommonLayer.models;
using RepositoryLayer.entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.interfaces
{
   public interface IUserRL
    {
        public User Registration(UserRegmodel userRegmodel);  // Method for registration.
       
        public LoginResponseModel UserLogin(UserLoginmodel info); // Response model class method.

        public string ForgotPassword(string Email); // For Password method.

        public bool ResetPassword(String Email, string Password, String ConfirmPassword); // Reseting the password.
        public List<User> GetAllUsers();


    }
}
