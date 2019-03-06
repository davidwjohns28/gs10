using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

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