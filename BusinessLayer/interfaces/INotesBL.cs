﻿using CommonLayer.models;
using RepositoryLayer.entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.interfaces
{
    public interface INotesBL
    {
        public bool AddNotes(Notesmodel notesmodel);
        public string UpdateNote(Notesmodel notesUpdatemodel);
        public IList<Notes> GetNote(long Id);
    }
}
