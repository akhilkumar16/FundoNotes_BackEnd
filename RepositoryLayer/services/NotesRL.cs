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
using System.Data;
using System.Linq;
using System.Text;

namespace RepositoryLayer.services
{
    /// <summary>
    /// Notes class interfaces connection
    /// </summary>
    public class NotesRL : INotesRL
    {
        /// <summary>
        /// Fundocontext private variable
        /// </summary>
        private readonly FundoContext fundoContext; //context class is used to query or save data to the database.
        IConfiguration _Toolsettings;  //IConfiguration interface is used to read Settings and Connection Strings from AppSettings.
        public NotesRL(FundoContext fundoContext, IConfiguration Toolsettings)
        {
            this.fundoContext = fundoContext;
            _Toolsettings = Toolsettings;
        }
        /// <summary>
        /// Creates Notes 
        /// </summary>
        /// <param name="notesmodel"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool AddNotes(Notesmodel notesmodel, long userId)
        {
            try
            {
                //entities class instance 
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
                fundonotes.ModifiedAt = DateTime.Now;
                fundonotes.CreatedAt = DateTime.Now;
                // stores in DataBase
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
        /// <summary>
        /// All Notes by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Notes> GetAllNotes(long userId)
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
        /// <summary>
        /// All Notes by Note Id
        /// </summary>
        /// <param name="NotesId"></param>
        /// <returns></returns>
        public List<Notes> GetNote(long NotesId)
        {
            // checking of Notes Db
            var listNote = fundoContext.Notestables.Where(list => list.NoteId == NotesId).SingleOrDefault();
            if (listNote != null)
            {
                return fundoContext.Notestables.Where(list => list.NoteId == NotesId).ToList();
            }
            return null;
        }
        /// <summary>
        /// Modifies the existing Note
        /// </summary>
        /// <param name="notesUpdatemodel"></param>
        /// <returns></returns>
        public string UpdateNote(Notesmodel notesUpdatemodel ,long NoteId )
        {
            try
            {
                //checking with the notes db
                var result = fundoContext.Notestables.Where(X => X.NoteId == NoteId).SingleOrDefault();
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
        /// <summary>
        /// Deletes total notes by Note Id
        /// </summary>
        /// <param name="NoteId"></param>
        /// <returns></returns>
        public string DeleteNote(long NoteId)
        {
            // checking of noteid in db
            var deletenote = fundoContext.Notestables.Where(del => del.NoteId == NoteId).SingleOrDefault();
            if (deletenote != null)
            {
                // deletes in the database
                fundoContext.Notestables.Remove(deletenote);
                this.fundoContext.SaveChanges();
                return "Notes Deleted Successfully";
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Hides the note
        /// </summary>
        /// <param name="NoteId"></param>
        /// <returns></returns>
        public string Archive(long NoteId)
        {
            // returns true noteid == noteid 
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
        /// <summary>
        /// Unhides the note
        /// </summary>
        /// <param name="NoteId"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Note get pinned
        /// </summary>
        /// <param name="NoteId"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Note gets Unpinned
        /// </summary>
        /// <param name="NoteId"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Deleted note by trash
        /// </summary>
        /// <param name="NoteId"></param>
        /// <returns></returns>
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
        /// <summary>
        /// A color gets added
        /// </summary>
        /// <param name="NoteId"></param>
        /// <param name="addcolor"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Uploads a Image --Method for uploading image from local host to cloudinary
        /// </summary>
        /// <param name="imageURL"></param>
        /// <param name="NoteId"></param>
        /// <returns></returns>
        public bool Image(IFormFile imageURL, long NoteId)// IFormFile-interface that represents transmitted files in an HTTP request.
        {
            try
            {
                if (NoteId > 0)
                {
                    var note = this.fundoContext.Notestables.Where(x => x.NoteId == NoteId).SingleOrDefault();
                    if (note != null)
                    {
                        Account acc = new Account(
                            //Iconfiguration --_Toolsettings
                            _Toolsettings["Cloudinary:cloud_name"],//Declare a Cloudinary service
                            _Toolsettings["Cloudinary:api_key"],
                            _Toolsettings["Cloudinary:api_secret"]// stored in app.json file
                            );
                        Cloudinary Cld = new Cloudinary(acc);
                        var path = imageURL.OpenReadStream();//extract the needed information from the URL
                        ImageUploadParams upLoadParams = new ImageUploadParams()//save an image to cloudinary
                        {
                            File = new FileDescription(imageURL.FileName, path)
                        };
                        var UploadResult = Cld.Upload(upLoadParams); //upload to cloudinary 
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
        /// <summary>
        /// Delete the uploaded Image
        /// </summary>
        /// <param name="NoteId"></param>
        /// <returns></returns>
        public bool DeleteImage(long NoteId)
        {
            try
            {
                if (NoteId > 0)
                {
                    var note = this.fundoContext.Notestables.Where(x => x.NoteId == NoteId).SingleOrDefault();
                    if (note != null)
                    {
                        note.Image = "Please select Image ";
                        note.ModifiedAt = DateTime.Now;
                        this.fundoContext.SaveChanges();
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
    }
}
