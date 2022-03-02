using CommonLayer.models;
using RepositoryLayer.entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.interfaces
{
    public interface INotesBL
    {
        public bool AddNotes(Notesmodel notesmodel,long userId);
        public string UpdateNote(Notesmodel notesUpdatemodel, long userId);
        public List<Notes> GetAllNotes(long UserId);
        public List<Notes> GetNote(long NoteId);
        public string DeleteNote(long NoteId);
        public string Archive(long NoteId);
        public string UnArchive(long NoteId);
        public string Pin(long NoteId);
        public string UnPin(long NoteId);
        public string Trash(long NoteId);

    }
}
