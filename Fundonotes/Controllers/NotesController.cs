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
        [Route("Create")]
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
        [Route("update")]
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
        [Route("GetAll")]
        public IActionResult GetAllNotes()
        {
            try
            {
                long userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                IEnumerable<Notes> notes = this.notesBL.GetAllNotes(userid);
                return Ok(notes);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("GetById")]
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
        [Route("Delete")]
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
                var archieve = this.notesBL.Archive(NoteId);
                return this.Ok(archieve);
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
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
        [Route("Trash")]
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
        [HttpPut]
        [Route("color")]
        public IActionResult AddColor(long NoteId, string addcolor)
        {
            try
            {
                long userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var color = this.notesBL.Color(NoteId, addcolor);
                return Ok(new { success = true, message = "Color Added", data = color });
            }
            catch (Exception)
            {
                return this.BadRequest(new { success = false, message = " Color not Added" });
            }
        }
        [HttpPut]
        [Route("Image")]
        public IActionResult Image(IFormFile imageURL, long NoteId)
        {
            try
            {
                long userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var upload = this.notesBL.Image(imageURL,NoteId);
                return this.Ok(new { success = true, message = " Image Successful",data = upload});
            }
            catch (Exception)
            {
                return this.BadRequest(new { success = false, message = "Image Unsuccessfull" });
            }
        }
    }
}