﻿using CommonLayer.models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.context;
using RepositoryLayer.entities;
using RepositoryLayer.interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.services
{
    /// <summary>
    /// To store Data in DB
    /// </summary>
    public class UserRL : IUserRL
    {
        private readonly FundoContext fundoContext; //context class is used to query or save data to the database.
        IConfiguration _Appsettings;  //IConfiguration interface is used to read Settings and Connection Strings from AppSettings.
        public UserRL(FundoContext fundoContext, IConfiguration Appsettings)
        {
            this.fundoContext = fundoContext;
            _Appsettings = Appsettings;
        }
        /// <summary>
        /// Registration
        /// </summary>
        /// <param name="userRegmodel"></param>
        /// <returns></returns>
        public User Registration(UserRegmodel userRegmodel) // User is the entitiy of RL.
        {
            try
            {
                User newUser = new User(); // instance created.
                newUser.FristName = userRegmodel.FirstName; // line 35 - 38 calling the registration model class to get and set the values.
                newUser.LastName = userRegmodel.LastName;
                newUser.Email = userRegmodel.Email;
                newUser.Password = userRegmodel.Password;
                newUser.CreatedAt = DateTime.Now;
                newUser.ModifiedAt = DateTime.Now;
                fundoContext.UserTables.Add(newUser); // Add a user in the DB.
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
        /// All Registerd Login Data
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public LoginResponseModel UserLogin(UserLoginmodel info) // Method for Login.
        {
            try
            {
                var Enteredlogin = this.fundoContext.UserTables.Where(X => X.Email == info.Email && X.Password == info.Password).FirstOrDefault();
                // Above line is for Selecting User from a table with LINQ statements/expression
                if (Enteredlogin !=null)
                {
                    LoginResponseModel data = new LoginResponseModel(); // instance created for login response model class.
                    string token = GenerateSecurityToken(Enteredlogin.Email , Enteredlogin.Id); // method for token creation.
                    data.Id = Enteredlogin.Id; // line 70 - 75 is for calling of the model class.
                    data.FirstName = Enteredlogin.FristName;
                    data.LastName = Enteredlogin.LastName;
                    data.Email = Enteredlogin.Email;
                    data.Password = Enteredlogin.Password;
                    data.Token = token;
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

        /// <summary>
        /// Token Created
        /// </summary>
        /// <param name="Email"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        private string GenerateSecurityToken(string Email,long Id) //Method for token Generation.
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Appsettings["Jwt:SecKey"])); // Adding a securiy key in appsettings.json
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256); // identity model for security.
            var claims = new[] {
                new Claim(ClaimTypes.Email,Email),// Access Claim values in controller.
                new Claim("Id",Id.ToString())
            };
            var token = new JwtSecurityToken(_Appsettings["Jwt:Issuer"], // we specify the values for the issuer, security key.
              _Appsettings["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(60), // time for the token to be active.
              signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
        /// <summary>
        /// Password Forgot method
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        public string ForgotPassword(string Email) // Method for Forgotpassword.
        {
            try
            {
                var Enteredlogin = this.fundoContext.UserTables.Where(X => X.Email == Email).FirstOrDefault(); //selecting Email from a table in DB.
                if (Enteredlogin != null)
                {
                    var token = GenerateSecurityToken(Email, Enteredlogin.Id); // To create a token for Authorization.
                    new MSMQmodel().MSMQSender(token); //Message Oriented Middleware that communicate using queues.
                    return token;
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
        /// Reset method
        /// </summary>
        /// <param name="Email"></param>
        /// <param name="Password"></param>
        /// <param name="ConfirmPassword"></param>
        /// <returns></returns>
        public bool ResetPassword(String Email , string Password , String ConfirmPassword) // method for Reseting the password for user registerd in DB.
        {
            try
            {
                if (Password.Equals(ConfirmPassword)) // comparing of passwords.
                {
                    User user = fundoContext.UserTables.Where(e => e.Email == Email).FirstOrDefault(); // selecting the email from DB to change the password.
                    user.Password = ConfirmPassword; // Given password should be as confirmed one.
                    fundoContext.SaveChanges(); // Store the Data entered.
                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
