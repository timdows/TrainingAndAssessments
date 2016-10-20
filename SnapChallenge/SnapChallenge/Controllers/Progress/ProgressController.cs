using System.Linq;
using System.Web.Mvc;
using SnapChallenge.Data;

namespace SnapChallenge.Controllers.Progress
{
    public class ProgressController : SnapController<SnapDataContext>
    {
        public JsonResult GetProgress()
        {
            var progress = _dataContext.SeederProgress.ToList();
            var todo = progress.Count(a_item => a_item.Total != a_item.Processed ||
                                                a_item.Processed == 0);

            return Json(new {todo, progress});
        }
    }
}