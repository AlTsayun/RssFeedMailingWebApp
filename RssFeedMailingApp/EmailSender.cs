using System;
using System.Net;
using System.Net.Mail;
using System.Web.Services;
namespace EmailSenderService{
 
    [WebService(Namespace = "http://example.com/webservices")]
    public class EmailSender : WebService
    {
        private const string emailSrc = "rssfeedmailingapp@mail.ru";
        private const string emailSrcPass = "OYfZZ5Iy8XIX";
        private const string subject = "RSS feed";

        private SmtpClient smtp = new SmtpClient()
        {
            Port = 587,
            Host = $"smtp.mail.ru",
            EnableSsl = true,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(emailSrc, emailSrcPass),
            DeliveryMethod = SmtpDeliveryMethod.Network
        };

        [WebMethod]
        public bool SendEmail(string emailDest,  string messageText)
        {
            bool res = true;
            try
            {
                MailMessage message = new MailMessage()
                {
                    From = new MailAddress(emailSrc),
                    Subject = subject,
                    IsBodyHtml = true,
                    Body = WebUtility.HtmlDecode(messageText)
                };
                message.To.Add(new MailAddress(emailDest));
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