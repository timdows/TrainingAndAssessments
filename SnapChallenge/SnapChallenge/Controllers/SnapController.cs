using System.Data.Entity;
using System.Web.Mvc;

namespace SnapChallenge.Controllers
{
    public abstract class SnapController : Controller
    {
        public new JsonResult Json(object data)
        {
            return base.Json(data, JsonRequestBehavior.AllowGet);
        }
    }

    public abstract class SnapController<TDataContext> : SnapController
        where TDataContext : DbContext, new()
    {
        protected readonly TDataContext _dataContext;

        protected SnapController()
        {
            _dataContext = new TDataContext();
        }

        protected SnapController(TDataContext dataContext)
        {
            _dataContext = dataContext;
        }
    }
}