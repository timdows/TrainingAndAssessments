using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using SnapChallenge.Data;
using SnapChallenge.Data.Models;

namespace SnapChallenge.Controllers.Dashboard
{
    public class DashboardController : SnapController<SnapDataContext>
    {
        public JsonResult GetTodayResults()
        {
            var beginningOfToday = HomeController.CurrentDateTime.Date;

            var answers = _dataContext.Answers
                .Include(a_item => a_item.Exercise.Domain)
                .Include(a_item => a_item.Exercise.LearningObjective)
                .Include(a_item => a_item.Exercise.Subject)
                .Where(a_item => a_item.SubmitDateTime >= beginningOfToday)
                .Where(a_item => a_item.SubmitDateTime <= HomeController.CurrentDateTime)
                .OrderBy(a_item => a_item.Exercise.Domain.Name)
                .ThenBy(a_item => a_item.Exercise.LearningObjective.Name)
                .ThenBy(a_item => a_item.Exercise.Subject.Name)
                .ThenBy(a_item => a_item.SubmitDateTime)
                .ToList();

            return GetResults(answers);
        }

        public JsonResult GetYesterdayResults()
        {
            var beginningOfYesterday = HomeController.CurrentDateTime.Date.AddDays(-1);
            var endOfYesterday = beginningOfYesterday.AddDays(1);

            var answers = _dataContext.Answers
                .Include(a_item => a_item.Exercise.Domain)
                .Include(a_item => a_item.Exercise.LearningObjective)
                .Include(a_item => a_item.Exercise.Subject)
                .Where(a_item => a_item.SubmitDateTime >= beginningOfYesterday)
                .Where(a_item => a_item.SubmitDateTime < endOfYesterday)
                .OrderBy(a_item => a_item.Exercise.Domain.Name)
                .ThenBy(a_item => a_item.Exercise.LearningObjective.Name)
                .ThenBy(a_item => a_item.Exercise.Subject.Name)
                .ThenBy(a_item => a_item.SubmitDateTime)
                .ToList();

            return GetResults(answers);
        }

        public JsonResult GetAllResults()
        {
            var answers = _dataContext.Answers
                .Include(a_item => a_item.Exercise.Domain)
                .Include(a_item => a_item.Exercise.LearningObjective)
                .Include(a_item => a_item.Exercise.Subject)
                .Where(a_item => a_item.SubmitDateTime <= HomeController.CurrentDateTime)
                .OrderBy(a_item => a_item.Exercise.Domain.Name)
                .ThenBy(a_item => a_item.Exercise.LearningObjective.Name)
                .ThenBy(a_item => a_item.Exercise.Subject.Name)
                .ThenBy(a_item => a_item.SubmitDateTime)
                .ToList();

            return GetResults(answers);
        }

        private JsonResult GetResults(List<Answer> answers)
        {
            var dashboardResultClientModels = new List<DashboardResultClientModel>();

            var subjects = answers
                .Select(a_item => a_item.Exercise.Subject)
                .Distinct()
                .ToList();

            foreach (var subject in subjects)
            {
                var domains = answers
                    .Where(a_item => a_item.Exercise.Subject.ID == subject.ID)
                    .Select(a_item => a_item.Exercise.Domain)
                    .Distinct()
                    .ToList();

                foreach (var domain in domains)
                {
                    var learningObjectiveClientModels = new List<LearningObjectiveClientModel>();

                    var learningObjectives = answers
                        .Where(a_item => a_item.Exercise.Subject.ID == subject.ID)
                        .Where(a_item => a_item.Exercise.Domain.ID == domain.ID)
                        .Select(a_item => a_item.Exercise.LearningObjective)
                        .Distinct()
                        .ToList();

                    foreach (var learningObjective in learningObjectives)
                    {
                        learningObjectiveClientModels.Add(new LearningObjectiveClientModel
                        {
                            Name = learningObjective.Name,
                            AmountOfAnswers = answers
                                .Count(a_item => a_item.Exercise.Subject.ID == subject.ID &&
                                                 a_item.Exercise.Domain.ID == domain.ID &&
                                                 a_item.Exercise.LearningObjective.ID == learningObjective.ID),
                            AmoutCorrect = answers
                                .Count(a_item => a_item.Exercise.Subject.ID == subject.ID &&
                                                 a_item.Exercise.Domain.ID == domain.ID &&
                                                 a_item.Exercise.LearningObjective.ID == learningObjective.ID &&
                                                 a_item.Correct),
                            AmoutIncorrect = answers
                                .Count(a_item => a_item.Exercise.Subject.ID == subject.ID &&
                                                 a_item.Exercise.Domain.ID == domain.ID &&
                                                 a_item.Exercise.LearningObjective.ID == learningObjective.ID &&
                                                 a_item.Correct == false),
                            SumOfProgress = answers
                                .Where(a_item => a_item.Exercise.Subject.ID == subject.ID &&
                                                 a_item.Exercise.Domain.ID == domain.ID &&
                                                 a_item.Exercise.LearningObjective.ID == learningObjective.ID)
                                .Select(a_item => a_item.Progress)
                                .Sum(),
                            SumOfDifficulty = answers
                                .Where(a_item => a_item.Exercise.Subject.ID == subject.ID &&
                                                 a_item.Exercise.Domain.ID == domain.ID &&
                                                 a_item.Exercise.LearningObjective.ID == learningObjective.ID)
                                .Select(a_item => a_item.Difficulty)
                                .Sum()
                        });
                    }

                    dashboardResultClientModels.Add(new DashboardResultClientModel
                    {
                        Subject = subject.Name,
                        Domain = domain.Name,
                        LearningObjectiveClientModels = learningObjectiveClientModels
                    });
                }
            }

            return Json(dashboardResultClientModels);
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