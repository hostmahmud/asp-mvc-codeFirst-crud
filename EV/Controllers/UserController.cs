using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EV.Models;

namespace mvcCodeFirstEV2.Controllers
{
    public class UserController : Controller
    {
        private ImmiDbContext db = new ImmiDbContext();
        
        // GET: User/Registration
        public ActionResult Registration()
        {
            if (Session["fname"] == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        // POST: User/Registration
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Registration([Bind(Include = "EmailID,FirstName,LastName,Password,ConfirmPassword")] Users users)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(users);
                await db.SaveChangesAsync();
                return RedirectToAction("Login");
            }

            return null;
        }

        public ActionResult Login()
        {
            if (Session["fname"] == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Immigrants");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "EmailID,Password")] Users login)
        {
            var obj = db.Users.Where(a => a.EmailID.Equals(login.EmailID) && a.Password.Equals(login.Password)).FirstOrDefault();
            if (obj != null)
            {
                Session["fname"] = obj.FirstName.ToString();
                return RedirectToAction("Index", "Immigrants");
            }
            return View(login);
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login","User");
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
