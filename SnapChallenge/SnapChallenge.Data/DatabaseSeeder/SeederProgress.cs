using SnapChallenge.Data.Models;

namespace SnapChallenge.Data.DatabaseSeeder
{
    public class SeederProgress : SqlBase
    {
        public string Name { get; set; }
        public long Total { get; set; }
        public long Processed { get; set; }
    }
}
