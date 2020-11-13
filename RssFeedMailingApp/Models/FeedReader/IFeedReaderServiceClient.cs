using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication1.Models.FeedReader
{
    public interface IFeedReaderServiceClient
    {
        List<IFeedItem> GetFeed(string urlText);
        Task<List<IFeedItem>> GetFeedAsync(string urlText);
        List<IFeedItem> GetFeedByKeywords(string urlText, string[] keywords);
        Task<List<IFeedItem>> GetFeedByKeywordsAsync(string urlText, string[] keywords);
    }
    
}