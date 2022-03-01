using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepositoryLayer.entities
{
    public class User
    {
        [Key] // DataAnnotation for setting the primary Key value.
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Generaters the vallues for the database Id's.
        public long Id { get; set; }
        [Required]
        [RegularExpression(@"^[A-Z]{1}[a-z]{3}$")] // Name in Specified format.
        public string FristName { get; set; }
        [Required]
        [RegularExpression(@"^[A-Z]{1}[a-z]{3}$")] // Last name in given format.
        public string LastName { get; set; }
        [Required]
        [RegularExpression(@"^[a-c]{3}[.+-_]{0,1}[x-z]{3}@[a-z]{2}[.+-]{0,1}[a-z]{2}[.+-]{0,1}[a-z]{2}$")] // Email should be as per regex statement.
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^((?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*-_.])(?=.{8,}))")] // Password should atleast one caps , expression , number.
        public string Password { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public Notes Notes { get; set; }

    }
}
