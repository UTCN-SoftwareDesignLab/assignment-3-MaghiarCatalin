using MVCRealtimeSignalR.Hubs;
using MVCRealtimeSignalR.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MVCRealtimeSignalR.Controllers
{
    public class DoctorController : Controller
    {
       
        private SignalRDbContext db = new SignalRDbContext();

        // GET: Consulations
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetConsultationData()
        {
            return PartialView("_ConsultationData", db.Consultations.ToList());
        }



        // GET: Doctor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Doctor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Date,PatientName,DoctorName,Details")] Consultation consultation)
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

        // GET: Doctor/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: Doctor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,PatientName,DoctorName,Details")] Consultation consultation)
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