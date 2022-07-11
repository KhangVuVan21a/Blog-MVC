using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogMVC.Models
{
    public class Category
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public virtual ICollection<Blog> Blogs { get; set; }

    }
}