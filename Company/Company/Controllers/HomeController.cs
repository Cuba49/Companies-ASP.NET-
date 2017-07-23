using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Company.Models;
using System.Data.Entity;

namespace Company.Controllers
{
    public class HomeController : Controller
    {
        private Models.DBCompanyEntities3 db = new Models.DBCompanyEntities3();
        public ActionResult Index()
        {
            
            var Items = db.Companies;
            ViewBag.Menu = db.Companies;
            return View(Items);
        }
        public ActionResult Add(int? id)
        {
            
            SelectList companies = new SelectList(db.Companies, "Id", "Name", id);
           
            ViewBag.Companies = companies;
            ViewBag.Selected = id;
            
            return View();
        }

        [HttpPost]
        public ActionResult Add(Models.Company company)
        {
            if (company.FK_Parent == null)
                company.FK_Parent = 0;
            db.Companies.Add(company);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        static int? firstValue = 0;

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Models.Company company = db.Companies.Find(id);
            if (company != null)
            {
                SelectList companies = new SelectList(db.Companies, "Id", "Name", company.Id);
                ViewBag.Companies = companies;
                firstValue = company.MyValue;
                return View(company);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(Models.Company company)
        {
            if (company.Id != company.FK_Parent)
            {
                db.Entry(company).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        //public void AddValue(int? newValue, int? ToAdd) {

        //    if (ToAdd != 0)
        //    {
        //        Models.Company company = db.Companies.Find(ToAdd);
        //        company.Value += newValue - firstValue;
        //        db.Entry(company).State = EntityState.Modified;
        //        db.SaveChanges();
        //        AddValue(newValue, company.FK_Parent);
        //    }
        //}
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null||id ==0)
            {
                return HttpNotFound();
            }
            Models.Company company = db.Companies.Find(id);
            if (company != null)
            {
                SelectList companies = new SelectList(db.Companies, "Id", "Name", company.Id);
                ViewBag.Companies = companies;
                firstValue = company.MyValue;
                return View(company);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(Models.Company company)
        {
            foreach(var childs in db.Companies.Where(p=>p.FK_Parent== company.Id))
            {
                childs.FK_Parent = 0;
                db.Entry(childs).State = EntityState.Modified;
               
            }
            Models.Company company1 = db.Companies.Find(company.Id);
            db.Companies.Remove(company1);
            
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}