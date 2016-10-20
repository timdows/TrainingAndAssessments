using System.Linq;
using System.Web.Mvc;
using SnapChallenge.Data;

namespace SnapChallenge.Controllers
{
    public class DataController : SnapController
    {
        public static DataController Method = null;

        public JsonResult MessagesNotifications()
        {
            return Json(true);
        }

        public JsonResult Bestselling()
        {
            return Json(true);
        }

        public JsonResult RecentItems()
        {
            return Json(true);
        }

        public JsonResult Todo()
        {
            return Json(true);
        }

        public JsonResult Test()
        {
            using (var dataContext = new SnapDataContext())
            {
                var teachers = dataContext.Teachers.ToList();
                return Json(teachers);
            }
        }
    }
}