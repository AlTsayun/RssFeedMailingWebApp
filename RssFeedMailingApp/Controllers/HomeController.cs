using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RssFeedReaderService;
using WebApplication1.Models;
using WebApplication1.Models.FeedReader;
using WebApplication1.Models.FeedReader.impl;
using WebApplication1.RssFeedReaderService;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index(string urls, string emails, string keywords)
        {
            IFeedItem[] feedItems;
            if (urls != null && emails != null && keywords != null)
            {
             
                var feedReader = new RssFeedReaderServiceClient();
                // feedItems = feedReader.GetFeedByKeyword("https://news.tut.by/rss","");   
                feedItems = feedReader.GetFeed("https://news.tut.by/rss");   
            }
            else
            {
                feedItems = new IFeedItem[]{};
            }
            ViewBag.feedItems = feedItems;
            return View();
        }

        // public ActionResult Index(IFeedItem[] feedItems)
        // {
        //     ViewBag.feedItems = feedItems;
        //     return View();
        // }


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
    }
}