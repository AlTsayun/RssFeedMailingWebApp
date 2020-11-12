using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Services;
using System.Xml;
using System.ServiceModel.Syndication;
namespace RssFeedReaderService{
 
    [WebService(Namespace = "http://example.com/webservices")]
    public class RssFeedReaderService : WebService 
    {

        [WebMethod]
        public RssFeedItem[] GetFeed(string urlText){
            if (!String.IsNullOrEmpty(urlText))
            {
                using (var reader = XmlReader.Create(urlText, new XmlReaderSettings()
                {
                    DtdProcessing=DtdProcessing.Parse
                }))
                {
                    var formatter = new Rss20FeedFormatter();
                    formatter.ReadFrom(reader);

                    List<RssFeedItem> items = new List<RssFeedItem>();
                    foreach (SyndicationItem item in formatter.Feed.Items)
                    {
                        items.Add(new RssFeedItem(item.Title.Text, item.Summary.Text, urlText));
                    }

                    return items.ToArray();
                }
            }
            
            return Array.Empty<RssFeedItem>();
        }
        
        
        [WebMethod]
        public RssFeedItem[] GetFeedByKeywords(string urlText, string[] keywords){
            if (!String.IsNullOrEmpty(urlText))
            {
                using (var reader = XmlReader.Create(urlText, new XmlReaderSettings()
                {
                    DtdProcessing=DtdProcessing.Parse
                }))
                {
                    var formatter = new Rss20FeedFormatter();
                    formatter.ReadFrom(reader);

                    List<RssFeedItem> items = new List<RssFeedItem>();
                    foreach (SyndicationItem item in formatter.Feed.Items)
                    {
                        foreach (var keyword in keywords)
                        {
                            if (CultureInfo.InvariantCulture.CompareInfo.IndexOf(item.Title.Text, keyword, CompareOptions.IgnoreCase) >= 0
                                || CultureInfo.InvariantCulture.CompareInfo.IndexOf(item.Summary.Text, keyword, CompareOptions.IgnoreCase) >= 0)
                            {
                                items.Add(new RssFeedItem(item.Title.Text, item.Summary.Text, urlText));
                            }                            
                        }
                    }

                    return items.ToArray();
                }
            }
            
            return Array.Empty<RssFeedItem>();
        }
    }

    public class RssFeedItem
    {
        public string title { get; set; }
        public string summary { get; set; }
        public string source { get; set; }
    
        public RssFeedItem(){ }
    
        public RssFeedItem(string title, string summary, string source)
        {
            this.source = source;
            this.title = title;
            this.summary = summary;
        }
    }
}