using SnapChallenge.Data.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using SnapChallenge.Data;

namespace SnapChallenge.Controllers.Student
{
    public class StudentController : SnapController<SnapDataContext>
    {
        public JsonResult GetStudents()
        {
            var students = _dataContext.Students
                .Take(7)
                .ToList()
                .Select(StudentController.CreateStudent)
                .ToList();
            return Json(students);
        }

        public JsonResult FindStudents(long id)
        {
            var students = _dataContext.Students
                .Where(a_item => a_item.ID == id)
                .ToList()
                .Select(StudentController.CreateStudent)
                .ToList();
            return Json(students);
        }

        public JsonResult GetStudentTodayResults(long id)
        {
            var beginningOfToday = HomeController.CurrentDateTime.Date;

            var answers = _dataContext.Answers
                .Include(a_item => a_item.Exercise.Domain)
                .Include(a_item => a_item.Exercise.LearningObjective)
                .Include(a_item => a_item.Exercise.Subject)
                .Where(a_item => a_item.Student.ID == id)
                .Where(a_item => a_item.SubmitDateTime >= beginningOfToday)
                .Where(a_item => a_item.SubmitDateTime <= HomeController.CurrentDateTime)
                .OrderBy(a_item => a_item.Exercise.Domain.Name)
                .ThenBy(a_item => a_item.Exercise.LearningObjective.Name)
                .ThenBy(a_item => a_item.Exercise.Subject.Name)
                .ThenBy(a_item => a_item.SubmitDateTime)
                .ToList()
                .Select(StudentController.CreateResult)
                .ToList();
            return Json(answers);
        }

        public JsonResult GetStudentAllResults(long id)
        {
            var answers = _dataContext.Answers
                .Include(a_item => a_item.Exercise.Domain)
                .Include(a_item => a_item.Exercise.LearningObjective)
                .Include(a_item => a_item.Exercise.Subject)
                .Where(a_item => a_item.Student.ID == id)
                .Where(a_item => a_item.SubmitDateTime <= HomeController.CurrentDateTime)
                .OrderBy(a_item => a_item.Exercise.Domain.Name)
                .ThenBy(a_item => a_item.Exercise.LearningObjective.Name)
                .ThenBy(a_item => a_item.Exercise.Subject.Name)
                .ThenBy(a_item => a_item.SubmitDateTime)
                .ToList()
                .Select(StudentController.CreateResult)
                .ToList();
            return Json(answers);
        }

        private static object CreateStudent(Data.Models.Student student)
        {
            using (var dataContext = new SnapDataContext())
            {
                var amountOfAnswers = dataContext.Answers
                    .Count(a_item => a_item.Student.ID == student.ID &&
                                     a_item.SubmitDateTime <= HomeController.CurrentDateTime);

                var lastAnswer = dataContext.Answers
                    .Where(a_item => a_item.Student.ID == student.ID)
                    .Where(a_item => a_item.SubmitDateTime <= HomeController.CurrentDateTime)
                    .OrderByDescending(a_item => a_item.SubmitDateTime)
                    .FirstOrDefault();

                DateTime? lastAnswerDateTime = null;
                if (lastAnswer != null)
                {
                    lastAnswerDateTime = lastAnswer.SubmitDateTime;
                }

                return new
                {
                    student.ID,
                    student.ProfilePictureLocation,
                    AmountOfAnswers = amountOfAnswers,
                    LastAnswerDateTime = lastAnswerDateTime?.ToString("dd-MM-yyyy HH:mm")
                };
            }
        }

        private static object CreateResult(Answer answer)
        {
            return new
            {
                Domain = answer.Exercise.Domain.Name,
                LearningObjective = answer.Exercise.LearningObjective.Name,
                Subject = answer.Exercise.Subject.Name,
                SubmitDateTime = answer.SubmitDateTime.ToString("dd-MM-yyyy HH:mm"),
                Correct = answer.Correct ? "Ja" : "Nee",
                answer.Progress,
                answer.Difficulty
            };
        }
    }
}