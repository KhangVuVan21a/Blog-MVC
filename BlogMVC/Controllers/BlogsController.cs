using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BlogMVC.Models;

namespace BlogMVC.Controllers
{
    public class BlogsController : Controller
    {
        private BlogDBContext db = new BlogDBContext();
        List<string> listpos = new List<string>{"Việt Nam","Châu Á","Châu Âu","Châu Mỹ"};

        // GET: Blogs
        public ActionResult Index()
        {
            return View(db.Blogs.ToList());
        }

        // GET: Blogs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // GET: Blogs/Create
        public ActionResult Create()
        {
            var list = new SelectList(db.Categories.ToList(), "ID", "Title");
            ViewData["listCategories"] = list;
            return View();
        }

        // POST: Blogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,title,shortDetail,detail,thumb,status,datePublic,CategoryId")] Blog blog)
        {
            
            if (ModelState.IsValid)
            {
                
                db.Blogs.Add(blog);
                db.SaveChanges();
                for (int i = 1; i <= 4; i++)
                {
                    string checkbox = "checkbox" + i;
                    if (Request.Form[checkbox] != null)
                    {
                        Position position = new Position();
                        position.BlogId = blog.ID;
                        position.address = Request.Form[checkbox];
                        position.ID = 0;
                        db.Positions.Add(position);
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blog);
        }

        // GET: Blogs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            var list = new SelectList(db.Categories.ToList(), "ID", "Title");
            ViewData["listCategories"] = list;
            for (int i= 0;i < 4;i++)
            {
                string checkbox = "checkbox" + (i+1);
                if(blog.Positions.Where(m => m.address == listpos[i]).Count()>0)
                    ViewData[checkbox] = "checked";
                else
                {
                    ViewData[checkbox] = "";
                }
            }
            
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // POST: Blogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,title,shortDetail,detail,thumb,status,datePublic,CategoryId")] Blog blog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blog).State = EntityState.Modified;
                db.SaveChanges();
                db.Positions.RemoveRange(db.Positions.ToList().FindAll(m => m.BlogId == blog.ID).ToList());
                for (int i = 1; i <= 4; i++)
                {
                    string checkbox = "checkbox" + i;
                    if (Request.Form[checkbox] != null)
                    {
                        Position position = new Position();
                        position.BlogId = blog.ID;
                        position.address = Request.Form[checkbox];
                        position.ID = 0;
                        db.Positions.Add(position);
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blog);
        }

        // GET: Blogs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            db.Blogs.Remove(blog);
            db.Positions.RemoveRange(db.Positions.ToList().FindAll(m => m.BlogId == blog.ID).ToList());
            if (blog == null)
            {
                return HttpNotFound();
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Blog blog = db.Blogs.Find(id);
            db.Blogs.Remove(blog);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Searching (FormCollection fc, string title) {
            var blogsearching = from m in db.Blogs where m.title==title select m ;
            if (!String.IsNullOrEmpty(title))
            {
                blogsearching = db.Blogs.Where(m => m.title == title);
            }
            return View(blogsearching);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
