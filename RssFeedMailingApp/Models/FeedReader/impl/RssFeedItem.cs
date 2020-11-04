namespace WebApplication1.Models
{
    public class RssFeedItem : IFeedItem
    {
        public RssFeedItem(string title, string summary)
        {
            this.title = title;
            this.summary = summary;
        }

        public RssFeedItem()
        {
        }

        public string title { get; set; }
        public string summary { get; set; }
    }
}