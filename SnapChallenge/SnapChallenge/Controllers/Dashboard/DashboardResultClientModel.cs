using System.Collections.Generic;

namespace SnapChallenge.Controllers.Dashboard
{
    public class DashboardResultClientModel
    {
        public string Subject { get; set; }
        public string Domain { get; set; }
        public List<LearningObjectiveClientModel> LearningObjectiveClientModels { get; set; }
    }
}