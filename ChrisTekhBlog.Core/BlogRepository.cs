using ChrisTekhBlog.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;

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

            return query.Single();
        }
    }
}
