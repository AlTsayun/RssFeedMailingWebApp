namespace WebApplication1.Models.FeedReader
{
    public interface IFeedReaderServiceClient
    {
        IFeedItem[] GetFeed(string urlText);
        IFeedItem[] GetFeedByKeyword(string urlText, string keyword);
    }
    
}