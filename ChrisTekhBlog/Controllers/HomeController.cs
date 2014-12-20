using ChrisTekhBlog.Core;
using ChrisTekhBlog.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChrisTekhBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBlogRepository _blogRepository;

        public HomeController()
        {
            _blogRepository = new BlogRepository(new ApplicationDbContext());
        }
        public ActionResult Index(int p = 1)
        {
            // pick latest 10 posts
            var posts = _blogRepository.Posts(p - 1, 10);

            var totalPosts = _blogRepository.TotalPosts();

            var listViewModel = new ListViewModel();
            listViewModel.Posts = posts;
            listViewModel.TotalPosts = totalPosts;

            ViewBag.Header = "ChrisTekh Blog";
            ViewBag.SubHeader = "\"A resource for Tech. Info. from ChrisTeck!\"";
            ViewBag.Title = "Home";
            return View("List", listViewModel);
        }
        public ViewResult Category(string category, int p = 1)
        {
            var viewmodel = new ListViewModel();
            viewmodel.Category = _blogRepository.Category(category);
            viewmodel.Posts = _blogRepository.PostsForCategory(category, p - 1, 10);
            viewmodel.TotalPosts = _blogRepository.TotalPostsForCategory(category);

            if (viewmodel.Category == null)
                throw new HttpException(404, "Category not found");
            ViewBag.Header = viewmodel.Category.Name;
            ViewBag.SubHeader = viewmodel.Category.Description;
            ViewBag.Title = String.Format(@"Latest posts on category ""{0}""",
                                viewmodel.Category.Name);
            return View("List", viewmodel);
        }
        public ViewResult Tag(string tag, int p = 1)
        {
            var viewmodel = new ListViewModel();
            viewmodel.Tag = _blogRepository.Tag(tag);
            viewmodel.Posts = _blogRepository.PostsForTag(tag, p - 1, 10);
            viewmodel.TotalPosts = _blogRepository.TotalPostsForTag(tag);

            if (viewmodel.Tag == null)
                throw new HttpException(404, "Category not found");
            ViewBag.Header = viewmodel.Tag.Name;
            ViewBag.SubHeader = viewmodel.Tag.Description;
            ViewBag.Title = String.Format(@"Latest posts on tag ""{0}""",
                                viewmodel.Tag.Name);
            return View("List", viewmodel);
        }
        [Route("Search")]
        public ViewResult Search(string s, int p = 1)
        {
            ViewBag.Title = String.Format(@"Lists of posts found 
                        for search text ""{0}""", s);

            var viewModel = new ListViewModel();
            viewModel.Search = s;
            viewModel.Posts = _blogRepository.PostsForSearch(s, p - 1, 10);
            viewModel.TotalPosts = _blogRepository.TotalPostsForSearch(s);
            ViewBag.Header = "Search";
            ViewBag.SubHeader = "\""+s+"\"";
            return View("List", viewModel);
        }
        public ViewResult Post(int year, int month, string title)
        {
            var post = _blogRepository.Post(year, month, title);

            if (post == null)
                throw new HttpException(404, "Post not found");

            if (post.Published == false && User.Identity.IsAuthenticated == false)
                throw new HttpException(401, "The post is not published");
            ViewBag.Header = post.Title;
            ViewBag.SubHeader = "\"" + post.Category.Name + "\"";
            return View(post);
        }
        public ActionResult About()
        {
            ViewBag.Header = "About the Blog!";
            ViewBag.SubHeader = "\"This is what it is!\"";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Header = "Contact Us!";
            ViewBag.SubHeader = "\"This is how you get to us!\"";

            return View();
        }
    }
}