using ChrisTekhBlog.Core.Objects;
using ChrisTekhBlog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChrisTekhBlog.ViewModel
{
    public class ListViewModel
    {
       
        public IList<Post> Posts { get; set; }
        public int TotalPosts { get; set; }
        public Category Category { get; set; }
        public Tag Tag { get; set; }
        public string Search { get; set; }
    }
}