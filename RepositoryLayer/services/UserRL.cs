using CommonLayer.models;
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
    public class UserRL : IUserRL
    {
        private readonly FundoContext fundoContext;
        IConfiguration _Appsettings;
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
        /// All Registerd Login Data
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public LoginResponseModel UserLogin(UserLoginmodel info)
        {
            try
            {
                var Enteredlogin = this.fundoContext.UserTables.Where(X => X.Email == info.Email && X.Password == info.Password).FirstOrDefault();
                if (Enteredlogin !=null)
                {
                    LoginResponseModel data = new LoginResponseModel();
                    string token = GenerateSecurityToken(Enteredlogin.Email , Enteredlogin.Id);
                    data.Id = Enteredlogin.Id;
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
        private string GenerateSecurityToken(string Email,long Id)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Appsettings["Jwt:SecKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
                new Claim(ClaimTypes.Email,Email),
                new Claim("Id",Id.ToString())
            };
            var token = new JwtSecurityToken(_Appsettings["Jwt:Issuer"],
              _Appsettings["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(60),
              signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
        /// <summary>
        /// Password Forgot method
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        public string ForgotPassword(string Email)
        {
            try
            {
                var Enteredlogin = this.fundoContext.UserTables.Where(X => X.Email == Email).FirstOrDefault();
                if (Enteredlogin != null)
                {
                    var token = GenerateSecurityToken(Email, Enteredlogin.Id);
                    new MSMQmodel().MSMQSender(token);
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
        public bool ResetPassword(String Email , string Password , String ConfirmPassword)
        {
            try
            {
                if (Password.Equals(ConfirmPassword))
                {
                    User user = fundoContext.UserTables.Where(e => e.Email == Email).FirstOrDefault();
                    user.Password = ConfirmPassword;
                    fundoContext.SaveChanges();
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
