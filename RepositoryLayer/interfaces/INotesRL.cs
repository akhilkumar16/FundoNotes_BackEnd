using CloudinaryDotNet.Actions;
using CommonLayer.models;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.interfaces
{
    public interface INotesRL
    {
        public bool CreateNote(Notesmodel noteModel, long userid);
        public IEnumerable<Notes> ShowUserNotes(long userid);
        public IEnumerable<Notes> GetIDNote(long noteid);
        public string UpdateNotes(Notes note);
        public bool ArchiveNote(long noteid);
        public bool PinNote(long noteid);
        public bool DeleteNote(long noteid);
        public bool ForeverDeleteNote(long noteid);
        public string AddNoteColor(string color, long noteid);
        public string RemoveNoteColor(long noteid);
        public bool AddNoteBgImage(IFormFile imageURL, long noteid);
        public bool DeleteNoteBgImage(long noteid);
        public IEnumerable<Notes> GetEveryonesNotes();

    }
}
