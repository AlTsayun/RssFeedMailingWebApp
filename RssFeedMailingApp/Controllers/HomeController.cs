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
        private IFeedReaderServiceClient feedReader = new RssFeedReaderServiceClient();
        private IFeedItem[] feedItems = {};

        public ActionResult Index()
        {
            ViewBag.feedItems = new IFeedItem[]{};
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

        [HttpPost]
        public RedirectResult StartEmailing(string urls, string emails, string keywords)
        {
            feedItems = feedReader.GetFeed("https://news.tut.by/rss");
            
            return Redirect("/");
        }
    }
}