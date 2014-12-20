using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChrisTekhBlog.Core.Objects
{
    public class Category
    {
        public virtual int Id
        { get; set; }
        [Required]
        [StringLength(50)]
        public virtual string Name
        { get; set; }
        [Required]
        [StringLength(50)]
        public virtual string UrlSlug
        { get; set; }
        
        [StringLength(50)]
        public virtual string Description
        { get; set; }

        public virtual IList<Post> Posts
        { get; set; }
    }
}
