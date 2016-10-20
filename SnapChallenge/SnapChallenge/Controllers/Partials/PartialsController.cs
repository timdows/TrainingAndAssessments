using System.Web.Mvc;

namespace SnapChallenge.Controllers.Partials
{
    public class PartialsController : Controller
    {
        public PartialViewResult Header()
        {
            this.ViewBag.LogoTitle = "Snap Challenge";
            return PartialView();
        }

        public PartialViewResult Dashboard()
        {
            return PartialView();
        }

        public PartialViewResult SidebarLeft()
        {
            return PartialView();
        }

        public PartialViewResult SidebarRight()
        {
            return PartialView();
        }

        public PartialViewResult DashboardWelcome()
        {
            this.ViewBag.LocalDateTime = HomeController.CurrentDateTime.ToLocalTime();
            return PartialView();
        }

        public PartialViewResult DashboardTeacher()
        {
            return PartialView();
        }

        public PartialViewResult DashboardStudent()
        {
            return PartialView();
        }
    }
}