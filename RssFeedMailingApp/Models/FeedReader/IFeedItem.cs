namespace WebApplication1.Models.FeedReader
{
    public interface IFeedItem
    {
        string title { get; set; }
        string summary { get; set; }
        string source { get; set; }
    }
}