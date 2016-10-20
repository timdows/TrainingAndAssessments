namespace SnapChallenge.Controllers.Dashboard
{
    public class LearningObjectiveClientModel
    {
        public string Name { get; set; }
        public int AmountOfAnswers { get; set; }
        public int AmoutCorrect { get; set; }
        public int AmoutIncorrect { get; set; }
        public int SumOfProgress { get; set; }
        public float? SumOfDifficulty { get; set; }
    }
}
