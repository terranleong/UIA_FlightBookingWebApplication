using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UIA_FlightBookingWebApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.TitleExplanation = "UIA's Company Brief Introduction.";
            ViewBag.UIADetail = "Ukraine International Airlines (UIA) is the flagship carrier and largest airline in Ukraine. It operates domestic and international passenger flights and cargo services to Europe, the Middle East, the United States, and Asia.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "UIA's contact page.";
            return View();
        }
    }
}