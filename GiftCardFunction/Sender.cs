using System.Net.Mail;
using System.Net;
using System.Drawing;
using System;

namespace FunctionApp
{
    public class Sender
    {
        private SmtpClient _smtpClient;

        public Sender()
        {
            _smtpClient = new SmtpClient("asmtp.mail.hostpoint.ch")
            {
                Port = 587,
                Credentials = new NetworkCredential("alfredtest@sato-code.com", "qwwedded4567tr"),
                EnableSsl = true,
            };
        }


        public bool EmailSender( string playerEmail, string attachmentPath)
        {

            var mailMessage = new MailMessage
            {
                From = new MailAddress("alfredtest@sato-code.com"),
                Subject = "Test",
                IsBodyHtml = true,
            };
            mailMessage.To.Add(playerEmail);
            
            Attachment attachment = new Attachment(attachmentPath);
            mailMessage.Attachments.Add(attachment);

            // Invio dell'email
            try
            {
                _smtpClient.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Si è verificato un errore durante l'invio dell'email: " + ex.Message);
                return false;
            }
        }
    }
}
