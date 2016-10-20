using System;
using System.Web.Mvc;

namespace SnapChallenge.Controllers
{
    public class HomeController : Controller
    {
        public static readonly DateTime CurrentDateTime = Convert.ToDateTime("2015-03-24T11:30:00.000Z");

        public ActionResult Index()
        {
            return View();
        }
    }
}