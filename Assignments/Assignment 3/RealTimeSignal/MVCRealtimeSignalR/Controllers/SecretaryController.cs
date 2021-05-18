using MVCRealtimeSignalR.Hubs;
using MVCRealtimeSignalR.Models;
using System;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace MVCRealtimeSignalR.Controllers
{
    public class SecretaryController : Controller
    {
        private SignalRDbContext db = new SignalRDbContext();

        // GET: Patients
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetPatientData()
        {
            return PartialView("_PatientData", db.Patients.ToList());
        }

        // GET: Secretary/Create
        public ActionResult CreatePatient()
        {
            return View();
        }

        // POST: Secretary/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePatient([Bind(Include = "Id,Name,IdentityNumber,PersonalNumericalCode,DateOfBirth,Address")] Patient patient)
        {
            var userDetails = db.Users.Where(x => x.Username == patient.PersonalNumericalCode).FirstOrDefault();
            if (userDetails != null)
            {
                TempData["errorMessage"] = "Patient with same numerical code already exists !";
                return View("Error");
            }

            DateTime todayStart = DateTime.Now.Date;
            if (todayStart.CompareTo(patient.DateOfBirth) <= 0)
            {
                TempData["errorMessage"] = "Date should be in the past !";
                return View("Error");
            }
            

            if (ModelState.IsValid)
            {
                db.Patients.Add(patient);
                db.SaveChanges();
                EmployeesHub.BroadcastData();
                return RedirectToAction("Index");
            }

            return View(patient);
        }

        // GET: Secretary/Edit/5
        public ActionResult EditPatient(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // POST: Secretary/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPatient([Bind(Include = "Id,Name,IdentityNumber,PersonalNumericalCode,DateOfBirth,Address")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();
                EmployeesHub.BroadcastData();
                return RedirectToAction("Index");
            }
            return View(patient);
        }










        public ActionResult GetConsultationData()
        {
            return PartialView("_ConsultationData", db.Consultations.ToList());
        }

        // GET: Secretary/Create
        public ActionResult CreateConsultation()
        {
            return View();
        }

        // POST: Secretary/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateConsultation([Bind(Include = "Id,Date,PatientName,DoctorName,Details")] Consultation consultation)
        {
            DateTime todayStart = DateTime.Now.Date;
            if (todayStart.CompareTo(consultation.Date) >= 0)
            {
                TempData["errorMessage"] = "Date should be in the future !";
                return View("Error");
            }

            var alreadyBooked = db.Consultations.Where(x => x.Date.CompareTo(consultation.Date) == 0).FirstOrDefault();
            if (alreadyBooked != null)
            {
                TempData["errorMessage"] = "Date already booked !";
                return View("Error");
            }

            if (ModelState.IsValid)
            {
                db.Consultations.Add(consultation);
                db.SaveChanges();
                EmployeesHub.BroadcastData();
                return RedirectToAction("Index");
            }

            return View(consultation);
        }

        // GET: Secretary/Edit/5
        public ActionResult EditConsultation(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consultation consultation = db.Consultations.Find(id);
            if (consultation == null)
            {
                return HttpNotFound();
            }
            return View(consultation);
        }

        // POST: Secretary/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditConsultation([Bind(Include = "Id,Date,PatientName,DoctorName,Details")] Consultation consultation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(consultation).State = EntityState.Modified;
                db.SaveChanges();
                EmployeesHub.BroadcastData();
                return RedirectToAction("Index");
            }
            return View(consultation);
        }

        // GET: Secretary/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consultation consultation = db.Consultations.Find(id);
            if (consultation == null)
            {
                return HttpNotFound();
            }
            return View(consultation);
        }

        // POST: Secretary/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Consultation consultation = db.Consultations.Find(id);
            db.Consultations.Remove(consultation);
            db.SaveChanges();
            EmployeesHub.BroadcastData();
            return RedirectToAction("Index");
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
