using System;
using System.Net;
using System.Net.Mail;
using System.Web.Services;
namespace EmailSenderService{
 
    [WebService(Namespace = "http://example.com/webservices")]
    public class EmailSender : WebService 
    {

        [WebMethod]
        public bool SendEmail(string emailSrc, string emailSrcPass, string emailDest, string subject, string messageText)
        {
            bool res = true;
            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress(emailSrc);
                SmtpClient smtp = new SmtpClient();
                message.To.Add(new MailAddress(emailDest));
                message.Subject = subject;
                message.IsBodyHtml = false; //to make message body as html  
                message.Body = messageText;
                smtp.Port = 587;
                smtp.Host = $"smtp.{emailSrc.Split('@')[1]}";
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(emailSrc, emailSrcPass);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception)
            {
                res = false;
            }

            return res;
        }
        
    }
}