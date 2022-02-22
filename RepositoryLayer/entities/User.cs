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
        public string FristName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        //public string PhoneNumber { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string Email { get; set; }
    }
}
