using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using gs10.Models;
using Microsoft.AspNet.Identity;
using gs10.Apps;

namespace gs10.Controllers
{
    [Authorize]
    public class ApplicantController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        const string currentYear = "2019";
        Encryption encryption = new gs10.Apps.Encryption();

        public ActionResult Index(int? id, [Bind(Include = "TeamID,Year")] Applicant applicant2)
        {
            ViewBag.Encryption = encryption;
            if (User.IsInRole("Admin"))
            {
                var year = applicant2.Year;
                var teamID = applicant2.TeamID;
                var teamName = string.Empty;

                // if coming from search page
                switch (teamID)
                {
                    case 1:
                        teamName = "All Teams";
                        break;
                    case 2:
                        teamName = "Team 1";
                        break;
                    case 3:
                        teamName = "Team 2";
                        break;
                    case 4:
                        teamName = "Team 3";
                        break;
                }

                if (year != null || teamID != null)
                {
                    List<Applicant> applicantList = new List<Applicant>();
                    var teams = db.Teams.Find(teamID);
                    ViewBag.TeamName = teamName;

                    // All Years abd Any Team
                    if (year == "0" && teamID == 1)
                    {
                        applicantList = db.Applicants.OrderBy(x => x.Year).ThenBy(x => x.LastName).ThenBy(x => x.FirstName).ToList();
                    }
                    // Specific Year and Any Team
                    else if (year != "0" && teamID == 1)
                    {
                        applicantList = db.Applicants.Where(b => b.Year.Equals(year)).OrderBy(x => x.Year).ThenBy(x => x.LastName).ThenBy(x => x.FirstName).ToList();
                    }
                    // Any Year and Specific Team
                    else if (year == "0" && teamID > 1)
                    {
                        applicantList = db.Applicants.Where(b => b.TeamID.Value.Equals(teamID.Value)).OrderBy(x => x.Year).ThenBy(x => x.LastName).ThenBy(x => x.FirstName).ToList();
                    }
                    // Specific Year and Specific Team
                    else if (year != "0" && teamID > 1)
                    {
                        applicantList = db.Applicants.Where(b => b.Year.Equals(year) && b.TeamID.Value.Equals(teamID.Value)).OrderBy(x => x.Year).ThenBy(x => x.LastName).ThenBy(x => x.FirstName).ToList();
                    }

                    foreach (Applicant applicant in applicantList)
                    {
                        applicant.PassportNumber = encryption.Decrypt(applicant.PassportNumber);
                    }
                    ViewBag.UserMode = "Edit";
                    return View(applicantList.OrderBy(x => x.Year).ThenBy(x => x.LastName).ThenBy(x => x.FirstName).ToList());
                }
            
                ViewBag.TeamID = new SelectList(db.Teams, "TeamID", "TeamName");
                return View("Search");
            }
            else
            {
                string userID = User.Identity.GetUserId();
                var applicants = db.Applicants.Where(b => b.UserId.Equals(userID) && b.Year.Equals(currentYear));

                foreach (Applicant applicant in applicants)
                {
                    applicant.PassportNumber = encryption.Decrypt(applicant.PassportNumber);
                }

                if (applicants.ToList().Count > 0)
                {
                    ViewBag.UserMode = "Edit";
                    if (applicants.ToList().Count == 1)
                    {
                        int applicantID = applicants.First().ApplicantID;

                        if (id != null && id.Value == -1)
                        {
                            return View(applicants.ToList());
                        }
                        else
                        {
                            return RedirectToAction("Edit/" + encryption.Encrypt(applicantID.ToString()));
                        }
                    }
                    else
                    {
                        return View(applicants.ToList());
                    }
                    return View(applicants.ToList());
                }
                else
                {
                    ViewBag.UserMode = "Create";
                    return RedirectToAction("Create");
                }
            }
        }

