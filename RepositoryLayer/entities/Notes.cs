using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.entities
{
    public class Notes
    {
        [Key] // DataAnnotation for setting the primary Key value.
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Generaters the vallues for the database Id's.
        // Requried attributes for Taking Note.
        public long NotesId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? ModifiedTime { get; set; }
        public string Remainder { get; set; }
        public string Image { get; set; }
        public bool Archive { get; set; }
        public bool Delete{ get; set; }
        public bool Pin { get; set; }
        public string Background { get; set; }
        public string Color { get; set; }
        // foreign key 
        public User User { get; set; }
    }
}
