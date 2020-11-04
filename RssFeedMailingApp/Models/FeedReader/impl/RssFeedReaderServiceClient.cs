using System;
using System.Collections.Generic;
using WebApplication1.RssFeedReaderService;

namespace WebApplication1.Models
{
    public class RssFeedReaderServiceClient : IFeedReaderServiceClient
    {
        public IFeedItem[] GetFeed(string urlText)
        {
            var rssFeedItemsDto = new RssFeedReaderServiceSoapClient().GetFeed(urlText);

            var rssFeedItemsList = new List<RssFeedItem>();
            foreach (var feedItemDto in rssFeedItemsDto)
            {
                rssFeedItemsList.Add(new RssFeedItem(feedItemDto.title, feedItemDto.summary));
            }
            return rssFeedItemsList.ToArray();
        }
    }
}