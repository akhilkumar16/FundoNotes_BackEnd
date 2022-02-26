using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.models
{
    /// <summary>
    /// Model class for registering all the details required to (sign up)
    /// </summary>
    public class UserRegmodel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
