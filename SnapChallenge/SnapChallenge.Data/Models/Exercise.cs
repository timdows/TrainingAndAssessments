namespace SnapChallenge.Data.Models
{
    public class Exercise : SqlBaseNoIdentity
    {
        public Subject Subject { get; set; }
        public Domain Domain { get; set; }
        public LearningObjective LearningObjective { get; set; }
    }
}