        // GET: /Applicant/Details/5
        public ActionResult Details(string id)
        {
            int applicationID;

            if (id != null)
            {
                id = encryption.Decrypt(id);

                if (Int32.TryParse(id, out applicationID))
                {
                    Applicant applicant = db.Applicants.Find(applicationID);
                    if (applicant != null)
                    {
                        applicant.PassportNumber = encryption.Decrypt(applicant.PassportNumber);
                        ViewBag.Encryption = encryption;
                        return View(applicant);
                    }
                    else
                    {
                        return HttpNotFound();
                    }
                }
                else
                {
                    return HttpNotFound();
                }
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        // GET: /Applicant/Create
        public ActionResult Create()
        {
            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleName");
            ViewBag.RoomShareID = new SelectList(db.RoomShares, "RoomShareID", "RoomShareName");
            ViewBag.SpanishLevelID = new SelectList(db.SpanishLevels, "SpanishLevelID", "SpanishLevelName");
            ViewBag.StateID = new SelectList(db.States, "StateID", "StateName");
            ViewBag.TeamID = new SelectList(db.Teams, "TeamID", "TeamName");
            return View();
        }

        // POST: /Applicant/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ApplicantID,UserId,Year,FirstName,MiddleName,LastName,PreferredName,Address1,Address2,City,StateID,ZipCode,Email,HomePhoneNumber,CellPhoneNumber,PassportNumber,NameOnPassport,EmergencyContactOneName,EmergencyContactOnePhoneNumber,EmergencyContactTwoName,EmergencyContactTwoPhoneNumber,HealthIssues,RoleID,RoleInformation,TeamID,SpanishLevelID,RoomShareID,RoomMateRequest,USDepartureAirline,USDepartureFlightNumber,GuatemalaArrivalDateTime,GuatemalaReturnAirline,GuatemalaReturnFlightNumber,GuatemalaReturnDateTime,Comments,Medications")] Applicant applicant)
        {
            if (ModelState.IsValid)
            {
                applicant.UserId = User.Identity.GetUserId();
                applicant.Submitted = false;
                applicant.Year = currentYear;
                applicant.PassportNumber = encryption.Encrypt(applicant.PassportNumber);

                string usDeparture = Request["USDeparture"];
                string guatemalaArrival = Request["GuatemalaArrival"];
                string guatemalaReturn = Request["GuatemalaReturn"];
                DateTime usDepartureDateTime;
                DateTime guatemalaArrivalDateTime;
                DateTime guatemalaReturnDateTime;

                if (DateTime.TryParse(usDeparture, out usDepartureDateTime))
                {
                    applicant.USDepartureDateTime = usDepartureDateTime;
                }
                else
                {
                    applicant.USDepartureDateTime = null;
                }

                if (DateTime.TryParse(guatemalaArrival, out guatemalaArrivalDateTime))
                {
                    applicant.GuatemalaArrivalDateTime = guatemalaArrivalDateTime;
                }
                else
                {
                    applicant.GuatemalaArrivalDateTime = null;
                }

                if (DateTime.TryParse(guatemalaReturn, out guatemalaReturnDateTime))
                {
                    applicant.GuatemalaReturnDateTime = guatemalaReturnDateTime;
                }
                else
                {
                    applicant.GuatemalaReturnDateTime = null;
                }

                db.Applicants.Add(applicant);
                db.SaveChanges();
                ViewBag.UserMode = "Edit";
                return RedirectToAction("Index/-1");
            }

            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleName", applicant.RoleID);
            ViewBag.RoomShareID = new SelectList(db.RoomShares, "RoomShareID", "RoomShareName", applicant.RoomShareID);
            ViewBag.SpanishLevelID = new SelectList(db.SpanishLevels, "SpanishLevelID", "SpanishLevelName", applicant.SpanishLevelID);
            ViewBag.StateID = new SelectList(db.States, "StateID", "StateName", applicant.StateID);
            ViewBag.TeamID = new SelectList(db.Teams, "TeamID", "TeamName", applicant.TeamID);
            return View(applicant);
        }

        // GET: /Applicant/Edit/5
        public ActionResult Edit(string id)
        {
            int applicationID;

            if (id != null)
            {
                id = encryption.Decrypt(id);

                if (Int32.TryParse(id, out applicationID))
                {
                    Applicant applicant = db.Applicants.Find(applicationID);
                    if (applicant != null)
                    {
                        ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleName", applicant.RoleID);
                        ViewBag.RoomShareID = new SelectList(db.RoomShares, "RoomShareID", "RoomShareName", applicant.RoomShareID);
                        ViewBag.SpanishLevelID = new SelectList(db.SpanishLevels, "SpanishLevelID", "SpanishLevelName", applicant.SpanishLevelID);
                        ViewBag.StateID = new SelectList(db.States, "StateID", "StateName", applicant.StateID);
                        ViewBag.TeamID = new SelectList(db.Teams, "TeamID", "TeamName", applicant.TeamID);
                        applicant.PassportNumber = encryption.Decrypt(applicant.PassportNumber);
                        ViewBag.UserMode = "Edit";
                        ViewBag.Encryption = encryption;
                        return View(applicant);
                    }
                    else
                    {
                        return HttpNotFound();
                    }
                }
                else
                {
                    return HttpNotFound();
                }
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        // POST: /Applicant/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ApplicantID,UserId,Year,FirstName,MiddleName,LastName,PreferredName,Address1,Address2,City,StateID,ZipCode,Email,HomePhoneNumber,CellPhoneNumber,PassportNumber,NameOnPassport,EmergencyContactOneName,EmergencyContactOnePhoneNumber,EmergencyContactTwoName,EmergencyContactTwoPhoneNumber,HealthIssues,RoleID,RoleInformation,TeamID,SpanishLevelID,RoomShareID,RoomMateRequest,USDepartureAirline,USDepartureFlightNumber,GuatemalaReturnAirline,GuatemalaReturnFlightNumber,GuatemalaReturn,Comments,Medications")] Applicant applicant)
        {
            if (ModelState.IsValid)
            {
                applicant.PassportNumber = encryption.Encrypt(applicant.PassportNumber);

                string usDeparture = Request["USDeparture"];
                string guatemalaArrival = Request["GuatemalaArrival"];
                string guatemalaReturn = Request["GuatemalaReturn"];
                string originalYear = Request["Year"];
                DateTime usDepartureDateTime;
                DateTime guatemalaArrivalDateTime;
                DateTime guatemalaReturnDateTime;

                bool submitted = false;
                bool result = Boolean.TryParse(Request["Submitted"], out submitted);
                applicant.Submitted = submitted;

                if (DateTime.TryParse(usDeparture, out usDepartureDateTime))
                {
                    applicant.USDepartureDateTime = usDepartureDateTime;
                }
                else
                {
                    applicant.USDepartureDateTime = null;
                }

                if (DateTime.TryParse(guatemalaArrival, out guatemalaArrivalDateTime))
                {
                    applicant.GuatemalaArrivalDateTime = guatemalaArrivalDateTime;
                }
                else
                {
                    applicant.GuatemalaArrivalDateTime = null;
                }

                if (DateTime.TryParse(guatemalaReturn, out guatemalaReturnDateTime))
                {
                    applicant.GuatemalaReturnDateTime = guatemalaReturnDateTime;
                }
                else
                {
                    applicant.GuatemalaReturnDateTime = null;
                }

                applicant.Year = originalYear;
                db.Entry(applicant).State = EntityState.Modified;
                db.SaveChanges();
               
                return RedirectToAction("Index", new { @id = -1 });          
            }
            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleName", applicant.RoleID);
            ViewBag.RoomShareID = new SelectList(db.RoomShares, "RoomShareID", "RoomShareName", applicant.RoomShareID);
            ViewBag.SpanishLevelID = new SelectList(db.SpanishLevels, "SpanishLevelID", "SpanishLevelName", applicant.SpanishLevelID);
            ViewBag.StateID = new SelectList(db.States, "StateID", "StateName", applicant.StateID);
            ViewBag.TeamID = new SelectList(db.Teams, "TeamID", "TeamName", applicant.TeamID);
            ViewBag.UserMode = "Edit";
            return RedirectToAction("Index");
        }

        // GET: /Applicant/Edit/5
        public ActionResult Submit(string id)
        {
            int applicationID;

            if (id != null)
            {
                id = encryption.Decrypt(id);

                if (Int32.TryParse(id, out applicationID))
                {
                    Applicant applicant = db.Applicants.Find(applicationID);
                    if (applicant != null)
                    {
                        applicant.Submitted = true;
                        db.Entry(applicant).State = EntityState.Modified;
                        db.SaveChanges();
                        ViewBag.UserMode = "Edit";
                        ViewBag.Encryption = encryption;
                        return RedirectToAction("Index/-1");
                    }
                    else
                    {
                        return HttpNotFound();
                    }
                }
                else
                {
                    return HttpNotFound();
                }
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        // GET: /Applicant/Delete/5
        public ActionResult Delete(string id)
        {
            int applicationID;

            if (id != null)
            {
                id = encryption.Decrypt(id);

                if (Int32.TryParse(id, out applicationID))
                {
                    Applicant applicant = db.Applicants.Find(applicationID);
                    if (applicant != null)
                    {
                        ViewBag.Encryption = encryption;
                        applicant.PassportNumber = encryption.Decrypt(applicant.PassportNumber);
                        return View(applicant);
                    }
                    else
                    {
                        return HttpNotFound();
                    }
                }
                else
                {
                    return HttpNotFound();
                }
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        // POST: /Applicant/Delete/5
        //[HttpPost, ActionName("Delete")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            int applicationID;

            if (id != null)
            {
                id = encryption.Decrypt(id);

                if (Int32.TryParse(id, out applicationID))
                {
                    Applicant applicant = db.Applicants.Find(applicationID);
                    if (applicant != null)
                    {
                        db.Applicants.Remove(applicant);
                        db.SaveChanges();
                        if (User.IsInRole("Admin"))
                        {
                            return RedirectToAction("Index", new { @id = -1 });
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }    
                    }
                    else
                    {
                        return HttpNotFound();
                    }
                }
                else
                {
                    return HttpNotFound();
                }
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }


        // GET: /Applicant/
        public ActionResult Passport(string id)
        {
            ViewBag.TeamID = new SelectList(db.Teams, "TeamID", "TeamName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Passport([Bind(Include = "TeamID")] Applicant applicant)
        {
            var teamID = applicant.TeamID;
            return RedirectToAction("PassportData/" + encryption.Encrypt(teamID.ToString()));
        }

        // GET: /Applicant/
        public ActionResult PassportData(string id)
        {
            int teamID;

            if (id != null)
            {
                id = encryption.Decrypt(id);

                if (Int32.TryParse(id, out teamID))
                {
                    List<Applicant> applicantList = new List<Applicant>();

                    var teams = db.Teams.Find(teamID);
                    if (teamID == 1)
                    {
                        ViewBag.TeamName = "All Teams";
                    }
                    else
                    {
                        ViewBag.TeamName = teams.TeamName;
                    }

                    if (teamID == 1)
                    {
                        applicantList = db.Applicants.ToList();
                    }
                    else
                    {
                        applicantList = db.Applicants.Where(b => b.TeamID.Value.Equals(teamID)).ToList();
                    }

                    foreach (Applicant applicant in applicantList)
                    {
                        applicant.PassportNumber = encryption.Decrypt(applicant.PassportNumber);
                    }


                    if (applicantList.Count > 0)
                    {
                        return View(applicantList.OrderBy(b => b.LastName));
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    return HttpNotFound();
                }
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
