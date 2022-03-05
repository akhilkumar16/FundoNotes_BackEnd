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
    /// <summary>
    /// collaborator class interface connection
    /// </summary>
    public class CollaboratorRL : ICollaboratorRL
    {
       //context class is used to query or save data to the database.
        public readonly FundoContext fundoContext;
        //Construction 
        public CollaboratorRL(FundoContext fundoContext)
        {
            this.fundoContext = fundoContext;
        }
        /// <summary>
        /// Method for Add colloborator
        /// </summary>
        /// <param name="collaborator"></param>
        /// <returns></returns>
        public bool AddCollaboratorToNotes(Collaboratormodel collaborator)
        {
            try
            {
                //checking of note Id in notesDb
                var notestable = this.fundoContext.Notestables.Where(check => check.NoteId == collaborator.NoteId).SingleOrDefault();
                // checking of Email in userDb
                var usertable = this.fundoContext.UserTables.Where(check => check.Email == collaborator.CollEmail).SingleOrDefault();
                if(notestable != null && usertable != null)
                {
                    //enitity class instance 
                    Collaborator instance = new Collaborator();
                    instance.UserId = usertable.UserId;
                    instance.NoteId = notestable.NoteId;
                    instance.CollEmail = collaborator.CollEmail;
                    this.fundoContext.Colltables.Add(instance); // Add's to collaborator db
                }
                int res = this.fundoContext.SaveChanges();
                if(res > 0)
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

                return false;;
            }
        }
        /// <summary>
        /// Deletes the collaborator 
        /// </summary>
        /// <param name="NoteId"></param>
        /// <returns></returns>
        public string DeleteCollaborator(long NoteId)
        {
            try
            {
                //checking of note Id in Collaborator Db
                var delete = this.fundoContext.Colltables.Where(del => del.NoteId == NoteId).FirstOrDefault();
                if( delete != null)
                {
                    // removes the entry from Db
                    this.fundoContext.Colltables.Remove(delete);
                }
                this.fundoContext.SaveChanges();
                return "Collaborator Deleted Succesfully";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Getting of note by NoteId
        /// </summary>
        /// <param name="NoteId"></param>
        /// <returns></returns>
        public List<Collaborator> GetNote(long NoteId)
        {
            //checking of entries in db
            var listNote = fundoContext.Colltables.Where(list => list.NoteId == NoteId).SingleOrDefault();
            if (listNote != null)
            {
                return fundoContext.Colltables.Where(list => list.NoteId == NoteId).ToList();
            }
            return null ;
        }
    }
}
