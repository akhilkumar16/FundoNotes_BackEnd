using CommonLayer.models;
using RepositoryLayer.entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.interfaces
{
    public interface ICollaboratorRL
    {
        public bool AddCollaboratorToNotes(Collaboratormodel collaborator);

    }
}
