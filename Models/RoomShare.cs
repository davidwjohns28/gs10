using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace gs10.Models
{
    public class RoomShare
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoomShareID { get; set; }
        public string RoomShareName { get; set; }
        public virtual ICollection<Applicant> Applicants { get; set; }
    }
}