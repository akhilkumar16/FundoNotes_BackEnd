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
        public string DeleteCollaborator(long NoteId);
        public List<Collaborator> GetNote(long NoteId);

    }
}
