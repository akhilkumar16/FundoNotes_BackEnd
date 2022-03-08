using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace CommonLayer.models
{
    public class MSMQmodel
    {
        MessageQueue messageQueue = new MessageQueue();
        public void MSMQSender(string token)
        {
            messageQueue.Path = @".\private$\Token";//for windows path

            if (!MessageQueue.Exists(messageQueue.Path))
            {

                MessageQueue.Create(messageQueue.Path);

            }
            messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });//Email will be in formatted order.
            messageQueue.ReceiveCompleted += MessageQueue_ReceiveCompleted;// Received is an Event .
            messageQueue.Send(token);
            messageQueue.BeginReceive();//its a method 
            messageQueue.Close();//line 23 - 25 is used to connecting with obj and sending multiple request to receive the response.
        }
         
        private void MessageQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)//Delegate is genreated by double tab.
        {
            var message = messageQueue.EndReceive(e.AsyncResult);// it receive the completed event.
            string token = message.Body.ToString();// returns a string which is related to object.
            string Subject = "Access Token"; // Display the Email subject.
            string Body = "Dear User token for changing of password is ---- "+ token; // Display the body of the mail.
            string JWT = DecodeJWT(token);//allows the application to use the data, and validation.
            var SmtpClient = new SmtpClient("Smtp.gmail.com") //To construct and send an email message by using SmtpClient.
            {
                Port = 587,
                Credentials = new NetworkCredential("mastroxtream@gmail.com", "Mastroxtream@123"),// connecting to Email.
                EnableSsl = true,
            };
            SmtpClient.Send("mastroxtream@gmail.com", JWT, Subject, Body); //Mail sending Method.
            messageQueue.BeginReceive(); //to asynchronously receive a server response.
        }
        private string DecodeJWT(string token)
        {
            try
            {
                var DecodeToken = token;
                var handler = new JwtSecurityTokenHandler();
                var jsonTOken = handler.ReadJwtToken(DecodeToken); //read access token from the current context//
                var result = jsonTOken.Claims.FirstOrDefault().Value; //get the current user identity
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}