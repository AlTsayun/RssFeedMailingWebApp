namespace WebApplication1.Models.FeedReader.impl
{
    public class RssFeedItem : IFeedItem
    {
        public RssFeedItem(string title, string summary, string source)
        {
            this.source = source;
            this.title = title;
            this.summary = summary;
        }

        public RssFeedItem()
        {
        }

        public string title { get; set; }
        public string summary { get; set; }
        public string source { get; set; }
    }
}