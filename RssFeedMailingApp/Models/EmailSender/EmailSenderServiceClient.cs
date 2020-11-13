using System.Threading.Tasks;

namespace WebApplication1.Models.EmailSender
{
    public interface EmailSenderServiceClient
    {
        bool SendEmail(string emailSrc, string emailSrcPass);
        Task<bool> SendEmailAsync(string emailSrc, string emailSrcPass);        
    }
}