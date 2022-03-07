﻿using BusinessLayer.interfaces;
using CommonLayer.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RepositoryLayer.context;
using RepositoryLayer.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Fundonotes.Controllers
{
    [Route("api/[controller]")] // Route is for matching incoming HTTP requests.
    [ApiController] // to enable Routing Requirements.
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL; // can only be assigned a value from within the constructor(s) of a class.
        private readonly FundoContext fundocontext;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        public UserController(IUserBL userBL,FundoContext fundocontext,IMemoryCache memoryCache , IDistributedCache distributedCache)
        {
            this.userBL = userBL;
            this.fundocontext = fundocontext;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
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
                    return this.Ok(new { success = true, message = "Login Successful", data = result.Email,result.Token }); // message.
                }
                else
                    return this.BadRequest(new { success = false, message = " Please enter correct password " });
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
        /// <summary>
        /// Cache Memory
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("redis")]
        public async Task<IActionResult> GetAllRedisCache()
        {
            var cacheKey = "AllUsers";
            string serializedAllUsers;
            var AllUsers = new List<User>();
            var redisAllUsers = await distributedCache.GetAsync(cacheKey);
            if (redisAllUsers != null)
            {
                serializedAllUsers = Encoding.UTF8.GetString(redisAllUsers);
                AllUsers = JsonConvert.DeserializeObject<List<User>>(serializedAllUsers);
            }
            else
            {
                AllUsers = await fundocontext.UserTables.ToListAsync();
                serializedAllUsers = JsonConvert.SerializeObject(AllUsers);
                redisAllUsers = Encoding.UTF8.GetBytes(serializedAllUsers);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisAllUsers, options);
            }
            return Ok(AllUsers);
        }
    }
}
