using BusinessLayer.interfaces;
using CommonLayer.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.context;
using RepositoryLayer.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fundonotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] //user to grant and restrict permissions on Web pages.
    public class NotesController : ControllerBase
    {
        private readonly INotesBL notesBL; // can only be assigned a value from within the constructor(s) of a class.
        private readonly FundoContext fundocontext;
        public NotesController(INotesBL notesBL)
        {
            this.notesBL = notesBL;
        }
        [HttpPost]// POST is to send and receive data.
        [Route("CreateNotes")]
        public IActionResult AddNotes(Notesmodel notesmodel)
        {
            try
            {
                long userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.notesBL.AddNotes(notesmodel,userid);
                return this.Ok(new { success = true, message = "Notes Added Successful", data = result });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { success = false, message = e.InnerException });
            }
        }
        [HttpPut]
        [Route("updateNotes")]
        public IActionResult UpdateNotes(Notesmodel notesUpdatemodel)
        {
            try
            {
                long userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.notesBL.UpdateNote(notesUpdatemodel,userid);
                return this.Ok(new { success = true, message = "Notes Updated Successful", data = result });
            }
            catch (Exception)
            {
                return this.BadRequest(new { success = false, message = "Notes Not Updated" });
            }
        }
        [HttpGet]
        [Route("GetAllNotes")]
        public IActionResult GetAllNotes(long userId)
        {
            try
            {
                long userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                List<Notes> notes = this.notesBL.GetAllNotes(userId);
                return Ok(notes);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("GetNotesId")]
        public IActionResult Getnote(long NoteId)
        {
            try
            {
                long userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                List<Notes> notes = this.notesBL.GetNote(NoteId);
                return Ok(notes);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpDelete]
        [Route("Deletenote")]
        public IActionResult DeleteNote(long NoteId)
        {
            try
            {
                long userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var delete = this.notesBL.DeleteNote(NoteId);
                return this.Ok(delete);
            }
            catch (Exception)
            {
                return this.BadRequest();
            }
        }
        [HttpPut]
        [Route("Archive")]
        public IActionResult Archieve(long NoteId)
        {
            try
            {
                long userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var Arch = this.fundocontext.Notestables.Where(x => x.NoteId == NoteId).SingleOrDefault();
                if (Arch.UserId == userid)
                {
                    var archieve = this.notesBL.Archive(NoteId);
                    return this.Ok(new { success = true, message = "Notes Archieve Successful" });
                }
                else
                {
                    return this.Unauthorized(new { Success = false, message = "Not a user" });
                }
            }
            catch (Exception)
            {
                return this.BadRequest(new { success = false, message = "Notes Not Archieved" });
            }
        }
        [HttpPut]
        [Route("Unarchive")]
        public IActionResult UnArchieve(long NoteId)
        {
            try
            {
                var unarchieve = this.notesBL.UnArchive(NoteId);
                return this.Ok(new { success = true, message = "Notes Unarchieve Successful",data = unarchieve });
            }
            catch (Exception)
            {
                return this.BadRequest(new { success = false, message = "Notes Not UnArchieved" });
            }
        }
        [HttpPut]
        [Route("pin")]
        public IActionResult Pin(long NoteId)
        {
            try
            {
                var pin = this.notesBL.Pin(NoteId);
                return this.Ok(new { success = true, message = "Notes Pinned Successful", data = pin });
            }
            catch (Exception)
            {
                return this.BadRequest(new { success = false, message = "Notes Not pinned" });
            }
        }
        [HttpPut]
        [Route("unpin")]
        public IActionResult UnPin(long NoteId)
        {
            try
            {
                var unpin = this.notesBL.UnPin(NoteId);
                return this.Ok(new { success = true, message = "Notes UnPinned Successful", data = unpin });
            }
            catch (Exception)
            {
                return this.BadRequest(new { success = false, message = "Notes Not pinned" });
            }
        }
        [HttpDelete]
        [Route("Trashnote")]
        public IActionResult TrashNote(long NoteId)
        {
            try
            {
                long userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var trash = this.notesBL.Trash(NoteId);
                return this.Ok(new { success = true, message = "Notes Trashed Successful", data = trash });
            }
            catch (Exception)
            {
                return this.BadRequest(new { success = false, message = "Notes UnTrashed" });
            }
        }
    }
}