using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepositoryLayer.entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required]
        [RegularExpression(@"^[A-Z]{1}[a-z]{3}$")]
        public string FristName { get; set; }
        [Required]
        [RegularExpression(@"^[A-Z]{1}[a-z]{3}$")]
        public string LastName { get; set; }
        [Required]
        [RegularExpression(@"^[a-c]{3}[.+-_]{0,1}[x-z]{3}@[a-z]{2}[.+-]{0,1}[a-z]{2}[.+-]{0,1}[a-z]{2}$")]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^((?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*-_.])(?=.{8,}))")]
        public string Password { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
