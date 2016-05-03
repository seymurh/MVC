using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using testApp.Models;
using testApp.Repositories;
using MongoDB.Bson;
using testApp.Controllers;
using testApp.Filters;
using System.IO;

namespace testApp.Controllers
{

    public class PostController : Controller
    {
        PostRepository rep = new PostRepository();
        UserRepository userRepo = new UserRepository();
        [Authorize]
        public ActionResult Index()
        {
            // User.Identity.IsAuthenticated

            List<Post> list = rep.Find();
            return View(list);
        }
        #region Post

        [HttpGet]
        public ActionResult AddPost()
        {
            return View();
        }
        [HttpPost,ValidateInput(false)]
        public ActionResult AddPost(Post post)
        {
            //User user = userRepo.FindById(post.Id);
            //post.Author.Name = user.Name;
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var mvcPath = "/Images/" + fileName;
                    var path = Server.MapPath("~" + mvcPath);
                    file.SaveAs(path);
                    post.ImageUrl = mvcPath;
                }
            }
            post.Author = HttpContext.CurrentUser();
            post.PostDate = DateTime.Now;
            rep.Add(post);
            //List<Post> list=rep.Find();
            //return View("Index",list);
            return RedirectToAction("Index");
        }
        [AuthorizePost(Method.Delete, Roles = "admin")]
        [HttpGet]
        public ActionResult DeletePost(ObjectId id)
        {
            rep.Delete(id);
            //List<Post> list = rep.Find();
            return RedirectToAction("Index");
        }
        [AuthorizePost(Method.Update)]
        [HttpGet]
        public ActionResult UpdatePost(ObjectId id)
        {

            Post post = rep.FindById(id);
            return View(post);
        }
        [AuthorizePost(Method.Update)]
        [HttpPost, ValidateInput(false)]
        public ActionResult UpdatePost(Post post)
        {
            Post pst = rep.FindById(post.Id);
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var mvcPath = "/Images/" + fileName;
                    var path = Server.MapPath("~" + mvcPath);
                    if (!System.IO.File.Exists(path))
                    {
                        file.SaveAs(path);
                    }
                    pst.ImageUrl = mvcPath;
                }
            }
            pst.Content = post.Content;
            pst.Header = post.Header;

            rep.Update(pst);

            return RedirectToAction("Index");
        }
        #endregion

        #region comment
        [HttpGet]
        public ActionResult AddComment(ObjectId id)
        {
            ViewBag.post = rep.FindById(id);
            return View();
        }

        [HttpPost]
        public ActionResult AddCommentToComment(ObjectId parentId, Comment comment)
        {
            Comment com = rep.FindCommentById(parentId);
            if (com != null)
            {
                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var mvcPath = "/Images/" + fileName;
                        var path = Server.MapPath("~" + mvcPath);
                        file.SaveAs(path);
                        comment.ImageUrl = mvcPath;
                    }
                }
                comment.CommentDate = DateTime.Now;
                comment.Id = ObjectId.GenerateNewId();
                comment.Author = HttpContext.CurrentUser();
                if (com.Replies == null)
                {
                    com.Replies = new List<Comment>();
                }
                com.Replies.Add(comment);
                rep.UpdateComment(com);
                return RedirectToAction("Index");
            }
            else
            {
                throw new Exception("Comment exception!");
            }
        }
        [HttpPost]
        public ActionResult AddComment(ObjectId post, Comment comment)
        {
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var mvcPath = "/Images/" + fileName;
                    var path = Server.MapPath("~" + mvcPath);
                    file.SaveAs(path);
                    comment.ImageUrl = mvcPath;
                }
            }
            comment.CommentDate = DateTime.Now;
            comment.Id = ObjectId.GenerateNewId();
            comment.Author = HttpContext.CurrentUser();
            rep.AddComment(post, comment);
            return RedirectToAction("Index");
        }
        [AuthorizePost(Method.DeleteComment, Roles = "admin")]
        [HttpGet]
        public ActionResult DeleteComment(ObjectId id)
        {
            rep.DeleteComment(id);
            return RedirectToAction("Index");
        }
        [AuthorizePost(Method.UpdateComment)]
        [HttpGet]
        public ActionResult UpdateComment(ObjectId id)
        {
            Comment comm = rep.FindCommentById(id);
            return View(comm);
        }
        [AuthorizePost(Method.UpdateComment)]
        [HttpPost]
        public ActionResult UpdateComment(Comment comment)
        {
            var oldComment = rep.FindCommentById(comment.Id);
            oldComment.Content = comment.Content;
            rep.UpdateComment(oldComment);
            return RedirectToAction("Index");
        }
        [Authorize]
        public FileResult DownloadCommentImage(ObjectId id)
        {
            Comment comment = rep.FindCommentById(id);
            string url = Server.MapPath("~" + comment.ImageUrl);
          //  "~/Images";// Relative path
          //  "C:\Users\seymour.h\Documents\visual studio 2013\Projects\testApp\testApp\Images" // Absolute path
            byte[] bytes = System.IO.File.ReadAllBytes(url);
            string fileName = comment.ImageUrl.Split('/').Last();
            return File(bytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
           // return View("");
        }
        //[HttpGet]
        public JsonResult Post(ObjectId id)
        {
            Post post = rep.FindById(id);
            return Json(post,JsonRequestBehavior.AllowGet);
        }
        public ContentResult Content()
        {
            return Content("<h1>maraqli content</h1>");
        }
        public PartialViewResult PostComments(ObjectId id)
        {
           // System.Threading.Thread.Sleep(10000);
            Post post = rep.FindById(id);
            return PartialView("_Comment", post.Comments);
        }
        public ActionResult PostDetails(ObjectId id)
        {
            Post post = rep.FindById(id);
            return View(post);
        }
        
        #endregion



    }
}
