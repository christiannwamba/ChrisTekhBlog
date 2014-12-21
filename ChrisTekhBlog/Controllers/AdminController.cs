using ChrisTekhBlog.Core;
using ChrisTekhBlog.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChrisTekhBlog.Controllers
{
    //[Authorize]
    public class AdminController : Controller
    {
         private readonly IBlogRepository _blogRepository;

        public AdminController()
        {
            _blogRepository = new BlogRepository(new ApplicationDbContext());
        }
        //
        // GET: /Admin/
        [Route("Manage")]
        public ActionResult Manage()
        {
            return View();
        }
        [Route("Json/Posts")]
        public JsonResult Posts() {
            var posts = from p in _blogRepository.Posts() select new { p.Category.Name, p.PostedOn, p.Published, Tags = p.Tags.Select(t => t.Name), p.Title, p.UrlSlug, p.Id };
            return Json(posts, JsonRequestBehavior.AllowGet);
        }
        [Route("Json/Categories")]
        public JsonResult Categories()
        {
            var cats = from c in _blogRepository.Categories() select new {c.Description, c.Name, c.UrlSlug, c.Id};
            return Json(cats, JsonRequestBehavior.AllowGet);
        }
        
        //[Route("Json/CreatePost")]
        [HttpPost]
        [ValidateInput(false)] 
        public JsonResult CreatePost(Post post)
        {
           
                _blogRepository.CreatePost(post);
                return Json("200", JsonRequestBehavior.AllowGet);
            
            
        }
         [Route("Json/Post/{id}")]
        public JsonResult Post(int id) {

            var post = _blogRepository.Post(id);
            return Json(new{
            post.Description, post.CategoryId, post.Id, post.Meta, post.Modified, post.Published, post.ShortDescription, post.Title, post.UrlSlug
            }, JsonRequestBehavior.AllowGet);
        }

         public JsonResult UpdatePost(Post post)
         {
             _blogRepository.UpdatePost(post);
             return Json("200", JsonRequestBehavior.AllowGet);
         }
         public void DeletePost(int id)
         {
             _blogRepository.DeletePost(id);
         }

         [HttpPost]
         public JsonResult CreateCategory(Category category)
         {

             _blogRepository.CreateCategory(category);
             return Json("200", JsonRequestBehavior.AllowGet);


         }
         [Route("Json/Category/{id}")]
         public JsonResult Category(int id)
         {

             var category = _blogRepository.Category(id);
             return Json(new
             {
                 category.Description,
                 category.Name,
                 category.Id,
                 category.UrlSlug
             }, JsonRequestBehavior.AllowGet);
         }

         public JsonResult UpdateCategory(Category category)
         {
             _blogRepository.UpdateCategory(category);
             return Json("200", JsonRequestBehavior.AllowGet);
         }
         public void DeleteCategory(int id)
         {
             _blogRepository.DeleteCategory(id);
         }
	}
}