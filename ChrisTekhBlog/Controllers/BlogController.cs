using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChrisTekhBlog.Core;
using ChrisTekhBlog.ViewModel;

namespace ChrisTekhBlog.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogRepository _blogRepository;

        public BlogController()
        {
            _blogRepository = new BlogRepository(new ApplicationDbContext());
        }
        //
        // GET: /Blog/
        public ViewResult Posts(int p = 1)
        {
            // pick latest 10 posts
            var posts = _blogRepository.Posts(p - 1, 10);

            var totalPosts = _blogRepository.TotalPosts();

            var listViewModel = new ListViewModel();
            listViewModel.Posts = posts;
            listViewModel.TotalPosts = totalPosts;

            ViewBag.Title = "Latest Posts";

            return View("List", listViewModel);
        }
	}
}