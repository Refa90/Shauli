using ShauliBlog.Models;
using ShauliBlog.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ShauliBlog.Controllers
{
    public class BlogController : Controller
    {
        private ShauliBlogContext db = new ShauliBlogContext();
        PostsController postsController = new PostsController();
        PostCommentsController postCommentsController = new PostCommentsController();
        CommentController commentController = new CommentController();

        // GET: Blog
        public ActionResult Index(string searchString)
        {
            var posts = from p in db.Posts select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                posts = posts.Where(s => s.Headline.Contains(searchString));
            }
            List<BlogVisitorModel> model = posts.ToList().Select(x => new BlogVisitorModel(x)).ToList();

            return View(model);
        }

        public ActionResult PostsManage()
        {
            List<Post> posts = db.Posts.ToList();

            string viewPath = "~/Views/Manage/PostManagement.cshtml";

            return View(viewPath, posts);
        }

        //[HttpPost]
        //public ActionResult CreatePost(Post post)
        //{
        //    return postsController.Create(post);
        //}

        [HttpGet]
        public ActionResult CreatePost()
        {
            return RedirectToAction("Create", "Posts");
        }

        public ActionResult GetPosts()
        {
            return postsController.Index("");
        }

        public ActionResult PostDetails(int? id)
        {
            return postsController.Details(id);
        }


        public ActionResult EditPost(int? id)
        {
            return postsController.Edit(id);
        }

        public ActionResult DeletePost(int? id)
        {
            return postsController.Delete(id);
        }

        public ActionResult PostCommentsDetails(int? id)
        {
            return postCommentsController.Details(id,"");
        }

        public ActionResult GetComments()
        {
            return commentController.Index();
        }

        public ActionResult CommentDetails(int? id)
        {
            return commentController.Details(id);
        }

        public ActionResult CommentCreate(Comment comment)
        {
            return commentController.Create(comment);
        }

        public ActionResult CommentEdit(int? id)
        {
            return commentController.Edit(id);
        }

        public ActionResult CommentDelete(int? id)
        {
            return commentController.Delete(id);
        }

        public ActionResult CommentDeleteConfirmed(int id)
        {
            return commentController.DeleteConfirmed(id);
        }
    }
}
