using MVCRealtimeSignalR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MVCRealtimeSignalR.Controllers
{
    public class LoginController : Controller
    {
        private SignalRDbContext db = new SignalRDbContext();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Id,Username,Password,AccountType")] User user)
        {
            var userDetails = db.Users.Where(x => x.Username == user.Username && x.Password == user.Password).FirstOrDefault();
            
            if (userDetails != null)
            {
                Session["userID"] = user.Username;
                
                if (userDetails.AccountType == 0) // secretary
                    return RedirectToAction("Index", "Secretary");
                if (userDetails.AccountType == 1) // Doctor
                    return RedirectToAction("Index", "Doctor");
                if (userDetails.AccountType == 2) // Admin
                    return RedirectToAction("Index", "Admin");
            }

            TempData["errorMessage"] = "User & pass not found !";
            return View("Error");

        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }

    }
}