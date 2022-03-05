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
    /// <summary>
    /// labelcontroller connected with basecontroller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]//user to grant and restrict permissions on Web pages.
    public class LabelController : ControllerBase
    {
       // can only be assigned a value from within the constructor(s) of a class.
        private readonly ILabelBL labelBL; 
        private readonly FundoContext fundocontext;
        public LabelController(ILabelBL labelBL, FundoContext fundocontext)
        {
            this.labelBL = labelBL;
            this.fundocontext = fundocontext;
        }
        /// <summary>
        /// Creates a label 
        /// </summary>
        /// <param name="labelmodel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addlabel")]
        public IActionResult AddLabel( Labelmodel labelmodel) // IActionResult --how the server should respond to the request.
        {
            try
            {
                //checking if the user has a claim to access.
                long userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var add = this.labelBL.AddLabel(labelmodel); // construtor call.
                return Ok(new { success = true, message = "Label Added Successful", data = labelmodel } );
            }
            catch (Exception)
            {
                return this.BadRequest(new { success = false, message = "label not Added", data = labelmodel });
            }
        }
        /// <summary>
        /// updates the created label
        /// </summary>
        /// <param name="LabelId"></param>
        /// <param name="LabelName"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("updatelabel")]
        public ActionResult UpdateLabel(long LabelId, string LabelName)
        {
            try
            {
                //checking if the user has a claim to access.
                long userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var update = this.labelBL.UpdateLabel(LabelId, LabelName);
                return Ok(new { success = true, message = "Label updated Successful", data = LabelName });
            }
            catch (Exception)
            {
                return this.BadRequest(new { success = false, message = "label not Added", data = LabelName });
            }
        }
        /// <summary>
        /// getting of label by label Id
        /// </summary>
        /// <param name="LabelId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("labelbyID")]
        public IActionResult GetLabel(long LabelId)
        {
            try
            {
                //checking if the user has a claim to access.
                long userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);

                //IEnumerable can find out the no of elements in the collection
                IEnumerable<Label> labelmodels = fundocontext.Labeltables.Where(G => G.LabelId == LabelId).ToList();
                List<Label> dis = this.labelBL.GetLabel(LabelId);
                return Ok(new { success = true, message = "list of label by Given Id", data = labelmodels });
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// getting all the labels
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("AllLabels")]
        public List<Label> GetAllLables()
        {
            //checking if the user has a claim to access.
            long userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
            return this.labelBL.GetAllLabel(); 
        }
        /// <summary>
        /// deleting the label by Id
        /// </summary>
        /// <param name="LabelId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Deletelabel")]
        public IActionResult DeleteLabel(long LabelId)
        {
            try
            {
                //checking if the user has a claim to access.
                long userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var update = this.labelBL.DeleteLabel(LabelId);
                return Ok(new { success = true, message = "Label Deleted ", data = LabelId });
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }
    }
}
