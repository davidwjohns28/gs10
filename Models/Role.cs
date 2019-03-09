using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace gs10.Models
{
    public class Role
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public virtual ICollection<Applicant> Applicants { get; set; }
    }
}