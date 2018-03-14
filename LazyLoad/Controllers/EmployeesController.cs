using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BLL.Models;
using Microsoft.AspNet.Identity;

namespace LazyLoad.Controllers
{
    public class EmployeesController : Controller
    {

        // GET: Employees
        public ActionResult Index()
        {
            
            return View(DataStore<Employees>.Get());/*new List<Employees>().AsQueryable().Where(c=>c.Name=="aiman"))*/
            //return View(DataStore<Employees>.Get("Name=='aiman'"));
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employees employees = DataStore<Employees>.Find(id);
            if (employees == null)
            {
                return HttpNotFound();
            }
            return View(employees);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Position")] Employees employees)
        {
            if (ModelState.IsValid)
            {
                employees.UserID = User.Identity.GetUserId();
                DataStore<Employees>.Add(employees);
                return RedirectToAction("Index");
            }

            return View(employees);
        }

        //amira
        // GET: Employees/Create
        public ActionResult Createrange()
        {
            return View();
        }

        public ActionResult Createrange1()
        {
            return View();
        }
        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.




        [HttpPost]
        public JsonResult Createrange(List<Employees> data)
        {
            bool status = false;
            if (ModelState.IsValid)
            {


                if (ModelState.IsValid)
                {
                    DataStore<Employees>.AddRange(data);

                }
                status = true;
            }

            else
            {
                status = false;
            }
            return new JsonResult { Data = new { status = status } };
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Createrange(/*[Bind(Include = "ID,Name,Position")] */List<Employees> employees)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        DataStore<Employees>.AddRange(employees);
        //        return RedirectToAction("Index");
        //    }

        //    return View(employees);
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Createrange1(/*[Bind(Include = "ID,Name,Position")] */List<Employees> employees)
        {
            if (ModelState.IsValid)
            {
                DataStore<Employees>.AddRange(employees);
                return RedirectToAction("Index");
            }

            return View(employees);
        }


        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employees employees = DataStore<Employees>.Find(id);
            if (employees == null)
            {
                return HttpNotFound();
            }
            return View(employees);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Position")] Employees employees)
        {
            if (ModelState.IsValid)
            {
                DataStore<Employees>.Update(employees);
                return RedirectToAction("Index");
            }
            return View(employees);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employees employees = DataStore<Employees>.Find(id);
            if (employees == null)
            {
                return HttpNotFound();
            }
            return View(employees);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DataStore<Employees>.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
