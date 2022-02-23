using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.models
{
    /// <summary>
    /// Model class for getting all the data of email when we enter (Email and password)
    /// </summary>
    public class UserLoginmodel
    {
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
