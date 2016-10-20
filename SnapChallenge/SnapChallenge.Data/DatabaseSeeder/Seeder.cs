using System.Reflection;
using System.Data.Entity;
using System.IO;
using System.Linq;
using SnapChallenge.Data.Models;
using System.Threading;

namespace SnapChallenge.Data.DatabaseSeeder
{
    public class Seeder : CreateDatabaseIfNotExists<SnapDataContext>
    {
        private const string Students = "Students";
        private const string Domains = "Domains";
        private const string Subjects = "Subjects";
        private const string LearningObjectives = "LearningObjectives";
        public const string Exercises = "Exercises";
        public const string Answers = "Answers";

        protected override void Seed(SnapDataContext dataContext)
        {
            base.Seed(dataContext);

            dataContext.Teachers.Add(new Teacher
            {
                FirstName = "Tim",
                LastName = "Theeuwes",
                ProfilePictureLocation = @"teachers/tim.jpg"
            });

            Seeder.ProgressCreate(dataContext, Seeder.Students);
            Seeder.ProgressCreate(dataContext, Seeder.Domains);
            Seeder.ProgressCreate(dataContext, Seeder.Subjects);
            Seeder.ProgressCreate(dataContext, Seeder.LearningObjectives);
            Seeder.ProgressCreate(dataContext, Seeder.Exercises);
            Seeder.ProgressCreate(dataContext, Seeder.Answers);

            var assembly = Assembly.GetExecutingAssembly();
            const string resourceName = "SnapChallenge.Data.Resources.work.csv";

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var streamReader = new StreamReader(stream))
            {
                var content = streamReader
                    .ReadToEnd()
                    .Split(new char[] {'\r', '\n'}, System.StringSplitOptions.RemoveEmptyEntries);

                var works = content
                    .Skip(1)
                    .Select(Work.ParseCsvIntoWork)
                    .ToList();

                var studentIDs = works.Select(a_item => a_item.UserID)
                    .Distinct()
                    .ToList();
                Seeder.ProgressInit(dataContext, Seeder.Students, studentIDs.Count());
                foreach (var studentID in studentIDs)
                {
                    var profilePicture = studentID%10;
                    dataContext.Students.Add(new Student
                    {
                        ID = studentID,
                        ProfilePictureLocation = $@"profile-pics/{profilePicture}.jpg"
                    });
                }
                Seeder.ProgressReport(dataContext, Seeder.Students, studentIDs.Count());

                var domains = works.Select(a_item => a_item.Domain)
                    .Distinct()
                    .ToList();
                Seeder.ProgressInit(dataContext, Seeder.Domains, domains.Count());
                foreach (var domain in domains)
                {
                    if (domain == null)
                    {
                        continue;
                    }

                    dataContext.Domains.Add(new Domain
                    {
                        Name = domain
                    });
                }
                Seeder.ProgressReport(dataContext, Seeder.Domains, domains.Count());

                var subjects = works.Select(a_item => a_item.Subject)
                    .Distinct()
                    .ToList();
                Seeder.ProgressInit(dataContext, Seeder.Subjects, subjects.Count());
                foreach (var subject in subjects)
                {
                    if (subject == null)
                    {
                        continue;
                    }

                    dataContext.Subjects.Add(new Subject
                    {
                        Name = subject
                    });
                }
                Seeder.ProgressReport(dataContext, Seeder.Subjects, subjects.Count());

                var learningObjectives = works.Select(a_item => a_item.LearningObjective)
                    .Distinct()
                    .ToList();
                Seeder.ProgressInit(dataContext, Seeder.LearningObjectives, learningObjectives.Count());
                foreach (var learningObjective in learningObjectives)
                {
                    if (learningObjective == null)
                    {
                        continue;
                    }

                    dataContext.LearningObjectives.Add(new LearningObjective
                    {
                        Name = learningObjective
                    });
                }
                dataContext.SaveChanges();
                Seeder.ProgressReport(dataContext, Seeder.LearningObjectives, learningObjectives.Count());

                var seederThread = new SeederThread();
                var importThread = new Thread(() => seederThread.DoImport(works)) {IsBackground = true};
                importThread.Start();
            }
        }

        public static void ProgressInit(SnapDataContext dataContext, string name, long total)
        {
            var progress = dataContext.SeederProgress.Single(a_item => a_item.Name == name);
            progress.Total = total;
            dataContext.SaveChanges();
        }

        public static void ProgressReport(SnapDataContext dataContext, string name, long processed)
        {
            var progress = dataContext.SeederProgress.Single(a_item => a_item.Name == name);
            progress.Processed = processed;
            dataContext.SaveChanges();
        }

        private static void ProgressCreate(SnapDataContext dataContext, string name)
        {
            dataContext.SeederProgress.Add(new SeederProgress
            {
                Name = name,
                Total = 0,
                Processed = 0
            });
            dataContext.SaveChanges();
        }
    }
}