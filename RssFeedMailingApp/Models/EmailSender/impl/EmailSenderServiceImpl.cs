using System.Threading.Tasks;
using WebApplication1.EmailSenderService;

namespace WebApplication1.Models.EmailSender.impl
{
    public class EmailSenderServiceImpl : EmailSenderServiceClient
    {
        public bool SendEmail(string emailSrc, string emailSrcPass)
        {
            return new EmailSenderSoapClient().SendEmail(emailSrc, emailSrcPass);
        }

        public Task<bool> SendEmailAsync(string emailSrc, string emailSrcPass)
        {
            return new EmailSenderSoapClient()
                .SendEmailAsync(emailSrc, emailSrcPass)
                .ContinueWith(task => task.Result.Body.SendEmailResult);
        }
    }
}