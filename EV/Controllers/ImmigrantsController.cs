using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EV.Models;
using EV.ViewModel;

namespace mvcCodeFirstEV2.Controllers
{
    public class ImmigrantsController : Controller
    {
        private ImmiDbContext db = new ImmiDbContext();

        // GET: Immigrants
        public async Task<ActionResult> Index()
        {
            if (Session["fname"] != null)
            {
                ViewBag.Dactive = "active";

                var immigrants = db.Immigrants.Include(i => i.aCountry).Include(i => i.dCountry).Include(i => i.nCountry);
                List<Immigrants> records = await immigrants.ToListAsync();
                ImmigrantsVM immigrantsVM = new ImmigrantsVM();

                List<ImmigrantsVM> itemsVMsList = records.Select(x => new ImmigrantsVM
                {
                    iId = x.iId,
                    iName = x.iName,
                    iNationality = x.nCountry.cName,
                    iPassNo = x.iPassNo,
                    iDob = x.iDob,
                    iGender = x.iGender,
                    iPurpose = x.iPurpose,
                    iImage = x.iImage,
                }).ToList();

                return View(itemsVMsList);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        // GET: Immigrants/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (Session["fname"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                //Immigrants immigrants = await db.Immigrants.FindAsync(id);
                Immigrants immigrants = await db.Immigrants.Include(i => i.aCountry).Include(i => i.dCountry).Include(i => i.nCountry).FirstOrDefaultAsync(i => i.iId == id.Value);

                if (immigrants == null)
                {
                    return HttpNotFound();
                }
                return View(immigrants);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        // GET: Immigrants/Create
        public ActionResult Create()
        {
            if (Session["fname"] != null)
            {
                ViewBag.aCountryId = new SelectList(db.Countries, "cId", "cName");
                ViewBag.dCountryId = new SelectList(db.Countries, "cId", "cName");
                ViewBag.nCountryId = new SelectList(db.Countries, "cId", "cName");
                return View();
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        // POST: Immigrants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "iId,iName,nCountryId,iPassNo,iDob,iGender,iPurpose,iImmiType,dCountryId,aCountryId,iDate,iImage,File")] Immigrants immigrants)
        {
            if (Session["fname"] != null)
            {
                if (ModelState.IsValid)
                {
                    string filename = Path.GetFileName(immigrants.File.FileName);//abc.jpg
                    string _filename = DateTime.Now.ToString("hhmmssfff") + filename;//1235abc.jpg
                    string path = Path.Combine(Server.MapPath("~/Images/"), _filename);
                    immigrants.iImage = _filename;
                    db.Immigrants.Add(immigrants);
                    if (await db.SaveChangesAsync() > 0)
                    {
                        immigrants.File.SaveAs(path);
                        return PartialView("_success");
                    }
                    else
                    {
                        return PartialView("_error");
                    }
                }

                ViewBag.aCountryId = new SelectList(db.Countries, "cId", "cName", immigrants.aCountryId);
                ViewBag.dCountryId = new SelectList(db.Countries, "cId", "cName", immigrants.dCountryId);
                ViewBag.nCountryId = new SelectList(db.Countries, "cId", "cName", immigrants.nCountryId);
                return PartialView("_error");
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        // GET: Immigrants/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (Session["fname"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Immigrants immigrants = await db.Immigrants.FindAsync(id);
                Session["CurrentImage"] = immigrants.iImage;
                if (immigrants == null)
                {
                    return HttpNotFound();
                }
                ViewBag.aCountryId = new SelectList(db.Countries, "cId", "cName", immigrants.aCountryId);
                ViewBag.dCountryId = new SelectList(db.Countries, "cId", "cName", immigrants.dCountryId);
                ViewBag.nCountryId = new SelectList(db.Countries, "cId", "cName", immigrants.nCountryId);
                return View(immigrants);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        // POST: Immigrants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "iId,iName,nCountryId,iPassNo,iDob,iGender,iPurpose,iImmiType,dCountryId,aCountryId,iDate,iImage,File")] Immigrants immigrants)
        {
            if (Session["fname"] != null)
            {
                if (ModelState.IsValid)
                {
                    if (immigrants.File != null)
                    {
                        string imgPath = Request.MapPath("~/Images/" + Session["CurrentImage"].ToString());
                        System.IO.File.Delete(imgPath);

                        string filename = Path.GetFileName(immigrants.File.FileName);//abc.jpg
                        string _filename = DateTime.Now.ToString("hhmmssfff") + filename;//1235abc.jpg
                        string path = Path.Combine(Server.MapPath("~/Images/"), _filename);
                        immigrants.iImage = _filename;
                        immigrants.File.SaveAs(path);
                        Session.Clear();
                    }
                    db.Entry(immigrants).State = EntityState.Modified;
                    if (await db.SaveChangesAsync() > 0)
                    {
                        TempData["Success"] = "Success";
                        return RedirectToAction("Edit");
                    }
                }
                ViewBag.aCountryId = new SelectList(db.Countries, "cId", "cName", immigrants.aCountryId);
                ViewBag.dCountryId = new SelectList(db.Countries, "cId", "cName", immigrants.dCountryId);
                ViewBag.nCountryId = new SelectList(db.Countries, "cId", "cName", immigrants.nCountryId);
                return View(immigrants);
            }
            {
                return RedirectToAction("Login", "User");
            }
        }

        // GET: Immigrants/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Immigrants immigrants = await db.Immigrants.FindAsync(id);
            if (immigrants == null)
            {
                return HttpNotFound();
            }
            else
            {
                string imgPath = Request.MapPath("~/Images/" + immigrants.iImage);
                System.IO.File.Delete(imgPath);
                db.Immigrants.Remove(immigrants);
                db.SaveChanges();
            }
            return View(immigrants);
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
