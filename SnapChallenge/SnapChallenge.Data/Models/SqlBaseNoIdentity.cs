using System.ComponentModel.DataAnnotations.Schema;

namespace SnapChallenge.Data.Models
{
    public abstract class SqlBaseNoIdentity : SqlBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public new long ID { get; set; }
    }
}
