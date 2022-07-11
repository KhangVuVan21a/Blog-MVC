using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BlogMVC.Models
{
    public class Blog
    {
        public int ID { get; set; }
        public string title { get; set; }
        public string shortDetail { get; set; }
        public string detail { get; set; }
        public string thumb { get; set; }
        public bool status { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime datePublic { get; set; }
        public int CategoryId { get; set; }
        public virtual Category category { get; set; }

        public virtual ICollection<Position> Positions { get; set; }
    }
    public class BlogDBContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Position> Positions { get; set; }
    }
    

}