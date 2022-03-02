using BusinessLayer.interfaces;
using CommonLayer.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
                var result = this.notesBL.UpdateNote(notesUpdatemodel);
                return this.Ok(new { success = true, message = "Notes Updated Successful", data = result });
            }
            catch (Exception)
            {
                return this.BadRequest(new { success = false, message = "Notes Not Updated" });
            }
        }
        [HttpGet]
        [Route("GetAllNotes")]
        public IActionResult GetAllNotes()
        {
            try
            {
                List<Notes> notes = this.notesBL.GetAllNotes();
                return Ok(notes);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpDelete]
        [Route("Deletenote")]
        public IActionResult DeleteNote(int Noteid)
        {
            try
            {
                var delete = this.notesBL.DeleteNote(Noteid);
                return this.Ok(delete);
            }
            catch (Exception)
            {
                return this.BadRequest();
            }
        }
    }
}