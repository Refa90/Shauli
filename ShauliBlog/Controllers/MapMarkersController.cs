using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShauliBlog.Models;
using System.Data.Entity.Migrations;

namespace ShauliBlog.Controllers
{
    public class MapMarkersController : Controller
    {
        private ShauliBlogContext db = new ShauliBlogContext();

        public MapMarkersController()
        {
            //var markers = from m in db.MapMarkers select m;
            List<MapMarker> markers = db.MapMarkers.ToList();
            ViewBag.markers = markers;
        }



        // GET: MapMarkers
        public ActionResult Index()
        {


            //MapMarker mapMarker1 = new MapMarker
            //{
            //    Id = 1,
            //    Label = "home",
            //    Lat = 32.0878408F,
            //    Lng = 34.7781465F
            //};

            //MapMarker mapMarker2 = new MapMarker
            //{
            //    Id = 2,
            //    Label = "parlament",
            //    Lat = 31.964284F,
            //    Lng = 34.794780F
            //};

            //MapMarker mapMarker3 = new MapMarker
            //{
            //    Id = 3,
            //    Label = "zoo",
            //    Lat = 32.0661607F,
            //    Lng = 34.7904903F
            //};

            //MapMarker mapMarker4 = new MapMarker
            //{
            //    Id = 4,
            //    Label = "hospital",
            //    Lat = 32.3350427F,
            //    Lng = 34.8568825F
            //};



            //db.MapMarkers.Add(mapMarker1);
            //db.MapMarkers.Add(mapMarker2);
            //db.MapMarkers.Add(mapMarker3);
            //db.MapMarkers.Add(mapMarker4);

            //db.SaveChanges();

            List<MapMarker> markers = db.MapMarkers.ToList();
            return View(markers);
        }

        // GET: MapMarkers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MapMarker mapMarker = db.MapMarkers.Find(id);
            if (mapMarker == null)
            {
                return HttpNotFound();
            }
            return View(mapMarker);
        }

        // GET: MapMarkers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MapMarkers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Label,Lat,Lng")] MapMarker mapMarker)
        {
            if (ModelState.IsValid)
            {
                db.MapMarkers.Add(mapMarker);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mapMarker);
        }

        // GET: MapMarkers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MapMarker mapMarker = db.MapMarkers.Find(id);
            if (mapMarker == null)
            {
                return HttpNotFound();
            }
            return View(mapMarker);
        }

        // POST: MapMarkers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Label,Lat,Lng")] MapMarker mapMarker)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mapMarker).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mapMarker);
        }

        // GET: MapMarkers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MapMarker mapMarker = db.MapMarkers.Find(id);
            if (mapMarker == null)
            {
                return HttpNotFound();
            }
            return View(mapMarker);
        }

        // POST: MapMarkers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MapMarker mapMarker = db.MapMarkers.Find(id);
            db.MapMarkers.Remove(mapMarker);
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



