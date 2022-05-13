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
    public class UsersController : Controller
    {
        private TTDBEntities1 db = new TTDBEntities1();

        // GET: Users
        public ActionResult Index()
        {
            if (Session["Id"] != null)
                return View(db.Users.ToList());
            else { return RedirectToAction("Login", "Users"); }
        }

        public ActionResult Show()
        {
            if (Session["Id"] != null)
                return View(db.Users.ToList());
            else { return RedirectToAction("Login", "Users"); }
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Login", "Users");
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
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
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Login", "Users");
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,userName,Fname,Lname,Email,Password,phoneNumber,Photo,Role")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Login", "Users");
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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

        // GET: Users/Create
        public ActionResult Register()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "Id,userName,Fname,Lname,Email,Password,phoneNumber,Photo,Role")] User user , HttpPostedFileBase imgFile)
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
                return RedirectToAction("Login");
            }

            return View(user);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login([Bind(Include = "Email,Password")] User use)
        {
            var rec = db.Users.Where(x => x.Email == use.Email && x.Password == use.Password).ToList().FirstOrDefault();
            if (rec != null)
            {
                Session["username"] = rec.userName;
                Session["Role"] = rec.Role;
                Session["Id"] = rec.Id;
                if (rec.Role.Equals("Agency"))
                {
                    return RedirectToAction("Index","Posts");
                }
                else if (rec.Role.Equals("Admin"))
                {
                    return RedirectToAction("Index","Admin");
                }
                else
                {
                    return RedirectToAction("Index","Traveler");
                }
            }
            else
            {
                return View(use);
            }
        }

        public ActionResult Logout()
        {
            Session.RemoveAll();
            return RedirectToAction("Login");
        }

    }
}
