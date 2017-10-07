using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShauliBlog.Models;

namespace ShauliBlog.Controllers
{
    public class FansClubController : Controller
    {
        private ShauliBlogContext db = new ShauliBlogContext();

        // GET: FansClub
        public ActionResult Index(string searchString, string ddl, bool Gender = false, bool IsAdult = false, bool seniorityCB = false, string seniorityText="")
        {
            var fans = from f in db.Fans select f;

            if (!String.IsNullOrEmpty(searchString))
            {
                fans = fans.Where(s => s.FirstName.Contains(searchString));
            }

            if (Gender)
                fans = fans.Where(s => s.Gender == true);

            var currentDate = DateTime.Today.Year;
            if (IsAdult)
                fans = fans.Where(s => currentDate - s.BirthDate.Year > 18 );
            
            if (seniorityCB)
            {
                if (seniorityText != "")
                {
                    int seniorityNum = Int32.Parse(seniorityText);
                    if (ddl == "Higher")
                        fans = fans.Where(s => s.Seniority > seniorityNum);
                    else
                        fans = fans.Where(s => s.Seniority < seniorityNum);
                }
            }
            return View(fans.ToList());
        }

        // GET: FansClub/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fan fan = db.Fans.Find(id);
            if (fan == null)
            {
                return HttpNotFound();
            }
            return View(fan);
        }

        // GET: FansClub/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FansClub/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Gender,BirthDate,Seniority")] Fan fan)
        {
            if (ModelState.IsValid)
            {
                db.Fans.Add(fan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fan);
        }

        // GET: FansClub/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fan fan = db.Fans.Find(id);
            if (fan == null)
            {
                return HttpNotFound();
            }
            return View(fan);
        }

        // POST: FansClub/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Gender,BirthDate,Seniority")] Fan fan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fan);
        }

        // GET: FansClub/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fan fan = db.Fans.Find(id);
            if (fan == null)
            {
                return HttpNotFound();
            }
            return View(fan);
        }

        // POST: FansClub/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Fan fan = db.Fans.Find(id);
            db.Fans.Remove(fan);
            db.SaveChanges();
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
