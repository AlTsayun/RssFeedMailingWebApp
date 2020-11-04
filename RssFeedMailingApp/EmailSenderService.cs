using System;
using System.Net.Mail;
using System.Web.Services;
namespace EmailSenderService{
 
    [WebService(Namespace = "http://example.com/webservices")]
    public class EmailSenderService : WebService 
    {

        [WebMethod]
        public void SendEmail(string emailDest, string messageText){
            try
            {
                MailMessage message = new MailMessage();
                //todo: message.From = new MailAddress("FromMailAddress");  
                SmtpClient smtp = new SmtpClient();
                message.To.Add(new MailAddress(emailDest));  
                message.Subject = "RssFeed";  
                message.IsBodyHtml = false; //to make message body as html  
                message.Body = messageText;  
                smtp.Port = 587;  
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;  
                smtp.UseDefaultCredentials = false;  
                //todo: smtp.Credentials = new NetworkCredential("FromMailAddress", "password");  
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;  
                smtp.Send(message);  
            } catch (Exception) {}     
        }
        
    }
}