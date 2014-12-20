using ChrisTekhBlog.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChrisTekhBlog.Core
{
    public interface IBlogRepository
    {
        //All Posts
        IList<Post> Posts(int pageNo, int pageSize);
        int TotalPosts();

        //by Cat
        IList<Post> PostsForCategory(string categorySlug, int pageNo, int pageSize);
        int TotalPostsForCategory(string categorySlug);
        Category Category(string categorySlug);

        //by Tag
        IList<Post> PostsForTag(string tagSlug, int pageNo, int pageSize);
        int TotalPostsForTag(string tagSlug);
        Tag Tag(string tagSlug);

        //by Search
        IList<Post> PostsForSearch(string search, int pageNo, int pageSize);
        int TotalPostsForSearch(string search);

        //Detail
        Post Post(int year, int month, string titleSlug);

        //Cats
        IList<Category> Categories();
        IList<Tag> Tags();
    }
}
