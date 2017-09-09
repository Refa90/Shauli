using ShauliBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShauliBlog.Controllers
{
    public class ManageController : Controller
    {
        private ShauliBlogContext db = new ShauliBlogContext();

        // GET: Manage
        public ActionResult Index()
        {
            return View(db.Posts.ToList());
        }
    }
}