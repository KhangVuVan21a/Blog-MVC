using System;
using System.Collections.Generic;
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
        public DateTime datePublic { get; set; }
        public Category category { get; set; }
    }
    public class BlogDBContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }

        public DbSet<Category> Categories { get; set; }
    }
    public class Category
    {
        public int ID { get; set; }
        public string Title { get; set; }

    }
    public class CategoryDBContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
    }
    public class Position
    {
        public int ID { get; set; }
        public string address { get; set; }
        public Blog blog { get; set; }
    }
    public class PositionDBContext : DbContext
    {
        public DbSet<Position> Positions { get; set; }
    }
}