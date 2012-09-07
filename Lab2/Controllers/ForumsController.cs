using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lab2.Models.Repositories;
using Lab2.Models.Entities;
using Lab2.ViewModels;
using Lab2.Models.SessionManager;

namespace Lab2.Controllers
{
    public class ForumsController : Controller
    {


        public ActionResult Index()
        {
            ThreadViewModel vm = new ThreadViewModel();
            vm.Threads = Repository.Instance.GetSortedThreads();
            vm.Posts = Repository.Instance.GetAllPosts();
            return View(vm);
        }

        //
        // GET: /Thread/
        public ActionResult Thread(Guid id)
        {
            List<User> users = Repository.Instance.GetSortedUsers(10, 0);
            ViewBag.Users = users;
            ThreadViewModel vm = new ThreadViewModel();
            vm.Thread = Repository.Instance.GetThreadById(id);
            vm.Posts = Repository.Instance.GetPostsByThreadID(id);
            return View(vm);
        }


        //
        // GET: /CreatePost/
        public ActionResult CreatePost(Guid id)
        {
            ViewBag.ThreadID = id;
            if (SessionManager.CurrentUser == null)
                return RedirectToAction("Index", "Forums");
                
            return View();
        }

        //
        // POST: /CreatePost/
        [HttpPost]
        public ActionResult CreatePost(Post post)
        {
            if (SessionManager.CurrentUser == null ||
                SessionManager.CurrentUser.userstatus != Models.Entities.User.UserStatus.Active)
                return RedirectToAction("Index", "Users");

            if (ModelState.IsValid)
            {
                post.CreatedByID = SessionManager.CurrentUser.ID;
                if (post.ThreadID == Guid.Empty) {
                    ForumThread thread = new ForumThread();
                    thread.Title = post.Title;
                    post.ThreadID = thread.ID;
                    Repository.Instance.Save<ForumThread>(thread);
                    Repository.Instance.Save<Post>(post);
                    return RedirectToAction("Thread", new { id = thread.ID });
                } else {
                    Repository.Instance.Save<Post>(post);
                    return RedirectToAction("Thread", new { id = post.ThreadID });
                }
            }

            return View();
        }

    }
}
