using CommonLayer.models;
using RepositoryLayer.context;
using RepositoryLayer.entities;
using RepositoryLayer.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.services
{
    /// <summary>
    /// Label class interface connection
    /// </summary>
    public class LabelRL : ILabelRL
    {
        //context class is used to query or save data to the database.
        public readonly FundoContext fundoContext;
        //constructor
        public LabelRL(FundoContext fundoContext)
        {
            this.fundoContext = fundoContext;
        }
        /// <summary>
        /// Method to add Label
        /// </summary>
        /// <param name="labelmodel"></param>
        /// <returns></returns>
        public string AddLabel(Labelmodel labelmodel)
        {
            try
            {
                // checking with the notestable db to find NoteId
                var note = fundoContext.Notestables.Where(L => L.NoteId == labelmodel.NoteId).SingleOrDefault();
                // Entity class Instance
                Label label = new Label()
                {
                    UserId = note.UserId,
                    LabelName = labelmodel.LabelName,
                    NoteId = labelmodel.NoteId,
                };
                // storing the data in Label db
                this.fundoContext.Labeltables.Add(label);
                this.fundoContext.SaveChanges();
                return "Label Added";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Method to update the label Name
        /// </summary>
        /// <param name="LabelId"></param>
        /// <param name="LabelName"></param>
        /// <returns></returns>
        public string UpdateLabel(long LabelId, string LabelName)
        {
            // checking with the notestable db to find NoteId
            var update = this.fundoContext.Labeltables.Where(U => U.LabelId == LabelId).SingleOrDefault();
            if (update != null)
            {
                update.LabelName = LabelName;
                this.fundoContext.Labeltables.Update(update);//updates the label
                this.fundoContext.SaveChanges(); 
                return "Updated";
            }
            else 
            { 
                return default; 
            }
        }
        /// <summary>
        /// Getting label by NoteId
        /// </summary>
        /// <param name="NoteId"></param>
        /// <returns></returns>
        public List<Label> GetLabel(long NoteId)
        {
            // checking of note Id in db
            var listlabel = fundoContext.Labeltables.Where(list => list.NoteId == NoteId).SingleOrDefault();
            if (listlabel != null)
            {
                return fundoContext.Labeltables.Where(list => list.NoteId == NoteId).ToList();
            }
            return null;
        }
        /// <summary>
        /// Getting of all labels
        /// </summary>
        /// <returns></returns>
        public List<Label> GetAllLabel()
        {
            try
            {
                // displays all the label from db
                return this.fundoContext.Labeltables.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Deleting of label
        /// </summary>
        /// <param name="LabelId"></param>
        /// <returns></returns>
        public string DeleteLabel(long LabelId)
        {
            //checks in the db with label Id
            var Deletelabel = this.fundoContext.Labeltables.Where(del => del.LabelId == LabelId).FirstOrDefault();
            if (Deletelabel != null)
            {
                // deletes the entry
                this.fundoContext.Labeltables.Remove(Deletelabel);
                this.fundoContext.SaveChanges();
                return "Deleted";
            }
            else { return null; }
        }
    }
}
