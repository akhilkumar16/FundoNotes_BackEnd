﻿using BusinessLayer.interfaces;
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

        public IList<Notes> GetNote(long Id)
        {
            return notesRL.GetNote(Id);
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
