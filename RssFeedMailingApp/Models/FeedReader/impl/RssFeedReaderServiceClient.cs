using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.RssFeedReaderService;

namespace WebApplication1.Models.FeedReader.impl
{
    public class RssFeedReaderServiceClient : IFeedReaderServiceClient
    {
        public List<IFeedItem> GetFeed(string urlText)
        {
            var rssFeedItemsDto = new RssFeedReaderServiceSoapClient().GetFeed(urlText);
        
            var rssFeedItemsList = new List<IFeedItem>();
            foreach (var feedItemDto in rssFeedItemsDto)
            {
                rssFeedItemsList.Add(new RssFeedItem(feedItemDto.title, feedItemDto.summary, feedItemDto.source));
            }
            return rssFeedItemsList;
        }

        public Task<List<IFeedItem>> GetFeedAsync(string urlText)
        {            
            return new RssFeedReaderServiceSoapClient().GetFeedAsync(urlText)
                .ContinueWith(task =>
            {
                var rssFeedItems = task.Result.Body.GetFeedResult;
                var rssFeedItemsList = new List<IFeedItem>();
                foreach (var feedItemDto in rssFeedItems)
                {
                    rssFeedItemsList.Add(new RssFeedItem(feedItemDto.title, feedItemDto.summary, feedItemDto.source));
                }

                return rssFeedItemsList;
            });
        }

        public List<IFeedItem> GetFeedByKeywords(string urlText, string[] keywords)
        {
            var rssFeedItemsDto = new RssFeedReaderServiceSoapClient().GetFeedByKeywords(urlText, convertArrayToArrayOfString(keywords));

            var rssFeedItemsList = new List<IFeedItem>();
            foreach (var feedItemDto in rssFeedItemsDto)
            {
                rssFeedItemsList.Add(new RssFeedItem(feedItemDto.title, feedItemDto.summary, feedItemDto.source));
            }
            return rssFeedItemsList;
        }     
        public Task<List<IFeedItem>> GetFeedByKeywordsAsync(string urlText, string[] keywords)
        {
            return new RssFeedReaderServiceSoapClient().GetFeedByKeywordsAsync(urlText, convertArrayToArrayOfString(keywords))
                .ContinueWith(task =>
                {
                    var rssFeedItems = task.Result.Body.GetFeedByKeywordsResult;
                    var rssFeedItemsList = new List<IFeedItem>();
                    foreach (var feedItemDto in rssFeedItems)
                    {
                        rssFeedItemsList.Add(new RssFeedItem(feedItemDto.title, feedItemDto.summary, feedItemDto.source));
                    }

                    return rssFeedItemsList;
                });
        }

        private ArrayOfString convertArrayToArrayOfString(string[] strings)
        {
            //todo: decent array to ArrayOfStrings conversion
            var arrayOfString = new ArrayOfString();
            foreach (var s in strings)
            {
                arrayOfString.Add(s);
            }
            return arrayOfString;
        }
    }
}