using BusinessLayer.interfaces;
using CommonLayer.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Fundonotes.Controllers
{
    [Route("api/[controller]")] // Route is for matching incoming HTTP requests.
    [ApiController] // to enable Routing Requirements.
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL; // can only be assigned a value from within the constructor(s) of a class.
        public UserController(IUserBL userBL)
        {
            this.userBL = userBL; 
        }
        [HttpPost("Register")] // POST is to send and receive data.
        public IActionResult addUser(UserRegmodel userRegmodel) //IActionResult lets you return both data and HTTP codes.
        {
            try
            {
                var result = userBL.Registration(userRegmodel);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Registration Successful", data = result }); // message
                }
                else
                    return this.BadRequest(new { success = false, message = "Registration Unsuccessful" });
            }
            catch (Exception)
            {

                throw;
            }
        }
       
        [HttpPost("Login")] // POST is to send and receive data.
        public IActionResult UserLogin(UserLoginmodel logindata) //IActionResult lets you return both data and HTTP codes.
        {
            try
            {
                var result = userBL.UserLogin(logindata);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Login Successful", data = result }); // message.
                }
                else
                    return this.BadRequest(new { success = false, message = "Login Unsuccessful" });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost("ForgotPassword")] // to send data .
        public IActionResult ForgotPassword(string Email)  //IActionResult lets you return both data and HTTP codes.
        {
            try
            {
                var result = userBL.ForgotPassword(Email);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Link sent" }); // message.
                }
                else
                    return this.BadRequest(new { success = false, message = " Oops!!!Could not find Email" }); // message.
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize] //user to grant and restrict permissions on Web pages.
        [HttpPost("ResetPassword")] // To send data .
        public IActionResult ResetPassword( string Password, string ConfirmPassword) //IActionResult lets you return both data and HTTP codes.
        {
            try
            {
                //var Email = User.Claims.First(e => e.Type == "Email").Value;
                var Email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                var result = userBL.ResetPassword(Email,Password,ConfirmPassword);
                return this.Ok(new { success = true, message = "yaah!!! ResetPassword Done " });
            }
            catch (Exception)
            {
                return this.BadRequest(new { success = false, message = "Oops!!! Could not find Email" });
            }
        }
    }
}
