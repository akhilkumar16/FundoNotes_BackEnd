using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.models
{
    public class Notesmodel
    {
        public string Title { get; set; }
        public string Discription { get; set; }
        public string Image { get; set; }
        public string Backgroundcolour { get; set; }
        public string Color { get; set; }
        public bool Delete { get; set; }
        public bool Pin { get; set; }
        public bool Archive { get; set; }
        public DateTime? Reminder { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
