using BusinessLayer.interfaces;
using CommonLayer.models;
using RepositoryLayer.entities;
using RepositoryLayer.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.services
{
    public class LabelBL: ILabelBL
    {
        private readonly ILabelRL labelRL;
        public LabelBL(ILabelRL labelRL)
        {
            this.labelRL = labelRL;
        }
        public string AddLabel(Labelmodel labelmodel)
        {
            try
            {
                return labelRL.AddLabel(labelmodel);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string DeleteLabel(long LabelId)
        {
            try
            {
                return labelRL.DeleteLabel(LabelId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Label> GetAllLabel()
        {
            try
            {
                return labelRL.GetAllLabel();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Label> GetLabel(long NoteId)
        {
            try
            {
                return labelRL.GetLabel(NoteId);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public string UpdateLabel(long LabelId, string LabelName)
        {
            try
            {
                return labelRL.UpdateLabel(LabelId, LabelName);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
