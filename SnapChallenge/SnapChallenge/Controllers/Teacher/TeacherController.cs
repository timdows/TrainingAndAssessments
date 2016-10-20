using System.Linq;
using System.Web.Mvc;
using SnapChallenge.Data;

namespace SnapChallenge.Controllers.Teacher
{
    public class TeacherController : SnapController<SnapDataContext>
    {
        public JsonResult GetTeachers()
        {
            var teachers = _dataContext.Teachers.ToList();
            return Json(teachers);
        }

        public JsonResult GetTeacher(long id)
        {
            var teacher = _dataContext.Teachers.Single(a_item => a_item.ID == id);
            return Json(teacher);
        }
    }
}
