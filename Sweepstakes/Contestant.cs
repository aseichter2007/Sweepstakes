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

        public void Notify(string[] company, string body)
        {
           
            MimeMessage message = new MimeMessage()
            {
                Sender = new MailboxAddress(company[0], company[1]),
            Subject = "A winner has been chosen",
            Body = new TextPart(TextFormat.Plain) {Text= body}
            };
            message.To.Add(new MailboxAddress(email));

            SmtpClient smtpClient = new MailKit.Net.Smtp.SmtpClient();

            smtpClient.MessageSent += async (sender, args) =>
            { 
                smtpClient.ServerCertificateValidationCallback = (s, c, h, e) => true;

                await smtpClient.ConnectAsync("smtp-mail.outlook.com", 587, MailKit.Security.SecureSocketOptions.Auto);
                await smtpClient.AuthenticateAsync(company[1], company[2]);
                await smtpClient.SendAsync(message);
                await smtpClient.DisconnectAsync(true);
            };
            
        }
    }
}
