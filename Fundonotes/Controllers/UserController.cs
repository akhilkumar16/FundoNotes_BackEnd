using BusinessLayer.interfaces;
using CommonLayer.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fundonotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL;
        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }
        [HttpPost("Register")]
        public IActionResult addUser(UserRegmodel userRegmodel)
        {
            try
            {
                var result = userBL.Registration(userRegmodel);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Registration Successful", data = result });
                }
                else
                    return this.BadRequest(new { success = false, message = "Registration Unsuccessful" });
            }
            catch (Exception)
            {

                throw;
            }
        }
       
        [HttpPost("Login")]
        public IActionResult UserLogin(UserLoginmodel logindata)
        {
            try
            {
                var result = userBL.UserLogin(logindata);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Login Successful", data = result });
                }
                else
                    return this.BadRequest(new { success = false, message = "Login Unsuccessful" });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost("ForgotPassword")]
        public IActionResult ForgotPassword(string Email)
        {
            try
            {
                var result = userBL.ForgotPassword(Email);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Link sent" });
                }
                else
                    return this.BadRequest(new { success = false, message = " Oops!!!Could not find Email" });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpPost("ResetPassword")]
        public IActionResult ResetPassword( string Password, string ConfirmPassword )
        {
            try
            {
                var Email = User.Claims.First(e => e.Type == "Email").Value;
                var result = userBL.ResetPassword(Email,Password,ConfirmPassword);
                return this.Ok(new { success = true, message = "ResetPassword Link sent" });
            }
            catch (Exception)
            {
                return this.BadRequest(new { success = false, message = "Oops!!!Could not find Email" });
            }
        }
    }
}
