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
    [Authorize]
    public class CollaboratorController : ControllerBase
    {
        private readonly ICollaboratorBL collaboratorBL; // can only be assigned a value from within the constructor(s) of a class.
        private readonly FundoContext fundocontext;
        public CollaboratorController(ICollaboratorBL collaboratorBL)
        {
            this.collaboratorBL = collaboratorBL;
        }
        [HttpPost]// POST is to send and receive data.
        [Route("Create")]
        public IActionResult CollaboratorNotes(Collaboratormodel collaborator)
        {
            try
            {
                long userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.collaboratorBL.AddCollaboratorToNotes(collaborator);
                if (result != false)
                {
                    return this.Ok(new { result });
                }
                return BadRequest();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
