using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LazyLoad.Models;

namespace LazyLoad.Controllers
{
    public class UserRoleGruopsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UserRoleGruops
        public ActionResult Index()
        {
            return View(db.UserRoleGruops.ToList());
        }

        // GET: UserRoleGruops/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRoleGruop userRoleGruop = db.UserRoleGruops.Find(id);
            if (userRoleGruop == null)
            {
                return HttpNotFound();
            }
            return View(userRoleGruop);
        }
        public ActionResult RoleGruopJoinUsersList(Guid id)
        {
            return PartialView(db.RoleGruopJoinUsers.Where(c => c.UserRoleGruopID == id).ToList());
        }
        public ActionResult AddUserToGruop(Guid id)
        {
            ViewBag.UserID = new SelectList(db.Users, "Id", "UserName");
            return View(new RoleGruopJoinUsers() { UserRoleGruopID = id });
        }
        [HttpPost]
        public ActionResult AddUserToGruop(RoleGruopJoinUsers model)
        {
            if(ModelState.IsValid)
            {
                db.RoleGruopJoinUsers.Add(model);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = model.UserRoleGruopID });
            }
            ViewBag.UserID = new SelectList(db.Users, "Id", "UserName");
            return View(new RoleGruopJoinUsers() { UserRoleGruopID = model.UserRoleGruopID });
        }
        public ActionResult GroupRoles()
        {
            return PartialView();
        }
        public ActionResult GroupRolesDelete(Guid UserRoleGruopID, Guid UserRoleID)
        {
            var roleJoinRoleGruop = db.RoleJoinRoleGruops.Where(c => c.UserRoleID == UserRoleID && c.UserRoleGruopID == UserRoleGruopID).SingleOrDefault();
            if(roleJoinRoleGruop!=null)
            {
                db.RoleJoinRoleGruops.Remove(roleJoinRoleGruop);
                db.SaveChanges();
            }
            return RedirectToAction("Details", new { id = UserRoleGruopID });
        }
        public ActionResult GroupRolesCreate(Guid id)
        {
            return View(new RoleJoinRoleGruop() { UserRoleGruopID = id });
        }
        [HttpPost]
        public ActionResult GroupRolesCreate(RoleJoinRoleGruop model)
        {
            model.UserRole.ID = Guid.NewGuid();
            db.RoleJoinRoleGruops.Add(model);
            db.SaveChanges();
            return RedirectToAction("Details",new { id = model.UserRoleGruopID });
        }
        // GET: UserRoleGruops/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserRoleGruops/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] UserRoleGruop userRoleGruop)
        {
            if (ModelState.IsValid)
            {
                userRoleGruop.ID = Guid.NewGuid();
                db.UserRoleGruops.Add(userRoleGruop);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userRoleGruop);
        }

        // GET: UserRoleGruops/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRoleGruop userRoleGruop = db.UserRoleGruops.Find(id);
            if (userRoleGruop == null)
            {
                return HttpNotFound();
            }
            return View(userRoleGruop);
        }

        // POST: UserRoleGruops/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] UserRoleGruop userRoleGruop)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userRoleGruop).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userRoleGruop);
        }

        // GET: UserRoleGruops/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRoleGruop userRoleGruop = db.UserRoleGruops.Find(id);
            if (userRoleGruop == null)
            {
                return HttpNotFound();
            }
            return View(userRoleGruop);
        }

        // POST: UserRoleGruops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            UserRoleGruop userRoleGruop = db.UserRoleGruops.Find(id);
            db.UserRoleGruops.Remove(userRoleGruop);
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
