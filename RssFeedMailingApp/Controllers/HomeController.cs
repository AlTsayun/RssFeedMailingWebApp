using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public async Task<ActionResult> Index(string urlsLine, string emailsLine, string keywordsLine)
        {
            //todo: concurrent collection
            List<IFeedItem> allFeedItems = new List<IFeedItem>();
            if (urlsLine != null && emailsLine != null && keywordsLine != null)
            {
                var urls = separateStrings(urlsLine, separator);
                var emails = separateStrings(emailsLine, separator);
                var keywords = separateStrings(keywordsLine, separator);

                IFeedReaderServiceClient feedReader = new RssFeedReaderServiceClient();
                EmailSenderServiceClient emailer = new EmailSenderServiceImpl(); 

                
                var getFeedTasks = new List<Task<List<IFeedItem>>>();

                foreach (var url in urls)
                {
                    Task<List<IFeedItem>> task;
                    if (keywords.Length == 0)
                    {
                        task = feedReader.GetFeedAsync(url);
                    }
                    else
                    {
                        task = feedReader.GetFeedByKeywordsAsync(url, keywords);
                    }

                    task.ContinueWith(getFeedTask =>
                    {
                        var currentFeedItems = getFeedTask.Result;
                        allFeedItems.AddRange(currentFeedItems);
                        
                        //todo: email feed to all emails
                        // foreach (var email in emails)
                        // {
                        //     
                        // }
                    });
                    getFeedTasks.Add(task);
                }

            }
            
            
            ViewBag.feedItems = allFeedItems;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
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
    }
}