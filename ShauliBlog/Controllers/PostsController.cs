using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShauliBlog.Models;
using ShauliBlog.Utils;

namespace ShauliBlog.Controllers
{
    public class PostsController : Controller
    {
        private ShauliBlogContext db = new ShauliBlogContext();

        // GET: Posts
        public ActionResult Index(string searchString)
        {
            var posts = from p in db.Posts select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                posts = posts.Where(s => s.Headline.Contains(searchString));
            }

            return View(posts.ToList());
        }

        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Posts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PublishDate,Headline,Author,AuthorWebsiteAddress,Content")] Post post, HttpPostedFileBase Image, HttpPostedFileBase Video)
        {
            post.PublishDate = DateTime.Now;

            if (ModelState.IsValid)
            {
                post = HandleMedia(post, Image, Video);
                
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            

            return View(post);
        }

        // GET: Posts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PublishDate,Headline,Author,AuthorWebsiteAddress,Content")] Post post, HttpPostedFileBase Image, HttpPostedFileBase Video)
        {
            if (ModelState.IsValid)
            {
                post = HandleMedia(post, Image, Video);

                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
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

        private Post HandleMedia(Post post, HttpPostedFileBase Image, HttpPostedFileBase Video)
        {
            string absolueImagePath = null;
            string absoluteVideoPath = null;

            if (Image != null)
            {
                string imageName = System.IO.Path.GetFileName(Image.FileName);
                string relativeImagePath = Consts.IMAGE_PATH + imageName;
                absolueImagePath = System.IO.Path.Combine(Server.MapPath(Consts.IMAGE_PATH), imageName);
                post.Image = relativeImagePath;

                // file is uploaded
                Image.SaveAs(absolueImagePath);
            }

            if (Video != null)
            {
                string videoName = System.IO.Path.GetFileName(Video.FileName);
                string relativeVideoPath = Consts.VIDEO_PATH + videoName;
                absoluteVideoPath = System.IO.Path.Combine(Server.MapPath(Consts.VIDEO_PATH), videoName);
                post.Video = relativeVideoPath;

                // file is uploaded
                Video.SaveAs(absoluteVideoPath);
            }

            return post;
        }
    }
}
