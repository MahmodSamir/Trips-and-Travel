using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using Trips_and_Travel.Models;
using Trips_and_Travel.ViewModel;
using System.Data.Entity;
using System.IO;

namespace Trips_and_Travel.Controllers
{
    public class AdminController : Controller
    {
        private TTDBEntities1 db = new TTDBEntities1();

        // GET: Admin
        public ActionResult Index()
        {
            if(Session["Id"]!=null)
            return View();
            else { return RedirectToAction("Login", "Users"); }
        }
        //public ActionResult DBData()
        //{
        //    var tables = new Multiple
        //    {
        //        posting = db.Posts.ToList(),
        //        use= db.Users.ToList(),
        //    };
        //    return View(tables);
        //}
        public ActionResult ShowUsers()
        {
            if (Session["Id"] != null)
                return View(db.Users.ToList());
            else { return RedirectToAction("Login", "Users"); }
        }
        public ActionResult ShowInfo()
        {
            if (Session["Id"] == null)
            {
                 return RedirectToAction("Login", "Users");
            }
            User user = db.Users.Find(Session["Id"]);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        public ActionResult ShowPosts()
        {
            if (Session["Id"] != null)
                return View(db.Posts.ToList());
            else { return RedirectToAction("Login", "Users"); }
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
        public ActionResult Edit([Bind(Include = "Id,Title,Details,Date,Destination,Photo,Price,AgencyName,AgencyID,PostDate")] Post post, HttpPostedFileBase imgFile)
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
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }
        public ActionResult Edit1(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Login","Users");
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit1([Bind(Include = "Id,userName,Fname,Lname,Email,Password,phoneNumber,Photo,Role")] User user, HttpPostedFileBase imgFile)
        {
            if (ModelState.IsValid)
            {
                string path = "";
                if (imgFile.FileName.Length > 0)
                {
                    path = "~/image/" + Path.GetFileName(imgFile.FileName);
                    imgFile.SaveAs(Server.MapPath(path));
                }
                user.Photo = path;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                if(Session["Role"].Equals("Agency"))
                return RedirectToAction("Index","Posts");
                else
                {
                    return RedirectToAction("Index");
                }
            }
            return View(user);
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
        // GET: Users/Delete/5
        public ActionResult Delete1(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Login","Users");
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete1")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed1(int id)
        {
            User user = db.Users.Find(id);
              //Post post = db.Posts.Where(x => x.AgencyID == id).FirstOrDefault();
              //db.Posts.Remove(post);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("ShowUsers");
        }
        // GET: Users/Create
        public ActionResult Create()
        {
            if (Session["Id"] != null)
                return View();
            else { return RedirectToAction("Login", "Users"); }
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,userName,Fname,Lname,Email,Password,phoneNumber,Photo,Role")] User user, HttpPostedFileBase imgFile)
        {
            if (ModelState.IsValid)
            {
                string path = "";
                if (imgFile.FileName.Length > 0)
                {
                    path = "~/image/" + Path.GetFileName(imgFile.FileName);
                    imgFile.SaveAs(Server.MapPath(path));
                }
                user.Photo = path;
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("ShowUsers");
            }

            return View(user);
        }
    }
}