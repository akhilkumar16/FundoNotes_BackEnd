using BusinessLayer.interfaces;
using CommonLayer.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fundonotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INotesBL notesBL; // can only be assigned a value from within the constructor(s) of a class.
        public NotesController(INotesBL notesBL)
        {
            this.notesBL = notesBL;
        }
        [HttpPost("CreateNotes")] // POST is to send and receive data.
        public IActionResult AddNotes(Notesmodel notesmodel)
        {
            try
            {
                var result = this.notesBL.AddNotes(notesmodel);
                return this.Ok(new { success = true, message = "Notes Added Successful", data = result });
            }
            catch (Exception)
            {
                return this.BadRequest(new { success = false, message = "Notes Not Added" });
            }
        }
    }
}
