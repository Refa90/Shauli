using ShauliBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ShauliBlog.Controllers
{
    public class PostCommentsController : Controller
    {

        private ShauliBlogContext db = new ShauliBlogContext();

        public ActionResult Details(int? Postid, string searchString)
        {
            if (Postid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var comments = db.Comments.Where(comment => comment.PostId == Postid);

            if (!String.IsNullOrEmpty(searchString))
            {
                comments = comments.Where(s => s.Headline.Contains(searchString));
            }

            if (comments == null)
            {
                return HttpNotFound();
            }
            List<Comment> CommentList = comments.ToList<Comment>();

            return View(CommentList);
        }

    }
}