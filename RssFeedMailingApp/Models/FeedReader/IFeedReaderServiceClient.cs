namespace WebApplication1.Models
{
    public interface IFeedReaderServiceClient
    {
        IFeedItem[] GetFeed(string urlText);
    }
    
}