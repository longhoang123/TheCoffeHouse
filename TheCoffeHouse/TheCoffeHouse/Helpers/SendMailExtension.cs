using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace TheCoffeHouse.Helpers
{
    public class SendMailExtension
    {
        public static void SendEmail(string subject, string body, string recipients)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("thecoffeehouseie307@gmail.com");
                mail.To.Add(recipients);
                mail.Subject = subject;
                mail.Body = body;
                SmtpServer.Port = 587;
                SmtpServer.Host = "smtp.gmail.com";
                SmtpServer.EnableSsl = true;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential("thecoffeehouseie307@gmail.com", "1a2b3c4dEe");

                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                //error
            }
        }
    }
}
