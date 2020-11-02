using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Services;
using System.Xml;
using System.ServiceModel.Syndication;

namespace RssFeedReaderService{
 
    public class RssFeedReaderService : WebService 
    {

        [WebMethod]
        public  RssFeedItem[] GetFeed(string urlText){
            if (!String.IsNullOrEmpty(urlText))
            {
                using (var feedReader = XmlReader.Create(urlText))
                {
                    List<RssFeedItem> items = new List<RssFeedItem>();
                    SyndicationFeed channel = SyndicationFeed.Load(feedReader);
                    foreach (SyndicationItem item in channel.Items)
                    {
                        items.Add(new RssFeedItem(item.Title.Text, item.Summary.Text));
                    }
                    return items.ToArray();
                }
            }

            return Array.Empty<RssFeedItem>();
        }
    }

    public class RssFeedItem
    {
        private string title { get; set; }
        private string summary { get; set; }

        public RssFeedItem(){ }

        public RssFeedItem(string title, string summary)
        {
            this.title = title;
            this.summary = summary;
        }
    }
}