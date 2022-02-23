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
        [Required]
        [RegularExpression(@"^[A-Z]{1}[a-z]{3}$")]
        public string FirstName { get; set; }
        [Required]
        [RegularExpression(@"^[A-Z]{1}[a-z]{3}$")]
        public string LastName { get; set; }
        [Required]
        [RegularExpression(@"^[a-c]{3}[.+-_]{0,1}[x-z]{3}@[a-z]{2}[.+-]{0,1}[a-z]{2}[.+-]{0,1}[a-z]{2}$")]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^((?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*-_.])(?=.{8,}))")]
        public string Password { get; set; }
    }
}
