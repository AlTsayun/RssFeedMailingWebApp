using System.Threading.Tasks;
using WebApplication1.EmailSenderService;

namespace WebApplication1.Models.EmailSender.impl
{
    public class EmailSenderServiceImpl : EmailSenderServiceClient
    {
        public bool SendEmail(string emailSrc, string emailSrcPass, string emailDest, string subject, string messageText)
        {
            return new EmailSenderSoapClient().SendEmail(emailSrc, emailSrcPass, emailDest, subject, messageText);
        }

        public async Task<bool> SendEmailAsync(string emailSrc, string emailSrcPass, string emailDest, string subject, string messageText)
        {
            var sendEmailTask = new EmailSenderSoapClient().SendEmailAsync(emailSrc, emailSrcPass, emailDest, subject, messageText);
            return await Task<bool>.Run(() => sendEmailTask.Result.Body.SendEmailResult);
        }
    }
}