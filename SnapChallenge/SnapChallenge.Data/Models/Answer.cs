using System;

namespace SnapChallenge.Data.Models
{
    public class Answer : SqlBaseNoIdentity
    {
        public DateTime SubmitDateTime { get; set; }
        public bool Correct { get; set; }
        public int Progress { get; set; }
        public float? Difficulty { get; set; }
        public Student Student { get; set; }
        public Exercise Exercise { get; set; }
    }
}