using CommonLayer.models;
using RepositoryLayer.entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.interfaces
{
    public interface ICollaboratorBL
    {
        public bool AddCollaboratorToNotes(Collaboratormodel collaborator);
        public string DeleteCollaborator(long NoteId);

    }
}
