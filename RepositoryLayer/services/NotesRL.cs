using CommonLayer.models;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.context;
using RepositoryLayer.entities;
using RepositoryLayer.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.services
{
    public class NotesRL : INotesRL
    {
        private readonly FundoContext fundoContext; //context class is used to query or save data to the database.
        IConfiguration _Toolsettings;  //IConfiguration interface is used to read Settings and Connection Strings from AppSettings.
        public NotesRL(FundoContext fundoContext, IConfiguration Toolsettings)
        {
            this.fundoContext = fundoContext;
            _Toolsettings = Toolsettings;
        }
        public bool AddNotes(Notesmodel notesmodel) 
        {
            try
            {
                Notes fundonotes = new Notes();
                fundonotes.Id = notesmodel.Id;
                fundonotes.Title = notesmodel.Title;
                fundonotes.Discription = notesmodel.Discription;
                fundonotes.Reminder = notesmodel.Reminder;
                fundonotes.Color = notesmodel.Color;
                fundonotes.Image = notesmodel.Image;
                fundonotes.Backgroundcolour = notesmodel.Backgroundcolour;
                fundonotes.Archive = notesmodel.Archive;
                fundonotes.Pin = notesmodel.Pin;
                fundonotes.ModifiedAt = DateTime.Now;
                fundonotes.CreatedAt = DateTime.Now;
                fundoContext.Notestables.Add(fundonotes);
                var result = this.fundoContext.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                    
            }
            catch (Exception)
            { 
                throw;
            }
        }
        public List<Notes> GetAllNotes()
        {
            this.fundoContext.SaveChanges();
            return this.fundoContext.Notestables.ToList();
        }
        public List<Notes> GetNote(int Id)
        {
            var listNote = fundoContext.Notestables.Where(list => list.Id == Id).SingleOrDefault();
            if (listNote != null)
            {
                return fundoContext.Notestables.Where(list => list.Id == Id).ToList();
            }
            return null;
        }

        public string UpdateNote(Notesmodel notesUpdatemodel)
        {
            try
            {
                var result = fundoContext.Notestables.Where(X => X.Id == notesUpdatemodel.Id).SingleOrDefault();
                if (result != null)
                {
                    result.Title = notesUpdatemodel.Title;
                    result.Discription = notesUpdatemodel.Discription;
                    result.ModifiedAt = DateTime.Now;
                    result.Color = notesUpdatemodel.Color;
 
                    this.fundoContext.SaveChanges();
                    return "Modified";
                }
                else
                {
                    return "Not Modified";

                }

            }
            catch (Exception)
            {
                
                throw;
            }
            
        }
        public string DeleteNote(int Noteid)
        {
            var deletenote = fundoContext.Notestables.Where(del => del.Id == Noteid).SingleOrDefault();
            if (deletenote != null)
            {
                fundoContext.Notestables.Remove(deletenote);
                this.fundoContext.SaveChanges();
                return "Note Deleted Successfully";
            }
            else
            {
                return null;
            }
        }
        public string Archieve(long NoteId)
        {
            try
            {
                var archieve = fundoContext.Notestables.Where(Arch => Arch.Id == NoteId).SingleOrDefault();
                if(archieve != null)
                {
                    archieve.Archive = true;
                    this.fundoContext.SaveChanges();
                    return "Notes Archieved";
                }
                else
                {
                    return "Not Archieved";
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}