using Microsoft.VisualBasic.FileIO;
using System;
using System.Globalization;
using System.IO;

namespace SnapChallenge.Data.DatabaseSeeder
{
    public class Work
    {
        public long SubmittedAnswerID { get; set; }
        public DateTime SubmitDateTime { get; set; }
        public bool Correct { get; set; }
        public int Progress { get; set; }
        public long UserID { get; set; }
        public long ExerciseID { get; set; }
        public float? Difficulty { get; set; }
        public string Subject { get; set; }
        public string Domain { get; set; }
        public string LearningObjective { get; set; }

        public static Work ParseCsvIntoWork(string line)
        {
            using (var parser = new TextFieldParser(new StringReader(line)))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                var values = parser.ReadFields();

                return new Work
                {
                    SubmittedAnswerID = Convert.ToInt64(values[0]),
                    SubmitDateTime = Convert.ToDateTime(string.Join("{0}Z", values[1])),
                    Correct = Convert.ToBoolean(Convert.ToInt16(values[2])),
                    Progress = Convert.ToInt32(values[3]),
                    UserID = Convert.ToInt64(values[4]),
                    ExerciseID = Convert.ToInt64(values[5]),
                    Difficulty = values[6] != "NULL"
                            ? float.Parse(values[6], CultureInfo.InvariantCulture.NumberFormat)
                            : (float?) null,
                    Subject = values[7],
                    Domain = values[8],
                    LearningObjective = values[9]
                };
            }
        }
    }
}
