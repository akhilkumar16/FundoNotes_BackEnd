using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommonLayer.models;
using Microsoft.AspNetCore.Http;
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
    public class NotesRL : INotesRL
    {
        private readonly FundoContext fundoContext; //context class is used to query or save data to the database.
        IConfiguration _Toolsettings;  //IConfiguration interface is used to read Settings and Connection Strings from AppSettings.
        public NotesRL(FundoContext fundoContext, IConfiguration Toolsettings)
        {
            this.fundoContext = fundoContext;
            _Toolsettings = Toolsettings;
        }
        public bool AddNotes(Notesmodel notesmodel, long userId)
        {
            try
            {
                Notes fundonotes = new Notes();
                fundonotes.UserId = userId;
                fundonotes.Title = notesmodel.Title;
                fundonotes.Discription = notesmodel.Discription;
                fundonotes.Reminder = notesmodel.Reminder;
                fundonotes.Color = notesmodel.Color;
                fundonotes.Image = notesmodel.Image;
                fundonotes.Backgroundcolour = notesmodel.Backgroundcolour;
                fundonotes.Archive = notesmodel.Archive;
                fundonotes.Pin = notesmodel.Pin;
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
        public List<Notes> GetAllNotes(long userid)
        {
            try
            {
                return this.fundoContext.Notestables.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Notes> GetNote(long NotesId)
        {
            var listNote = fundoContext.Notestables.Where(list => list.NoteId == NotesId).SingleOrDefault();
            if (listNote != null)
            {
                return fundoContext.Notestables.Where(list => list.NoteId == NotesId).ToList();
            }
            return null;
        }

        public string UpdateNote(Notesmodel notesUpdatemodel)
        {
            try
            {
                var result = fundoContext.Notestables.Where(X => X.NoteId == notesUpdatemodel.NotesId).SingleOrDefault();
                if (result != null)
                {
                    result.Title = notesUpdatemodel.Title;
                    result.Discription = notesUpdatemodel.Discription;
                    result.ModifiedAt = DateTime.Now;
                    result.Color = notesUpdatemodel.Color;

                    this.fundoContext.SaveChanges();
                    return "Modified";
                }
                else
                {
                    return "Not Modified";

                }

            }
            catch (Exception)
            {

                throw;
            }

        }
        public string DeleteNote(long NoteId)
        {
            var deletenote = fundoContext.Notestables.Where(del => del.NoteId == NoteId).SingleOrDefault();
            if (deletenote != null)
            {
                fundoContext.Notestables.Remove(deletenote);
                this.fundoContext.SaveChanges();
                return "Notes Deleted Successfully";
            }
            else
            {
                return null;
            }
        }
        public string Archive(long NoteId)
        {
            var result = this.fundoContext.Notestables.Where(arch => arch.NoteId == NoteId).SingleOrDefault();
            if (result != null)
            {
                result.Archive = true;
                this.fundoContext.SaveChanges();
                return "Notes archived";
            }
            else
            {
                return null;
            }
        }
        public string UnArchive(long NoteId)
        {
            var result = this.fundoContext.Notestables.Where(arch => arch.NoteId == NoteId && arch.Archive == true).SingleOrDefault();
            if (result != null)
            {
                result.Archive = false;
                this.fundoContext.SaveChanges();
                return "Notes Unarchived";
            }
            else
            {
                return null;
            }
        }
        public string Pin(long NoteId)
        {
            var result = this.fundoContext.Notestables.Where(pin => pin.NoteId == NoteId).SingleOrDefault();
            if (result != null)
            {
                result.Pin = true;
                this.fundoContext.SaveChanges();
                return "Notes Pinned";
            }
            else
            {
                return null;
            }
        }
        public string UnPin(long NoteId)
        {
            var result = this.fundoContext.Notestables.Where(Upin => Upin.NoteId == NoteId && Upin.Pin == true).SingleOrDefault();
            if (result != null)
            {
                result.Pin = false;
                this.fundoContext.SaveChanges();
                return "Notes UnPinned";
            }
            else
            {
                return null;
            }
        }
        public string Trash(long NoteId)
        {
            var TrashNote = this.fundoContext.Notestables.Where(X => X.NoteId == NoteId).SingleOrDefault();
            if (TrashNote != null)
            {
                if (TrashNote.Delete == false)
                {
                    TrashNote.Delete = true;
                    this.fundoContext.SaveChanges();
                    return "Notes trashed";
                }
                if (TrashNote.Delete == true)
                {
                    TrashNote.Delete = false;
                    this.fundoContext.SaveChanges();
                    return "Notes Untrashed";
                }
                return null;
            }
            else
            {
                return " No Note";

            }
        }
        public string Color(long NoteId, string addcolor)
        {

            var color = this.fundoContext.Notestables.Where(c => c.NoteId == NoteId).SingleOrDefault();
            if (color != null)
            {
                if (addcolor != null)
                {
                    color.Color = addcolor;
                    this.fundoContext.Notestables.Update(color);
                    return this.fundoContext.SaveChanges().ToString();
                }
                else
                {
                    return null;
                }
            }
            throw new Exception();
        }
        public bool Image(IFormFile imageURL, long NoteId)
        {
            try
            {
                if (NoteId > 0)
                {
                    var note = this.fundoContext.Notestables.Where(x => x.NoteId == NoteId).SingleOrDefault();
                    if (note != null)
                    {
                        Account acc = new Account(
                            _Toolsettings["Cloudinary:cloud_name"],
                            _Toolsettings["Cloudinary:api_key"],
                            _Toolsettings["Cloudinary:api_secret"]
                            );
                        Cloudinary Cld = new Cloudinary(acc);
                        var path = imageURL.OpenReadStream();
                        ImageUploadParams upLoadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(imageURL.FileName, path)
                        };
                        var UploadResult = Cld.Upload(upLoadParams);
                        note.Image = UploadResult.Url.ToString();
                        note.ModifiedAt = DateTime.Now;
                        this.fundoContext.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteNoteBgImage(long NoteId)
        {
            try
            {
                if (NoteId > 0)
                {
                    var note = this.fundoContext.Notestables.Where(x => x.NoteId == NoteId).SingleOrDefault();
                    if (note != null)
                    {
                        note.Image = "Image";
                        note.ModifiedAt = DateTime.Now;
                        this.fundoContext.SaveChanges();
                        return true;
                    }
                    return false;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
