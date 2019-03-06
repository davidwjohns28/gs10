using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace gs10.Models
{
    public class Applicant
    {
        const string FIRST_DATE = "1/1/1900 12:00:00 AM";

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ApplicantID { get; set; }

        // foreign key to ApplicationUser.Id--need to explicitly create foreign key constraint via sql server.
        [Display(Name = "UserId", Order = 1)]
        [HiddenInput(DisplayValue = false)]
        public string UserId { get; set; }

        [Display(Name = "Year", Order = 2)]
        public string Year { get; set; }

        [Display(Name = "Team", Order = 3)]
        [ForeignKey("Team")]
        public int? TeamID { get; set; }

        [Required]
        [Display(Name = "First Name", Order = 4)]
        [StringLength(30, ErrorMessage = "First Name cannot be longer than 30 characters.")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name/Initial", Order = 5)]
        [StringLength(30, ErrorMessage = "Middle Name cannot be longer than 30 characters.")]
        public string MiddleName { get; set; }

        [Required]
        [Display(Name = "Last Name", Order = 6)]
        [StringLength(30, ErrorMessage = "Last Name cannot be longer than 30 characters.")]
        public string LastName { get; set; }

        [Display(Name = "Preferred Name", Order = 7)]
        [StringLength(30, ErrorMessage = "Preferred Name cannot be longer than 30 characters.")]
        public string PreferredName { get; set; }

        [Display(Name = "Address1", Order = 8)]
        [StringLength(50, ErrorMessage = "Address1 cannot be longer than 50 characters.")]
        public string Address1 { get; set; }

        [Display(Name = "Address2", Order = 9)]
        [StringLength(50, ErrorMessage = "Address2 cannot be longer than 50 characters.")]
        public string Address2 { get; set; }

        [Display(Name = "City", Order = 10)]
        [StringLength(50, ErrorMessage = "City cannot be longer than 50 characters.")]
        public string City { get; set; }

        [Display(Name = "State", Order = 11)]
        [ForeignKey("State")]
        public int? StateID { get; set; }

        [Display(Name = "Zip Code", Order = 12)]
        [StringLength(10, ErrorMessage = "Zip Code cannot be longer than 10 characters.")]
        public string ZipCode { get; set; }

        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Display(Name = "Email", Order = 13)]
        public string Email { get; set; }
     
        [Display(Name = "Home Phone No.", Order = 14)]
        [StringLength(20, ErrorMessage = "Home Phone cannot be longer than 20 characters.")]
        public string HomePhoneNumber { get; set; }

        [Display(Name = "Cell Phone No.", Order = 15)]
        [StringLength(20, ErrorMessage = "Cell Phone cannot be longer than 20 characters.")]
        public string CellPhoneNumber { get; set; }

        [Display(Name = "Passport No.", Order = 16)]
        [StringLength(60, ErrorMessage = "Passport Number cannot be longer than 60 characters.")]
        public string PassportNumber { get; set; }

        [Display(Name = "Name As It Appears on Passport", Order = 17)]
        [StringLength(50, ErrorMessage = "Passport Name cannot be longer than 50 characters.")]
        public string NameOnPassport { get; set; }

        [Display(Name = "First Emergency Contact Name", Order = 18)]
        [StringLength(50, ErrorMessage = "Emergency Contact Name cannot be longer than 50 characters.")]
        public string EmergencyContactOneName { get; set; }

        [Display(Name = "First Emergency Contact Phone No.", Order = 19)]
        [StringLength(20, ErrorMessage = "Emergency Contact Phone No. cannot be longer than 20 characters.")]
        public string EmergencyContactOnePhoneNumber { get; set; }

        [Display(Name = "Second Emergency Contact Name", Order = 20)]
        [StringLength(50, ErrorMessage = "Emergency Contact Name cannot be longer than 50 characters.")]
        public string EmergencyContactTwoName { get; set; }

        [Display(Name = "Second Emergency Contact Phone No.", Order = 21)]
        [StringLength(20, ErrorMessage = "Emergency Contact Phone No. cannot be longer than 20 characters.")]
        public string EmergencyContactTwoPhoneNumber { get; set; }

        [Display(Name = "Health Issues?", Order = 22)]
        [StringLength(500, ErrorMessage = "Health Issues cannot be longer than 500 characters.")]
        public string HealthIssues { get; set; }

        [Display(Name = "Medications? (Name, Dose & Frequency)", Order = 23)]
        [StringLength(500, ErrorMessage = "Medications cannot be longer than 500 characters.")]
        public string Medications { get; set; }

        [Display(Name = "Job Title/Position", Order = 24)]
        [ForeignKey("Role")]
        public int? RoleID { get; set; }

        [Display(Name = "Physician Other Specialty", Order = 25)]
        [StringLength(50, ErrorMessage = "Physician Other Specialty cannot be longer than 50 characters.")]
        public string RoleInformation { get; set; }

        [Display(Name = "Spanish Level", Order = 26)]
        [ForeignKey("SpanishLevel")]
        public int? SpanishLevelID { get; set; }

        [ForeignKey("RoomShare")]
        [Display(Name = "Room Preference", Order = 27)]
        public int? RoomShareID { get; set; }

        [Display(Name = "Requested Roommate", Order = 28)]
        [StringLength(50, ErrorMessage = "Requested Roommate Name cannot be longer than 50 characters.")]
        public string RoomMateRequest { get; set; }
        
        ////////////////////////////////////////////////////////////////////////////////////////////////
        // U.S. DEPARTURE
        [Display(Name = "Departure Airline (from U.S.)", Order = 29)]
        [StringLength(50, ErrorMessage = "Airline Name cannot be longer than 50 characters.")]
        public string USDepartureAirline { get; set; }

        [StringLength(20, ErrorMessage = "Airline Flight No. cannot be longer than 20 characters.")]
        [Display(Name = "Departure Flight No. (from U.S.)", Order = 30)]
        public string USDepartureFlightNumber { get; set; }

        public DateTime? USDepartureDateTime { get; set; }
 
        [StringLength(30, ErrorMessage = "Departure Date/Time cannot be longer than 30 characters.")]
        [Display(Name = "Departure Date/Time (from U.S.) - (mm/dd/yyyy HH:MM AM/PM", Order = 31)]
        [RegularExpression("\\d{2}/\\d{2}/\\d{4}\\s+\\d{2}:\\d{2}\\s+(AM|PM)", ErrorMessage = "Use this format: mm/dd/yyyy HH:MM AM/PM")]
        public string USDeparture
        {
            get
            {
                return String.Format("{0:MM/dd/yyyy hh:mm tt}", USDepartureDateTime);  
            }
        }
       /////////////////////////////////////////////////////////////////////////////////
        // GUATEMALA ARRIVAL
        public DateTime? GuatemalaArrivalDateTime { get; set; }

        [StringLength(30, ErrorMessage = "Arrival Date/Time cannot be longer than 30 characters.")]
        [Display(Name = "Arrival Date/Time (in Guatemala) - (mm/dd/yyyy HH:MM AM/PM", Order = 32)]
        [RegularExpression("\\d{2}/\\d{2}/\\d{4}\\s+\\d{2}:\\d{2}\\s+(AM|PM)", ErrorMessage = "Invalid Date/Time Format")]
        public string GuatemalaArrival
        {
            get
            {
                return String.Format("{0:MM/dd/yyyy hh:mm tt}", GuatemalaArrivalDateTime);
            }
        }
        /////////////////////////////////////////////////////////////////////
        // GUATEMALA DEPARTURE
        [Display(Name = "Return Airline (from Guatemala)", Order = 33)]
        [StringLength(50, ErrorMessage = "Airline Name cannot be longer than 50 characters.")]
        public string GuatemalaReturnAirline { get; set; }

        [Display(Name = "Return Flight No. (from Guatemala)", Order = 34)]
        [StringLength(20, ErrorMessage = "Airline Flight No. cannot be longer than 20 characters.")]
        public string GuatemalaReturnFlightNumber { get; set; }

        public DateTime? GuatemalaReturnDateTime { get; set; }

        [StringLength(30, ErrorMessage = "Return Date/Time cannot be longer than 30 characters.")]
        [Display(Name = "Return Date/Time (from Guatemala) - (mm/dd/yyyy HH:MM AM/PM", Order = 35)]
        [RegularExpression("\\d{2}/\\d{2}/\\d{4}\\s+\\d{2}:\\d{2}\\s+(AM|PM)", ErrorMessage = "Use this format: mm/dd/yyyy HH:MM AM/PM")]
        public string GuatemalaReturn
        {
            get
            {
                return String.Format("{0:MM/dd/yyyy hh:mm tt}", GuatemalaReturnDateTime);
            }
        }
        /////////////////////////////////////////////////////////////////////

        [Display(Name = "Other Information/Comments", Order = 36)]
        [StringLength(500, ErrorMessage = "Comments cannot be longer than 500 characters.")]
        public string Comments { get; set; }

        public bool Submitted { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                if (String.IsNullOrEmpty(MiddleName))
                {
                    return FirstName + " " + LastName;
                }
                else
                {
                    return FirstName + " " + MiddleName + " " + LastName;
                }
            }
        }

        public virtual Team Team { get; set; }
        public virtual Role Role { get; set; }
        public virtual State State { get; set; }
        public virtual SpanishLevel SpanishLevel { get; set; }
        public virtual RoomShare RoomShare { get; set; }
    }
}

