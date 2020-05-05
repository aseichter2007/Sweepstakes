using MailKit;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Text;


namespace Sweepstakes
{
    class Contestant:INotification
    {
        public string firstName;
        public string lastName;
        public string email;
        public string registrationNumber;

        public Contestant(string firstName, string lastName, string email, string registrationNumber)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.registrationNumber = registrationNumber;
        }

        public void Notify(string[] company, string body, SmtpClient smtpClient)
        {
           
            MimeMessage message = new MimeMessage()
            {
                Sender = new MailboxAddress(company[0], company[1]),
            Subject = "A winner has been chosen",
            Body = new TextPart(TextFormat.Plain) {Text= body}
            };
            message.To.Add(new MailboxAddress(email));

            
            smtpClient.Send(message);
            UserInterface.DisplayInline("Message was sent to " + email);
            
        }
    }
}
