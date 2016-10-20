using System.ComponentModel.DataAnnotations.Schema;

namespace SnapChallenge.Data.Models
{
    public class Teacher : SqlBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePictureLocation { get; set; }

        [NotMapped]
        public string FullName
        {
            get { return $"{this.FirstName} {this.LastName}"; }
        }
    }
}
