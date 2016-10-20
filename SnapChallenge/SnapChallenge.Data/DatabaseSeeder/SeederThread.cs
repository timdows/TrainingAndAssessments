using SnapChallenge.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace SnapChallenge.Data.DatabaseSeeder
{
    public class SeederThread
    {
        public void DoImport(List<Work> works)
        {
            using (var dataContext = new SnapDataContext())
            {
                var subjectList = dataContext.Subjects.ToList();
                var domainList = dataContext.Domains.ToList();
                var learningObjectiveList = dataContext.LearningObjectives.ToList();

                var count = 0;
                var progressCount = 0;
                var exerciseIDs = works.Select(a_item => a_item.ExerciseID)
                    .Distinct()
                    .ToList();
                Seeder.ProgressInit(dataContext, Seeder.Exercises, exerciseIDs.Count());
                foreach (var exerciseID in exerciseIDs)
                {
                    var exercise = works.First(a_item => a_item.ExerciseID == exerciseID);

                    var subject = subjectList.SingleOrDefault(a_item => a_item.Name == exercise.Subject);
                    var domain = domainList.Single(a_item => a_item.Name == exercise.Domain);
                    var learningObjective =
                        learningObjectiveList.Single(a_item => a_item.Name == exercise.LearningObjective);

                    dataContext.Exercises.Add(new Exercise
                    {
                        ID = exercise.ExerciseID,
                        Subject = subject,
                        Domain = domain,
                        LearningObjective = learningObjective
                    });

                    if (++count == 100)
                    {
                        count = 0;
                        progressCount += 100;
                        dataContext.SaveChanges();
                        Seeder.ProgressReport(dataContext, Seeder.Exercises, progressCount);
                    }
                }
                dataContext.SaveChanges();
                Seeder.ProgressReport(dataContext, Seeder.Exercises, exerciseIDs.Count());

                var students = dataContext.Students.ToList();
                var exercises = dataContext.Exercises.ToList();

                count = 0;
                progressCount = 0;
                Seeder.ProgressInit(dataContext, Seeder.Answers, works.Count());
                foreach (var work in works)
                {
                    var student = students.Single(a_item => a_item.ID == work.UserID);
                    var exersise = exercises.Single(a_item => a_item.ID == work.ExerciseID);

                    dataContext.Answers.Add(new Answer
                    {
                        ID = work.SubmittedAnswerID,
                        SubmitDateTime = work.SubmitDateTime,
                        Correct = work.Correct,
                        Progress = work.Progress,
                        Difficulty = work.Difficulty,
                        Exercise = exersise,
                        Student = student
                    });

                    if (++count == 100)
                    {
                        count = 0;
                        progressCount += 100;
                        dataContext.SaveChanges();
                        Seeder.ProgressReport(dataContext, Seeder.Answers, progressCount);
                    }
                }
                dataContext.SaveChanges();
                Seeder.ProgressReport(dataContext, Seeder.Answers, works.Count());
            }
        }
    }
}