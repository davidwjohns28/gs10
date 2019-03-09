using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace gs10.Models
{
    public class SpanishLevel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SpanishLevelID { get; set; }
        public string SpanishLevelName { get; set; }
        public virtual ICollection<Applicant> Applicants { get; set; }
    }
}