using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Antlr.Runtime.Tree;
using RssFeedReaderService;
using WebApplication1.Models;
using WebApplication1.Models.FeedReader;
using WebApplication1.Models.FeedReader.impl;
using WebApplication1.RssFeedReaderService;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.Models.EmailSender;
using WebApplication1.Models.EmailSender.impl;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private const char separator = ',';

        public ActionResult Index(string urlsLine, string emailsLine, string keywordsLine)
        {
            //todo: concurrent collection
            var allFeedItems = new ConcurrentStack<IFeedItem>();
            if (urlsLine != null)
            {
                string[] urls = separateStrings(urlsLine, separator);
                string[] emails = separateStrings(emailsLine, separator);
                string[] keywords;
                if (keywordsLine != null)
                {
                    keywords = separateStrings(keywordsLine, separator);
                }
                else
                {
                    keywords = new string[]{};
                }

                IFeedReaderServiceClient feedReader = new RssFeedReaderServiceClient();
                EmailSenderServiceClient emailer = new EmailSenderServiceImpl(); 

                
                var fullTasks = new List<Task>();

                foreach (var url in urls)
                {
                    Task<List<IFeedItem>> taskToGetFeed;
                    Task fullTask;
                    if (keywords.Length == 0)
                    {
                        taskToGetFeed = feedReader.GetFeedAsync(url);
                    }
                    else
                    {
                        taskToGetFeed = feedReader.GetFeedByKeywordsAsync(url, keywords);
                    }

                    

                    fullTask = taskToGetFeed.ContinueWith(getFeedTask =>
                    {
                        var currentFeedItems = getFeedTask.Result;
                        allFeedItems.PushRange(currentFeedItems.ToArray());


                        if (emailsLine != null)
                        {
                            foreach (var email in emails)
                            {
                                emailer.SendEmailAsync(email, concatenateFeedItems(currentFeedItems, url, keywords));
                            }
                        }

                    });


                    fullTasks.Add(fullTask);
                }

                Task.WaitAll(fullTasks.ToArray());
            }



            ViewBag.feedItems = allFeedItems;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Title = "About";
            ViewBag.Message = "This is a simple web application, that provides gathering of news from rss sources and sending them to emails.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.GitHubAccount = "https://github.com/altsayun";
            return View();
        }

        private string[] separateStrings(string source, char separatorChar)
        {
            var res = new List<string>();
            foreach (var s in
                source.Split(new[] {separatorChar},
                    StringSplitOptions.RemoveEmptyEntries))
            {
                var trimmed = s.Trim();
                if (trimmed != String.Empty)
                {
                    res.Add(trimmed);
                }
            }

            return res.ToArray();
        }
        private string concatenateFeedItems(List<IFeedItem> items, string source, string[] keywords)
        {
            var res = new StringBuilder($"&lt;a href=&quot;{source}&quot;&gt;{source}&lt;/a&gt;");
            if (keywords != null && keywords.Length != 0)
            {
                res.Append("&lt;br/&gt;keywords: ");
                foreach (var keyword in keywords)
                {
                    res.Append($"{keyword}, ");
                }
            }
            foreach (var item in items)
            {
                res.Append($"&lt;h3&gt;{item.title}&lt;/h3&gt;&lt;br/&gt;{item.summary}&lt;br/&gt;");
            }
            return res.ToString();
        }
    }
}