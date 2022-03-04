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
    public class CollaboratorRL : ICollaboratorRL
    {
        public  readonly FundoContext fundoContext;
        public CollaboratorRL(FundoContext fundoContext)
        {
            this.fundoContext = fundoContext;
        }
        public bool AddCollaboratorToNotes(Collaboratormodel collaborator)
        {
            try
            {
                var notestable = this.fundoContext.Notestables.Where(check => check.NoteId == collaborator.NoteId).SingleOrDefault();
                var usertable = this.fundoContext.UserTables.Where(check => check.Email == collaborator.CollEmail).SingleOrDefault();
                if(notestable != null && usertable != null)
                {
                    Collaborator instance = new Collaborator();
                    instance.UserId = usertable.UserId;
                    instance.NoteId = notestable.NoteId;
                    instance.CollEmail = collaborator.CollEmail;
                    this.fundoContext.Colltables.Add(instance);
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
        public string DeleteCollaborator(long NoteId)
        {
            try
            {
                var delete = this.fundoContext.Colltables.Where(del => del.NoteId == NoteId).SingleOrDefault();
                this.fundoContext.Colltables.Remove(delete);
                this.fundoContext.SaveChanges();
                return "Collaborator Deleted Succesfully";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
