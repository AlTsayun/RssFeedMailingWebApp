using System.Threading.Tasks;

namespace WebApplication1.Models.EmailSender
{
    public interface EmailSenderServiceClient
    {
        bool SendEmail(string emailSrc, string emailSrcPass, string emailDest, string subject,
            string messageText);
        Task<bool> SendEmailAsync(string emailSrc, string emailSrcPass, string emailDest, string subject,
            string messageText);        
    }
}