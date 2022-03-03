using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.entities
{
    public class Collaborator
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CollId { get; set; }
        public string CollEmail { get; set; }
        [ForeignKey("Note")]
        public long NoteId { get; set; }
        public virtual Notes note { get; set; }
        [ForeignKey("user")]
        public long UserId { get; set; }
        public virtual User user { get; set; }
    }
}
