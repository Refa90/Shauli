using ShauliBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShauliBlog.Controllers
{
    public class BlogVisitorController : Controller
    {
        private ShauliBlogContext db = new ShauliBlogContext();

        // GET: BlogVisitor
        public ActionResult Index()
        {
            List<BlogVisitorModel> model = db.Posts.ToList().Select(post => new BlogVisitorModel(post)).ToList();
       
            return View(model);
        }
    }
}
