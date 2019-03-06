using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using gs10.Models;
using Microsoft.AspNet.Identity;
using System.IO;
using System.Security.Cryptography;
using System.Text;
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
                    //if (teamID.Value == 1)
                    //{
                    //    ViewBag.TeamName = "All Teams";
                    //}
                    //else
                    //{
                    //    ViewBag.TeamName = teams.TeamName;
                    //}

                    // All Years abd Any Team
                    if (year == "0" && teamID == 1)
                    {
                        //applicantList = db.Applicants.ToList();
                        //applicantList = db.Applicants.OrderBy(x => x.LastName).ThenBy(x => x.FirstName).ToList();
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
                    //if (year != currentYear)
                    //{
                    //    ViewBag.ReadOnly = true;
                    //}
                    //else
                    //{
                    //    ViewBag.ReadOnly = false;
                    //}
                    //return View(applicantList.OrderBy(b => b.LastName));
                    return View(applicantList.OrderBy(x => x.Year).ThenBy(x => x.LastName).ThenBy(x => x.FirstName).ToList());
                }
            
                ViewBag.TeamID = new SelectList(db.Teams, "TeamID", "TeamName");
                return View("Search");
            }
            else
            {
                string userID = User.Identity.GetUserId();
                //var applicants = db.Applicants.Where(b => b.UserId.Equals(userID));
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
            

            //ViewBag.Encryption = encryption;
            //id = encryption.Decrypt(id);

            //bool result = Int32.TryParse(id, out ApplicationID);

            //if (Int32.TryParse(id, out ApplicationID))
            //{
            //    Applicant applicant = db.Applicants.Find(ApplicationID);
            //    if (applicant != null)
            //    {
            //        applicant.PassportNumber = encryption.Decrypt(applicant.PassportNumber);
            //        return View(applicant);
            //    }
            //    else
            //    {
            //        return HttpNotFound();
            //    }
            //}
            //else
            //{
            //    return HttpNotFound();
            //}

            //Applicant applicant = db.Applicants.Find(ApplicationID);
            //if (applicant == null)
            //{
            //    return HttpNotFound();
            //}
            //applicant.PassportNumber = encryption.Decrypt(applicant.PassportNumber);
            //return View(applicant);
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ApplicantID,UserId,Year,FirstName,MiddleName,LastName,PreferredName,Address1,Address2,City,StateID,ZipCode,Email,HomePhoneNumber,CellPhoneNumber,PassportNumber,NameOnPassport,EmergencyContactOneName,EmergencyContactOnePhoneNumber,EmergencyContactTwoName,EmergencyContactTwoPhoneNumber,HealthIssues,RoleID,RoleInformation,TeamID,SpanishLevelID,RoomShareID,RoomMateRequest,USDepartureAirline,USDepartureFlightNumber,GuatemalaArrivalDateTime,GuatemalaReturnAirline,GuatemalaReturnFlightNumber,GuatemalaReturnDateTime,Comments,Medications")] Applicant applicant)
        //ApplicantID,UserId,Year,FirstName,MiddleName,LastName,PreferredName,Address1,Address2,City,StateID,ZipCode,Email,HomePhoneNumber,CellPhoneNumber,PassportNumber,NameOnPassport,EmergencyContactOneName,EmergencyContactOnePhoneNumber,EmergencyContactTwoName,EmergencyContactTwoPhoneNumber,HealthIssues,RoleID,RoleInformation,TeamID,SpanishLevelID,RoomShareID,RoomMateRequest,USDepartureAirline,USDepartureFlightNumber,USDepartureDateTime,GuatemalaArrivalDateTime,GuatemalaReturnAirline,GuatemalaReturnFlightNumber,GuatemalaReturnDateTime,Comments,Medications")] Applicant applicant)  
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
                //db.Entry(applicant).State = EntityState.;
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
        //public ActionResult Edit(int? id)
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ApplicantID,UserId,Year,FirstName,MiddleName,LastName,PreferredName,Address1,Address2,City,StateID,ZipCode,Email,HomePhoneNumber,CellPhoneNumber,PassportNumber,NameOnPassport,EmergencyContactOneName,EmergencyContactOnePhoneNumber,EmergencyContactTwoName,EmergencyContactTwoPhoneNumber,HealthIssues,RoleID,RoleInformation,TeamID,SpanishLevelID,RoomShareID,RoomMateRequest,USDepartureAirline,USDepartureFlightNumber,USDeparture,USDepartureDateTime,GuatemalaArrivalDateTime,GuatemalaReturnAirline,GuatemalaReturnFlightNumber,GuatemalaReturnDateTime,Comments,Medications")] Applicant applicant)
        public ActionResult Edit([Bind(Include = "ApplicantID,UserId,Year,FirstName,MiddleName,LastName,PreferredName,Address1,Address2,City,StateID,ZipCode,Email,HomePhoneNumber,CellPhoneNumber,PassportNumber,NameOnPassport,EmergencyContactOneName,EmergencyContactOnePhoneNumber,EmergencyContactTwoName,EmergencyContactTwoPhoneNumber,HealthIssues,RoleID,RoleInformation,TeamID,SpanishLevelID,RoomShareID,RoomMateRequest,USDepartureAirline,USDepartureFlightNumber,GuatemalaReturnAirline,GuatemalaReturnFlightNumber,GuatemalaReturn,Comments,Medications")] Applicant applicant)
        {
            if (ModelState.IsValid)
            {
                //////////////////////////
                //DateTime day = fromDateValue.
                //do for valid date
                //DateTime myDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

                //var datequery =
                //                db.WhatsOns.Where(c => c.start > myDate)
                //                .OrderByDescending(c => c.start)
                //                .GroupBy(c => c.start).AsEnumerable().Select(
                //                sGroup => new WhatsOn
                //                {
                //                    day = sGroup.Key,
                //                    whtscount = sGroup.Count()
                //                });
                //////////////////////////////////////////////////////
                //@Html.HiddenFor(model => model.ApplicantID)
                //string userID = Request["USDeparture"];

                //applicant.UserId = User.Identity.GetUserId();
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

                //applicant.Year = currentYear;
                applicant.Year = originalYear;
                db.Entry(applicant).State = EntityState.Modified;
                db.SaveChanges();
                //applicant.GuatemalaArrivalDateTime = GuatemalaArrivalDateTime;
                //db.Entry(applicant).State = EntityState.Modified;
                //db.SaveChanges();


                return RedirectToAction("Index", new { @id = -1 });

                //DateTime newDate = DateTime.TryParseExact(applicant.GuatemalaArrivalDateTime, "mm/yy/dd", CultureInfo.InvariantCulture);
                //DateTime result result;
                //DateTime newDate = DateTime.TryParse(applicant.GuatemalaArrivalDateTime, result);
                //return RedirectToAction("Index");


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



            //string applicantID = encryption.Decrypt(id);
            //int appID;

            //bool result = Int32.TryParse(applicantID, out appID);

            ////Applicant applicant = db.Applicants.Find(id);
            //Applicant applicant = db.Applicants.Find(appID);
            //if (applicant == null)
            //{
            //    return HttpNotFound();
            //}
            //applicant.PassportNumber = encryption.Decrypt(applicant.PassportNumber);
            //return View(applicant);
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

            //Applicant applicant = db.Applicants.Find(id);
            //db.Applicants.Remove(applicant);
            //db.SaveChanges();
            //return RedirectToAction("Index", "Home");
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
            //}
            //return View("PassportData");
        }

        // GET: /Applicant/
        public ActionResult PassportData(string id)
        {
            //   id = encryption.Decrypt(id);

            //    if (Int32.TryParse(id, out applicationID))
            //    {
            //        Applicant applicant = db.Applicants.Find(applicationID);
            //        if (applicant != null)
            //        {
            //            applicant.Submitted = true;
            //            db.Entry(applicant).State = EntityState.Modified;
            //            db.SaveChanges();
            //            ViewBag.UserMode = "Edit";
            //            ViewBag.Encryption = encryption;
            //            return RedirectToAction("Index/-1");
            //        }
            //        else
            //        {
            //            return HttpNotFound();
            //        }
            //    }
            //    else
            //    {
            //        return HttpNotFound();
            //    }
            //}
            //else
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}





            //List<Applicant> applicantList = new List<Applicant>();
            //var applicants = db.Applicants;

            //string applicantID = encryption.Decrypt(id);
            //int appID;

            //bool result = Int32.TryParse(applicantID, out appID);

            ////////////////////////
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

                    //var applicants = db.Applicants.Where(b => b.TeamID.Value.Equals(id.Value) && b.NameOnPassport != null);
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


                    //Applicant applicant = db.Applicants.Find(teamID);
                    //if (applicant != null)
                    //{
                    //    db.Applicants.Remove(applicant);
                    //    db.SaveChanges();
                    //    if (User.IsInRole("Admin"))
                    //    {
                    //        return RedirectToAction("Index", new { @id = -1 });
                    //    }
                    //    else
                    //    {
                    //        return RedirectToAction("Index", "Home");
                    //    }
                    //}
                    //else
                    //{
                    //    return HttpNotFound();
                    //}
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


            ////////////////////////

            //var teams = db.Teams.Find(appID);
            //if (appID == 1)
            //{
            //    ViewBag.TeamName = "All Teams";
            //}
            //else
            //{
            //    ViewBag.TeamName = teams.TeamName;
            //}

            ////var applicants = db.Applicants.Where(b => b.TeamID.Value.Equals(id.Value) && b.NameOnPassport != null);
            //if (appID == 1)
            //{
            //    applicantList = db.Applicants.ToList();
            //}
            //else
            //{
            //    applicantList = db.Applicants.Where(b => b.TeamID.Value.Equals(appID)).ToList();
            //}

            //foreach (Applicant applicant in applicantList)
            //{
            //    applicant.PassportNumber = encryption.Decrypt(applicant.PassportNumber);
            //}


            //if (applicantList.Count > 0)
            //{
            //    return View(applicantList.OrderBy(b => b.LastName));
            //}
            //else
            //{
            //    return RedirectToAction("Index", "Home");
            //}

        }

        //private string Encrypt(string clearText)
        //{
        //    if (clearText != null)
        //    {
        //        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);

        //        using (Aes encryptor = Aes.Create())
        //        {
        //            //Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
        //            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, salt);
        //            encryptor.Key = pdb.GetBytes(32);
        //            encryptor.IV = pdb.GetBytes(16);

        //            using (MemoryStream ms = new MemoryStream())
        //            {
        //                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
        //                {
        //                    cs.Write(clearBytes, 0, clearBytes.Length);
        //                    cs.Close();
        //                }
        //                clearText = Convert.ToBase64String(ms.ToArray());
        //            }
        //        }
        //    }
        //    return clearText;
        //}

        //private string Decrypt(string cipherText)
        //{
        //    if (cipherText != null)
        //    {
        //        //string EncryptionKey = "MAKV2SPBNI99212";

        //        byte[] cipherBytes = Convert.FromBase64String(cipherText);

        //        using (Aes encryptor = Aes.Create())
        //        {
        //            //Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
        //            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, salt);
        //            encryptor.Key = pdb.GetBytes(32);
        //            encryptor.IV = pdb.GetBytes(16);

        //            using (MemoryStream ms = new MemoryStream())
        //            {
        //                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
        //                {
        //                    cs.Write(cipherBytes, 0, cipherBytes.Length);
        //                    cs.Close();
        //                }
        //                cipherText = Encoding.Unicode.GetString(ms.ToArray());
        //            }
        //        }
        //    }
        //    return cipherText;

        //}

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
