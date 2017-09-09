﻿using ShauliBlog.Models;
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
            BlogVisitorModel model = new BlogVisitorModel();
            model.Posts = db.Posts.ToList();
            model.NewComment = new Comment();

            return View(model);
        }
    }
}
