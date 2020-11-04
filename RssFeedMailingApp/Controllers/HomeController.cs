using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RssFeedReaderService;
using WebApplication1.Models;
using WebApplication1.RssFeedReaderService;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private IFeedReaderServiceClient feedReader = new RssFeedReaderServiceClient();

        public ActionResult Index()
        {
            ViewBag.feedItems = feedReader.GetFeed("https://news.tut.by/rss");
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

        [HttpPost]
        public RedirectResult StartEmailing(string urls, string emails)
        {
            
            // feedReader = new RssFeedReaderServiceSoapClient();
            // feedItems = (IFeedItem[]) feedReader.GetFeed("https://news.tut.by/rss.html");
            return Redirect("/Home/Index");
        }
    }
}