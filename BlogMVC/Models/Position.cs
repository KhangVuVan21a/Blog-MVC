using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogMVC.Models
{
    public class Position
    {
        public int ID { get; set; }
        public string address { get; set; }
        public int BlogId { get; set; }
        public virtual Blog blog { get; set; }
    }
}