//public class MyAwesomeDateValidation : ValidationAttribute
//{
//    public override bool IsValid(object value)
//    {
//        DateTime dt;
//        bool parsed = DateTime.TryParse((string)value, out dt);
//        if (!parsed)
//            return false;

//        // eliminate other invalid values, etc
//        // if contains valid hour for your business logic, etc

//        return true;
//    }
//}

//public class AfterTodayAttribute : ValidationAttribute
//{
//    public AfterTodayAttribute()
//    {
//    }

//    //Tried this as well...same result
//    //protected override ValidationResult IsValid(object value, ValidationContext validationContext)
//    //{
//    //    return base.IsValid(value, validationContext);
//    //}

//    public override bool IsValid(object value)
//    {
//        //value is already a DateTime and set to DateTime.Max
//        var valid = false; // for testing only
//        return valid;
//    }
//}

//public class DateTimeValidation : RegularExpressionAttribute
//{
//    public DateTimeValidation()
//        : base(@"^((((31\/(0?[13578]|1[02]))|((29|30)\/(0?[1,3-9]|1[0-2])))\/(1[6-9]|[2-9]\d)?\d{2})|(29\/0?2\/(((1[6-9]|[2-9]\d)?(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))|(0?[1-9]|1\d|2[0-8])\/((0?[1-9])|(1[0-2]))\/((1[6-9]|[2-9]\d)?\d{2})) (20|21|22|23|[0-1]?\d):[0-5]?\d$")
//    {
//        ErrorMessage = "Date must be in the format of : dd/mm/yyyy hh:mm";
//    }
//}

