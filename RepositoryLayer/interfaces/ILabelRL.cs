﻿using CommonLayer.models;
using RepositoryLayer.entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.interfaces
{
    public interface ILabelRL
    {
        public string AddLabel(Labelmodel labelmodel);
        public string UpdateLabel(long LabelId, string LabelName);
        public List<Label> GetLabel(long NoteId);
        public List<Label> GetAllLabel();
        public string DeleteLabel(long LabelId);

    }
}
