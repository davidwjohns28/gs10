using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace gs10.Models
{
    public class Team
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeamID { get; set; }
        public string TeamName { get; set; }
        public virtual ICollection<Applicant> Applicants { get; set; }
    }
}