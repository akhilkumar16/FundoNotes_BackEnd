using BusinessLayer.interfaces;
using CommonLayer.models;
using RepositoryLayer.entities;
using RepositoryLayer.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.services
{
    public class CollaboratorBL : ICollaboratorBL
    {
        private readonly ICollaboratorRL collaboratorRL;
        public CollaboratorBL(ICollaboratorRL collaboratorRL)
        {
            this.collaboratorRL = collaboratorRL;
        }

        public bool AddCollaboratorToNotes(Collaboratormodel collaborator)
        {
            try
            {
                return collaboratorRL.AddCollaboratorToNotes(collaborator);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string DeleteCollaborator(long NoteId)
        {
            try
            {
                return collaboratorRL.DeleteCollaborator(NoteId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Collaborator> GetNote(long NoteId)
        {
            try
            {
                return collaboratorRL.GetNote(NoteId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
