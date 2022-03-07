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
    /// Collaborator controller connected with base controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]//user to grant and restrict permissions on Web pages.
    public class CollaboratorController : ControllerBase
    {
        private readonly ICollaboratorBL collaboratorBL; // can only be assigned a value from within the constructor(s) of a class.
        private readonly FundoContext fundocontext;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        public CollaboratorController(ICollaboratorBL collaboratorBL, FundoContext fundocontext, IMemoryCache memoryCache,IDistributedCache distributedCache)
        {
            this.collaboratorBL = collaboratorBL;
            this.fundocontext = fundocontext;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }
        /// <summary>
        /// Add notes to collaborator
        /// </summary>
        /// <param name="collaborator"></param>
        /// <returns></returns>
        [HttpPost]// POST is to send and receive data.
        [Route("Create")]
        public IActionResult CollaboratorNotes(Collaboratormodel collaborator)
        {
            try
            {
                //checking if the user has a claim to access.
                long userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.collaboratorBL.AddCollaboratorToNotes(collaborator);
                if (result != false)
                {
                    return this.Ok(new { success = true, message = "collaboration Addition Successful", data = collaborator });
                }
                return BadRequest();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
        /// <summary>
        /// Deleting the note from collaborator
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete")]
        public IActionResult DeleteCollaborator(long NoteId)
        {
            try
            {
                //checking if the user has a claim to access.
                long userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var Delete = this.collaboratorBL.DeleteCollaborator(NoteId);
                return Ok(new { Delete });
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }/// <summary>
        /// getting by noteID
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
                IEnumerable<Collaborator> collaborators = fundocontext.Colltables.Where(G => G.NoteId == NoteId).ToList();
                List<Collaborator> notes = this.collaboratorBL.GetNote(NoteId);
                return Ok(notes);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// Cache Memory
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("redis")]
        public async Task<IActionResult> GetAllRedisCache()
        {
            var cacheKey = "AllCollab";
            string serializedAllCollab;
            var AllCollab = new List<Collaborator>();
            var redisAllCollab = await distributedCache.GetAsync(cacheKey);
            if (redisAllCollab != null)
            {
                serializedAllCollab = Encoding.UTF8.GetString(redisAllCollab);
                AllCollab = JsonConvert.DeserializeObject<List<Collaborator>>(serializedAllCollab);
            }
            else
            {
                AllCollab = await fundocontext.Colltables.ToListAsync();
                serializedAllCollab = JsonConvert.SerializeObject(AllCollab);
                redisAllCollab = Encoding.UTF8.GetBytes(serializedAllCollab);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisAllCollab, options);
            }
            return Ok(AllCollab);
        }
    }
}
