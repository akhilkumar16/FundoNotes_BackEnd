using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommonLayer.models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
        /// Variables
        /// </summary>
        FundoContext context;
        IConfiguration config;

        /// <summary>
        /// Constructor function
        /// </summary>
        /// <param name="context"></param>
        /// <param name="_config"></param>
        public NotesRL(FundoContext context, IConfiguration config)
        {
            this.context = context;
            this.config = config;
        }

        /// <summary>
        /// Create note code
        /// </summary>
        /// <param name="noteModel"></param>
        /// <returns></returns>
        public bool CreateNote(Notesmodel noteModel, long userid)
        {
            try
            {
                Notes newNotes = new Notes();
                newNotes.Title = noteModel.Title;
                newNotes.Discription = noteModel.Discription;
                newNotes.Reminder = noteModel.Reminder;
                newNotes.Color = noteModel.Color;
                newNotes.Backgroundcolour = noteModel.Backgroundcolour;
                newNotes.CreatedAt = DateTime.Now;
                newNotes.UserId = userid;
                //Adding the data to database
                this.context.Notestables.Add(newNotes);
                //Save the changes in database
                int result = this.context.SaveChanges();
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
        /// Show user all his notes using this function
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Notes> ShowUserNotes(long userid)
        {
            try
            {
                return this.context.Notestables.ToList().Where(x => x.UserId == userid);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Show the user a specific note
        /// </summary>
        /// <param name="noteid"></param>
        /// <returns></returns>
        public IEnumerable<Notes> GetIDNote(long noteid)
        {
            try
            {
                return this.context.Notestables.Where(x => x.NoteId == noteid);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Update note and then change ModifiedAt time to modified date and time
        /// </summary>
        /// <param name="note"></param>
        /// <returns></returns>
        public string UpdateNotes(Notes note)
        {
            try
            {
                if (note.NoteId != 0)
                {
                    this.context.Entry(note).State = EntityState.Modified;
                    this.context.SaveChanges();
                    return "Done";
                }
                return "Failed";
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Archive note function using ID of the note
        /// </summary>
        /// <param name="noteid"></param>
        /// <returns></returns>
        public bool ArchiveNote(long noteid)
        {
            try
            {
                var note = this.context.Notestables.Where(x => x.NoteId == noteid).SingleOrDefault();
                if (note.Archive == false)
                {
                    note.Archive = true;
                    note.Pin = false;
                    note.ModifiedAt = DateTime.Now;
                    this.context.Entry(note).State = EntityState.Modified;
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    note.Archive = false;
                    note.ModifiedAt = DateTime.Now;
                    this.context.Entry(note).State = EntityState.Modified;
                    context.SaveChanges();
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Function to Pin the note using note id
        /// </summary>
        /// <param name="noteid"></param>
        /// <returns></returns>
        public bool PinNote(long noteid)
        {
            try
            {
                var note = this.context.Notestables.Where(x => x.NoteId == noteid).SingleOrDefault();
                if (note.Pin == false)
                {
                    note.Pin = true;
                    note.Archive = false;
                    note.Delete = false;
                    note.ModifiedAt = DateTime.Now;
                    this.context.Entry(note).State = EntityState.Modified;
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    note.Pin = false;
                    note.ModifiedAt = DateTime.Now;
                    this.context.Entry(note).State = EntityState.Modified;
                    context.SaveChanges();
                    return false;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Delete Note function using the ID of the note
        /// </summary>
        /// <param name="noteid"></param>
        /// <returns></returns>
        public bool DeleteNote(long noteid)
        {
            try
            {
                var note = this.context.Notestables.Where(x => x.NoteId == noteid).SingleOrDefault();
                if (note.Delete == false)
                {
                    note.Delete = true;
                    note.Archive = false;
                    note.Pin = false;
                    context.Entry(note).State = EntityState.Modified;
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    note.Delete = false;
                    note.ModifiedAt = DateTime.Now;
                    this.context.Entry(note).State = EntityState.Modified;
                    context.SaveChanges();
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Forever delete a note
        /// </summary>
        /// <param name="noteid"></param>
        /// <returns></returns>
        public bool ForeverDeleteNote(long noteid)
        {
            try
            {
                if (noteid > 0)
                {
                    var notes = this.context.Notestables.Where(x => x.NoteId == noteid).SingleOrDefault();
                    if (notes != null)
                    {
                        this.context.Notestables.Remove(notes);
                        this.context.SaveChangesAsync();
                        return true;
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
        /// Function for adding note color
        /// </summary>
        /// <param name="color"></param>
        /// <param name="noteid"></param>
        /// <returns></returns>
        public string AddNoteColor(string color, long noteid)
        {
            try
            {
                if (noteid > 0)
                {
                    var note = this.context.Notestables.Where(x => x.NoteId == noteid).SingleOrDefault();
                    if (note != null)
                    {
                        note.Color = color;
                        note.ModifiedAt = DateTime.Now;
                        this.context.SaveChangesAsync();
                        return "Updated";
                    }
                    else
                    {
                        return "Failed";
                    }
                }
                return "Failed";
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Function for removing note color
        /// </summary>
        /// <param name="color"></param>
        /// <param name="noteid"></param>
        /// <returns></returns>
        public string RemoveNoteColor(long noteid)
        {
            try
            {
                if (noteid > 0)
                {
                    var note = this.context.Notestables.Where(x => x.NoteId == noteid).SingleOrDefault();
                    if (note != null)
                    {
                        note.Color = "";
                        note.ModifiedAt = DateTime.Now;
                        this.context.SaveChangesAsync();
                        return "Updated";
                    }
                    else
                    {
                        return "Failed";
                    }
                }
                return "Failed";
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// function for adding a background image for a note
        /// </summary>
        /// <param name="imageURL"></param>
        /// <param name="noteid"></param>
        /// <returns></returns>
        public bool AddNoteBgImage(IFormFile imageURL, long noteid)
        {
            try
            {
                if (noteid > 0)
                {
                    var note = this.context.Notestables.Where(x => x.NoteId == noteid).SingleOrDefault();
                    if (note != null)
                    {
                        Account acc = new Account(
                            config["Cloudinary:cloud_name"],
                            config["Cloudinary:api_key"],
                            config["Cloudinary:api_secret"]
                            );
                        Cloudinary Cld = new Cloudinary(acc);
                        var path = imageURL.OpenReadStream();
                        ImageUploadParams upLoadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(imageURL.FileName, path)
                        };
                        var UploadResult = Cld.Upload(upLoadParams);
                        note.Backgroundcolour = UploadResult.Url.ToString();
                        note.ModifiedAt = DateTime.Now;
                        this.context.SaveChangesAsync();
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
        /// function to remove a background image from a note
        /// </summary>
        /// <param name="noteid"></param>
        /// <returns></returns>
        public bool DeleteNoteBgImage(long noteid)
        {
            try
            {
                if (noteid > 0)
                {
                    var note = this.context.Notestables.Where(x => x.NoteId == noteid).SingleOrDefault();
                    if (note != null)
                    {
                        note.Backgroundcolour = "";
                        note.ModifiedAt = DateTime.Now;
                        this.context.SaveChangesAsync();
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

        /// <summary>
        /// admin function
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Notes> GetEveryonesNotes()
        {
            try
            {
                return this.context.Notestables.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
