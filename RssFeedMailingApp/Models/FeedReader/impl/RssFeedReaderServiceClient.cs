using System.Collections.Generic;
using WebApplication1.RssFeedReaderService;

namespace WebApplication1.Models.FeedReader.impl
{
    public class RssFeedReaderServiceClient : IFeedReaderServiceClient
    {
        public IFeedItem[] GetFeed(string urlText)
        {
            var rssFeedItemsDto = new RssFeedReaderServiceSoapClient().GetFeed(urlText);

            var rssFeedItemsList = new List<RssFeedItem>();
            foreach (var feedItemDto in rssFeedItemsDto)
            {
                rssFeedItemsList.Add(new RssFeedItem(feedItemDto.title, feedItemDto.summary, feedItemDto.source));
            }
            return rssFeedItemsList.ToArray();
        }

        public IFeedItem[] GetFeedByKeyword(string urlText, string keyword)
        {
            var rssFeedItemsDto = new RssFeedReaderServiceSoapClient().GetFeedByKeyword(urlText, keyword);

            var rssFeedItemsList = new List<RssFeedItem>();
            foreach (var feedItemDto in rssFeedItemsDto)
            {
                rssFeedItemsList.Add(new RssFeedItem(feedItemDto.title, feedItemDto.summary, feedItemDto.source));
            }
            return rssFeedItemsList.ToArray();
        }
    }
}