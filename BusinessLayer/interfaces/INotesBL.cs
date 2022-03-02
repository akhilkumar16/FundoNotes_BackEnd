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
        public List<Notes> GetAllNotes();
        public List<Notes> GetNote(int Id);
        public string DeleteNote(int Noteid);

    }
}
