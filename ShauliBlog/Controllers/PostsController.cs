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

        public class PostJoinComments
        {
            public int Id { get; set; }
            public string Headline { get; set; }
            public string Author { get; set; }
            public string AuthorWebsiteAddress { get; set; }
            public DateTime PublishDate { get; set; }
            public int commentID { get; set; }
            public int commentPostID { get; set; }
            public string commentHeadline { get; set; }
            public string commentAuthor { get; set; }
            public string commentAuthorWebSiteAddress { get; set; }
        }

        public class GroupByWebsite
        {
            public string AuthorWebsiteAddress { get; set; }
            public int count { get; set; }
        }

        // GET: Posts
        public ActionResult Index(string searchString, bool HasVideo = false, bool HasPicture = false, bool HasComments = false)
        {
            var posts = from p in db.Posts select p;

            if (!String.IsNullOrEmpty(searchString))
                posts = posts.Where(s => s.Headline.Contains(searchString));

            if (HasVideo)
                posts = posts.Where(s => s.Video != null);

            if (HasPicture)
                posts = posts.Where(s => s.Image != null);

            if (HasComments)
                posts = posts.Where(s => s.Comments.Count != 0);


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

        public ActionResult JoinByPost()
        {
            var posts = from p in db.Posts select p;
            var comments = from c in db.Comments select c;

            var all = posts.Join(comments,
                outerKey => outerKey.Id,
                innerKey => innerKey.PostId,
                (blogPosts, blogComments) => new PostJoinComments
                {
                    Id = blogPosts.Id,
                    Headline = blogPosts.Headline,
                    Author = blogPosts.Author,
                    AuthorWebsiteAddress = blogPosts.AuthorWebsiteAddress,
                    PublishDate = blogPosts.PublishDate,
                    commentID = blogComments.Id,
                    commentHeadline = blogComments.Headline,
                    commentAuthor = blogComments.Author,
                    commentAuthorWebSiteAddress = blogComments.AuthorWebsiteAddress
                }
                );
            return View(all);
        }
        public ActionResult JoinByAuthor()
        {
            var posts = from p in db.Posts select p;
            var comments = from c in db.Comments select c;


            var all = posts.Join(comments,
                outerKey => outerKey.Author,
                innerKey => innerKey.Author,
                (blogPosts, blogComments) => new PostJoinComments
                {
                    Id = blogPosts.Id,
                    Headline = blogPosts.Headline,
                    Author = blogPosts.Author,
                    AuthorWebsiteAddress = blogPosts.AuthorWebsiteAddress,
                    PublishDate = blogPosts.PublishDate,
                    commentID = blogComments.Id,
                    commentPostID = blogComments.PostId,
                    commentHeadline = blogComments.Headline,
                    commentAuthor = blogComments.Author,
                    commentAuthorWebSiteAddress = blogComments.AuthorWebsiteAddress
                }
                ).Where(s => s.Id == s.commentPostID);
            return View(all);
        }

        public ActionResult GroupByWebpage()
        {
            
            //var posts = from p in db.Posts select p;
            var comments = from c in db.Comments select c;
            var group = comments.GroupBy(a => a.AuthorWebsiteAddress).Select(c =>  new GroupByWebsite { AuthorWebsiteAddress = c.Key, count = c.Count() });

            return View(group);
            
            /*var comments = from c in db.Comments select c;
            var group = comments.GroupBy(a => a.AuthorWebsiteAddress);

            if (comments == null)
                return HttpNotFound();

            List<Comment> CommentList = group.ToList<Comment>();

            return View(CommentList);
            */
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
