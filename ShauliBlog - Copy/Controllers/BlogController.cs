using ShauliBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreatePost(Post post)
        {
            return postsController.Create(post);
        }

        public ActionResult GetPosts()
        {
            return postsController.Index();
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
            return postCommentsController.Details(id);
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

        public ActionResult Edit(int? id)
        {
            return commentController.Edit(id);
        }

        public ActionResult Delete(int? id)
        {
           return commentController.Delete(id);
        }

        public ActionResult DeleteConfirmed(int id)
        {
            return commentController.DeleteConfirmed(id);
        }





}
}
