using BusinessLayer.interfaces;
using CloudinaryDotNet.Actions;
using CommonLayer.models;
using Microsoft.AspNetCore.Http;
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

        public bool AddNotes(Notesmodel notesmodel,long userId)
        {
            try
            {
                return notesRL.AddNotes(notesmodel,userId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string Archive(long NoteId)
        {
            try
            {
                return notesRL.Archive(NoteId);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public string Color(long NoteId, string addcolor)
        {
            try
            {
                return notesRL.Color(NoteId, addcolor);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string DeleteNote(long NoteId)
        {
            try
            {
                return notesRL.DeleteNote(NoteId);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Notes> GetAllNotes(long userId)
        {
            try
            {
                return notesRL.GetAllNotes(userId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Notes> GetNote(long NoteId)
        {
            try
            {
                return notesRL.GetNote(NoteId);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Image(IFormFile imageURL, long NoteId)
        {
            try
            {
                return notesRL.Image(imageURL, NoteId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string Pin(long NoteId)
        {
            try
            {
                return notesRL.Pin(NoteId);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public string Trash(long NoteId)
        {
            try
            {
                return notesRL.Trash(NoteId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string UnArchive(long NoteId)
        {
            try
            {
                return notesRL.UnArchive(NoteId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string UnPin(long NoteId)
        {
            try
            {
                return notesRL.UnPin(NoteId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string UpdateNote(Notesmodel notesUpdatemodel, long userId)
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
