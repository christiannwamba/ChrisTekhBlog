using ChrisTekhBlog.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using System.Data.Entity;

namespace ChrisTekhBlog.Core
{
    public class BlogRepository : IBlogRepository
    {
        private readonly ApplicationDbContext _context;
        public BlogRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IList<Post> Posts(int pageNo, int pageSize)
        {
            var posts = _context.Post
                              .Where(p => p.Published)
                              .OrderByDescending(p => p.PostedOn)
                              .Skip(pageNo * pageSize)
                              .Take(pageSize)
                              .ToList();

            return posts;
        }
        public int TotalPosts()
        {

            return _context.Post.Where(p => p.Published).Count();
        }

        //By Cat
        public IList<Post> PostsForCategory(string categorySlug, int pageNo, int pageSize)
        {
            var posts = _context.Post
                                .Where(p => p.Published && p.Category.UrlSlug.Equals(categorySlug))
                                .OrderByDescending(p => p.PostedOn)
                                .Skip(pageNo * pageSize)
                                .Take(pageSize)
                                .ToList();

            var postIds = posts.Select(p => p.Id).ToList();

            return _context.Post
                          .Where(p => postIds.Contains(p.Id))
                          .OrderByDescending(p => p.PostedOn)
                          .ToList();
        }

        public int TotalPostsForCategory(string categorySlug)
        {
            return _context.Post
                        .Where(p => p.Published && p.Category.UrlSlug.Equals(categorySlug))
                        .Count();
        }

        public Category Category(string categorySlug)
        {
            return _context.Category
                        .FirstOrDefault(t => t.UrlSlug.Equals(categorySlug));
        }


        //By Tag
        public IList<Post> PostsForTag(string tagSlug, int pageNo, int pageSize)
        {
            var posts = _context.Post
                               .Where(p => p.Published && p.Tags.Any(t => t.UrlSlug.Equals(tagSlug)))
                                .OrderByDescending(p => p.PostedOn)
                                .Skip(pageNo * pageSize)
                                .Take(pageSize)
                                .ToList();

            var postIds = posts.Select(p => p.Id).ToList();

            return _context.Post
                          .Where(p => postIds.Contains(p.Id))
                          .OrderByDescending(p => p.PostedOn)
                          .ToList();
        }

        public int TotalPostsForTag(string tagSlug)
        {
            return _context.Post
                         .Where(p => p.Published && p.Tags.Any(t => t.UrlSlug.Equals(tagSlug)))
                           .Count();
        }

        public Tag Tag(string tagSlug)
        {
            return _context.Tag
                        .FirstOrDefault(t => t.UrlSlug.Equals(tagSlug));
        }

        //by Search
        public IList<Post> PostsForSearch(string search, int pageNo, int pageSize)
        {
            var posts = _context.Post
                                  .Where(p => p.Published && (p.Title.Contains(search) || p.Category.Name.Equals(search) || p.Tags.Any(t => t.Name.Equals(search))))
                                  .OrderByDescending(p => p.PostedOn)
                                  .Skip(pageNo * pageSize)
                                  .Take(pageSize)
                                  .ToList();

            var postIds = posts.Select(p => p.Id).ToList();

            return _context.Post
                  .Where(p => postIds.Contains(p.Id))
                  .OrderByDescending(p => p.PostedOn)
                  .ToList();
        }

        public int TotalPostsForSearch(string search)
        {
            return _context.Post
                    .Where(p => p.Published && (p.Title.Contains(search) || p.Category.Name.Equals(search) || p.Tags.Any(t => t.Name.Equals(search))))
                    .Count();
        }

        //Post Detail
        public Post Post(int year, int month, string titleSlug)
        {
            var query = _context.Post
                                .Where(p => p.PostedOn.Year == year && p.PostedOn.Month == month && p.UrlSlug.Equals(titleSlug));
            
            query.SelectMany(p => p.Tags);

            return query.FirstOrDefault();
        }

        //Cats
        public IList<Category> Categories()
        {
            return _context.Category.OrderBy(p => p.Name).ToList();
        }
        public IList<Tag> Tags()
        {
            return _context.Tag.OrderBy(p => p.Name).ToList();
        }
        //Admin
        public IList<Post> Posts()
        {
            //var posts = from p in _context.Post orderby p.PostedOn descending select new { p.Category, p.PostedOn, p.Published, Tags =  p.Tags.Select(t=>t.Name), p.Title, p.UrlSlug};
            return _context.Post.OrderByDescending(x => x.PostedOn).ToList();
        }
        //Create Post
        public void CreatePost(Post post)
        {
            _context.Post.Add(post);
            _context.SaveChanges();
            
        }
        //Post by Id
        public Post Post(int id)
        {
            return _context.Post.Find(id);
        }
        public void UpdatePost(Post post)
        {
            if (post != null)
            {
                var post2 = (from p in _context.Post where p.Id == post.Id select p).FirstOrDefault();
                post2.CategoryId = post.CategoryId;
                post2.Description = post.Description;
                post2.Meta = post.Meta;
                post2.Published = post.Published;
                post2.ShortDescription = post.ShortDescription;
                post2.Title = post.Title;
                post2.UrlSlug = post.UrlSlug;
                _context.Entry(post2).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public void DeletePost(int id)
        {
            Post post = _context.Post.Find(id);
            _context.Post.Remove(post);
            _context.SaveChanges();
        }

        //Admin Cat
       
        //Create Category
        public void CreateCategory(Category Category)
        {
            _context.Category.Add(Category);
            _context.SaveChanges();

        }
        //Category by Id
        public Category Category(int id)
        {
            return _context.Category.Find(id);
        }
        public void UpdateCategory(Category Category)
        {
            if (Category != null)
            {
                var Category2 = (from p in _context.Category where p.Id == Category.Id select p).FirstOrDefault();
                Category2.Name = Category.Name;
                Category2.Description = Category.Description;
                Category2.UrlSlug = Category.UrlSlug;
                _context.Entry(Category2).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public void DeleteCategory(int id)
        {
            Category category = _context.Category.Find(id);
            _context.Category.Remove(category);
            _context.SaveChanges();
        }
    }
}
