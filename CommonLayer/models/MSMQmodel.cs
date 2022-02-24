using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.models
{
    public class MSMQmodel
    {
        MessageQueue messageQueue = new MessageQueue();
        public void MSMQSender()
        {
            messageQueue.Path = @".\private$\Token";//for windows path

            if (!MessageQueue.Exists(messageQueue.Path))
            {

                MessageQueue.Create(messageQueue.Path);

            }
        }
    }
}
