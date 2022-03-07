using BusinessLayer.interfaces;
using CommonLayer.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RepositoryLayer.context;
using RepositoryLayer.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundonotes.Controllers
{
    /// <summary>
    /// Notes controller connted with base controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] //user to grant and restrict permissions on Web pages.
    public class NotesController : ControllerBase
    {
        private readonly INotesBL notesBL; // can only be assigned a value from within the constructor(s) of a class.
        private readonly FundoContext fundocontext;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        public NotesController(INotesBL notesBL , FundoContext fundocontext,IMemoryCache  memoryCache,IDistributedCache distributedCache)
        {
            this.notesBL = notesBL;
            this.fundocontext = fundocontext;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }
        /// <summary>
        /// Create opration Api
        /// </summary>
        /// <param name="notesmodel"></param>
        /// <returns></returns>
        [HttpPost]// POST is to send and receive data.
        [Route("Create")]
        public IActionResult AddNotes(Notesmodel notesmodel)
        {
            try
            {
                //checking if the user has a claim to access.
                long userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.notesBL.AddNotes(notesmodel,userid);
                return this.Ok(new { success = true, message = "Notes Added Successful", data = notesmodel });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { success = false, message = e.InnerException });
            }
        }
        /// <summary>
        /// Update operation Api
        /// </summary>
        /// <param name="notesUpdatemodel"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("update")]
        public IActionResult UpdateNotes(Notesmodel notesUpdatemodel)
        {
            try
            {
                //checking if the user has a claim to access.
                long userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.notesBL.UpdateNote(notesUpdatemodel,userid);
                return this.Ok(new { success = true, message = "Notes Updated Successful", data = notesUpdatemodel });
            }
            catch (Exception)
            {
                return this.BadRequest(new { success = false, message = "Notes Not Updated" });
            }
        }
        /// <summary>
        /// Read operation Api
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetByuserId")]
        public IActionResult GetAllNotes()
        {
            try
            {
                //checking if the user has a claim to access.
                long userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                IEnumerable<Notes> notes = this.notesBL.GetAllNotes(userid);
                return Ok(notes);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// Read opeartion Api by note Id
        /// </summary>
        /// <param name="NoteId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetById")]
        public IActionResult Getnote(long NoteId)
        {
            try
            {
                //checking if the user has a claim to access.
                long userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                List<Notes> notes = this.notesBL.GetNote(NoteId);
                return Ok(notes);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// All notes by userers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAllUserNotes()
        {
            try
            {
                IEnumerable<Notes> notes = this.notesBL.GetAllUserNotes();
                return Ok(notes);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HttpGet("redis")]
        public async Task<IActionResult> GetAllCustomersUsingRedisCache()
        {
            var cacheKey = "AllNOtes";
            string serializedAllNotes;
            var AllNotes = new List<Notes>();
            var redisAllNotes = await distributedCache.GetAsync(cacheKey);
            if (redisAllNotes != null)
            {
                serializedAllNotes = Encoding.UTF8.GetString(redisAllNotes);
                AllNotes = JsonConvert.DeserializeObject<List<Notes>>(serializedAllNotes);
            }
            else
            {
                AllNotes = await fundocontext.Notestables.ToListAsync();
                serializedAllNotes = JsonConvert.SerializeObject(AllNotes);
                redisAllNotes = Encoding.UTF8.GetBytes(serializedAllNotes);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisAllNotes, options);
            }
            return Ok(AllNotes);
        }
        /// <summary>
        /// Delete operation Api ( Delete total )
        /// </summary>
        /// <param name="NoteId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Delete")]
        public IActionResult DeleteNote(long NoteId)
        {
            try
            {
                //checking if the user has a claim to access.
                long userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var delete = this.notesBL.DeleteNote(NoteId);
                return this.Ok(delete);
            }
            catch (Exception)
            {
                return this.BadRequest();
            }
        }
        /// <summary>
        /// Api for archieve
        /// </summary>
        /// <param name="NoteId"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Api for Unarchieve
        /// </summary>
        /// <param name="NoteId"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Api for note getting pinned 
        /// </summary>
        /// <param name="NoteId"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Api for Unpin
        /// </summary>
        /// <param name="NoteId"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Note moves to trash on deletion  
        /// </summary>
        /// <param name="NoteId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Trash")]
        public IActionResult TrashNote(long NoteId)
        {
            try
            {
                //checking if the user has a claim to access.
                long userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var trash = this.notesBL.Trash(NoteId);
                return this.Ok(new { success = true, message = "Notes Trashed Successful", data = trash });
            }
            catch (Exception)
            {
                return this.BadRequest(new { success = false, message = "Notes UnTrashed" });
            }
        }
        /// <summary>
        /// Api to add color
        /// </summary>
        /// <param name="NoteId"></param>
        /// <param name="addcolor"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("color")]
        public IActionResult AddColor(long NoteId, string addcolor)
        {
            try
            {
                //checking if the user has a claim to access.
                long userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var color = this.notesBL.Color(NoteId, addcolor);
                return Ok(new { success = true, message = "Color Added", data = color });
            }
            catch (Exception)
            {
                return this.BadRequest(new { success = false, message = " Color not Added" });
            }
        }
        /// <summary>
        /// Api to upload Image
        /// </summary>
        /// <param name="imageURL"></param>
        /// <param name="NoteId"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Image")]
        public IActionResult Image(IFormFile imageURL, long NoteId)
        {
            try
            {
                //checking if the user has a claim to access.
                long userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var upload = this.notesBL.Image(imageURL,NoteId);
                return this.Ok(new { success = true, message = " Image Successful",data = upload});
            }
            catch (Exception)
            {
                return this.BadRequest(new { success = false, message = "Image Unsuccessfull" });
            }
        }
        /// <summary>
        /// Deletes Image
        /// </summary>
        /// <param name="NoteId"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("DeleteImage")]
        public IActionResult DeleteImage(long NoteId)
        {
            try
            {
                //checking if the user has a claim to access.
                long userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var upload = this.notesBL.DeleteImage( NoteId);
                return this.Ok(new { success = true, message = " Image Deleted Successful", data = upload });
            }
            catch (Exception)
            {
                return this.BadRequest(new { success = false, message = "Image Not Deleted Unsuccessfull" });
            }
        }
    }
}