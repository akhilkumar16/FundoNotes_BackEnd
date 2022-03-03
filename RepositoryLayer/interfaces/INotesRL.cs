﻿using CloudinaryDotNet.Actions;
using CommonLayer.models;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.interfaces
{
    public interface INotesRL
    {
        public bool AddNotes(Notesmodel notesmodel,long userId);
        public string UpdateNote(Notesmodel notesUpdatemodel);
        public List<Notes> GetAllNotes(long userId);
        public List<Notes> GetNote(long NoteId);
        public string DeleteNote(long NoteId);
        public string Archive(long NoteId);
        public string UnArchive(long NoteId);
        public string Pin(long NoteId);
        public string UnPin(long NoteId);
        public string Trash(long NoteId);
        public string Color(long NoteId, string addcolor);
        public bool Image(IFormFile imageURL, long NoteId);
        public bool DeleteNoteBgImage(long NoteId);


    }
}
