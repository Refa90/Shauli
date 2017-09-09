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

        public ActionResult Details(int? Postid)
        {
            if (Postid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<Comment> comments= db.Comments.Where(comment => comment.PostId == Postid).ToList();
            if (comments == null)
            {
                return HttpNotFound();
            }
            return View(comments);
        }

    }
}