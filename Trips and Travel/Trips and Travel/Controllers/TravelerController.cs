using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Trips_and_Travel.Models;

namespace Trips_and_Travel.Controllers
{
    public class TravelerController : Controller
    {
        private TTDBEntities1 db = new TTDBEntities1();

        // GET: Traveler
        public ActionResult Index()
        {
                return View(db.Posts.ToList());
        }
        [HttpPost]
        public ActionResult Index(String catsearch)
        {
            if (Session["Id"] != null)
            {
                var products = db.Posts.ToList();
                if (catsearch != null && catsearch != "" && catsearch != " ")
                {
                    return View(products.Where(y => y.Price.ToString().Contains(catsearch) || y.AgencyName.Contains(catsearch) || y.Date.ToString().Contains(catsearch)).ToList());
                }
                else
                {
                    return View(db.Posts.ToList());
                }
            }
            else
            {
                    return RedirectToAction("Login", "Users"); 
            }

        }

        // GET: Traveler/Details/5
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

        // GET: Traveler/Create
        public ActionResult Create()
        {
            if (Session["Id"] != null)
                return View();
            else { return RedirectToAction("Login", "Users"); }
        }

        // POST: Traveler/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Details,PostDate,Date,Destination")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(post);
        }

        // GET: Traveler/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Login", "Users");
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Traveler/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Details,PostDate,Date,Destination")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: Traveler/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Login", "Users");
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Traveler/Delete/5
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
