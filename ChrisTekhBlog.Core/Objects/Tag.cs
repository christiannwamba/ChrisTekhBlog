using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ChrisTekhBlog.Core.Objects
{
    public class Tag
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
         [StringLength(200)]
        public virtual string Description
        { get; set; }

        public virtual IList<Post> Posts
        { get; set; }
    }
}
