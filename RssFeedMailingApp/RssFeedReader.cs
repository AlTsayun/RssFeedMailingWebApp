using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web.Services;
using System.Xml;
using System.ServiceModel.Syndication;
using System.Web.UI.WebControls.WebParts;
using WebApplication1.Models;

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
                        items.Add(new RssFeedItem(item.Title.Text, item.Summary.Text));
                    }

                    return items.GetRange(0,70).ToArray();
                    
                    // todo: fix System.ServiceModel.QuotaExceededException 
                    // return items.ToArray();
                }
            }
            
            return Array.Empty<RssFeedItem>();
        }
    }

    public class RssFeedItem
    {
        public string title { get; set; }
        public string summary { get; set; }
    
        public RssFeedItem(){ }
    
        public RssFeedItem(string title, string summary)
        {
            this.title = title;
            this.summary = summary;
        }
    }
}