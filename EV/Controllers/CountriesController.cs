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
    public class CountriesController : Controller
    {
        private ImmiDbContext db = new ImmiDbContext();

        // GET: Countries
        public async Task<ActionResult> Index()
        {
            if (Session["fname"] != null)
            {
                ViewBag.Cactive = "active";
                return View(await db.Countries.ToListAsync());
            }
            else
            {
                return RedirectToAction("Login","User");
            }
                
        }

        // GET: Countries/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (Session["fname"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Countries countries = await db.Countries.FindAsync(id);
                if (countries == null)
                {
                    return HttpNotFound();
                }
                return View(countries);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        // GET: Countries/Create
        public ActionResult Create()
        {
            if (Session["fname"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        // POST: Countries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "cId,cName")] Countries countries)
        {
            if (Session["fname"] != null)
            {
                if (ModelState.IsValid)
                {
                    db.Countries.Add(countries);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

                return View(countries);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        // GET: Countries/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (Session["fname"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Countries countries = await db.Countries.FindAsync(id);
                if (countries == null)
                {
                    return HttpNotFound();
                }
                return View(countries);
            }
            {
                return RedirectToAction("Login", "User");
            }
        }

        // POST: Countries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "cId,cName")] Countries countries)
        {
            if (Session["fname"] != null)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(countries).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                return View(countries);
            }
            {
                return RedirectToAction("Login", "User");
            }
        }

        // GET: Countries/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Countries countries = await db.Countries.FindAsync(id);
            if (countries == null)
            {
                return HttpNotFound();
            }
            else
            {
                db.Countries.Remove(countries);
                await db.SaveChangesAsync();
            }
            return View(countries);
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
