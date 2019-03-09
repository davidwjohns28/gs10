using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace gs10.Models
{
    public class State
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StateID { get; set; }
        public string StateName { get; set; }
        public virtual ICollection<Applicant> Applicants { get; set; }
    }
}