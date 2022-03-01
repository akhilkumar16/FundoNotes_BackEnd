using CommonLayer.models;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.context;
using RepositoryLayer.entities;
using RepositoryLayer.interfaces;
using System;
using System.Collections.Generic;
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
    }
}
