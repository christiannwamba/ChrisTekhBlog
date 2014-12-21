using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ChrisTekhBlog.Core.Objects
{
    public class Post
    {
       
        public Post()
        {
            PostedOn = DateTime.Now;
            
        }
        [Key]
        public  int Id
        { get; set; }
        [Required]
        [StringLength(500)]
        public  string Title
        { get; set; }
        [Required]
        [StringLength(5000)]
        public  string ShortDescription
        { get; set; }
        [Required]
        [MaxLength]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public  string Description
        { get; set; }

        [Required]
        [StringLength(1000)]
        public  string Meta
        { get; set; }
        [Required]
        [StringLength(200)]
        public  string UrlSlug
        { get; set; }
        [Required]
        public virtual bool Published
        { get; set; }
        [Required]
        public  DateTime PostedOn
        { get; set; }

        public  DateTime? Modified
        { get; set; }
        public int CategoryId 
        { get; set; }
        public virtual Category Category
        { get; set; }

        public virtual IList<Tag> Tags
        { get; set; }
    }
}
