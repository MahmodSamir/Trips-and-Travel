using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Trips_and_Travel.Models;

namespace Trips_and_Travel.Controllers
{
    public class PostsController : Controller
    {
        private TTDBEntities1 db = new TTDBEntities1();

        // GET: Posts
        public ActionResult Index()
        {
            if (Session["Id"] != null)
                return View();
            else { return RedirectToAction("Login", "Users"); }
        }
        public ActionResult Mine()
        {
            if (Session["Id"] != null)
            {
                var products = db.Posts.ToList();
                return View(products.Where(y => (int)Session["Id"] == y.AgencyID).ToList());
            }
            else { return RedirectToAction("Login", "Users"); }
          

        }

        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Login","Users");
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Posts/Create
        public ActionResult Create()
        {
            if (Session["Id"] != null)
                return View();
            else { return RedirectToAction("Login", "Users"); }
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Details,PostDate,Date,Destination,Photo,AgencyName,AgnecyID,Price")] Post post, HttpPostedFileBase imgFile)
        {
            if (ModelState.IsValid)
            {
                string path = "";
                if (imgFile.FileName.Length > 0)
                {
                    path = "~/image/" + Path.GetFileName(imgFile.FileName);
                    imgFile.SaveAs(Server.MapPath(path));
                }
                post.Photo = path;
                post.PostDate = DateTime.Now;
                post.AgencyName = (string)Session["username"];
                post.AgencyID = (int?)Session["Id"];
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(post);
        }

        // GET: Posts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Login","Users");
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Details,Date,Destination,Photo,Price,PostDate")] Post post, HttpPostedFileBase imgFile)
        {
            if (ModelState.IsValid)
            {
                string path = "";
                if (imgFile.FileName.Length > 0)
                {
                    path = "~/image/" + Path.GetFileName(imgFile.FileName);
                    imgFile.SaveAs(Server.MapPath(path));
                }
                post.Photo = path;
                db.Entry(post).State = EntityState.Modified;
                post.AgencyName = (string)Session["username"];
                post.AgencyID = (int?)Session["Id"];
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Login","Users");
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
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
