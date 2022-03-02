using BusinessLayer.interfaces;
using CommonLayer.models;
using RepositoryLayer.entities;
using RepositoryLayer.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.services
{
    public class NotesBL : INotesBL
    {
        private readonly INotesRL notesRL; 
        public NotesBL(INotesRL notesRL)
        {
            this.notesRL = notesRL;
        }
        public bool AddNotes(Notesmodel notesmodel)
        {
            try
            {
                return notesRL.AddNotes(notesmodel);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string DeleteNote(int Noteid)
        {
            try
            {
                return notesRL.DeleteNote(Noteid);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Notes> GetAllNotes()
        {
            try
            {
                return notesRL.GetAllNotes();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Notes> GetNote(int Id)
        {
            try
            {
                return notesRL.GetNote(Id);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public string UpdateNote(Notesmodel notesUpdatemodel)
        {
            try
            {
                return notesRL.UpdateNote(notesUpdatemodel);
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
