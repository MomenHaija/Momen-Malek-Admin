using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using University_Registration.Models;

namespace University_Registration.Controllers
{
    public class SectionsAdminController : Controller
    {
        private ERP_SystemEntities db = new ERP_SystemEntities();

        // GET: SectionsAdmin
       
        public ActionResult Index(int newid)
        {
            var sections = db.Sections.Include(s => s.Subject).Where(x=>x.Subject_ID==newid);
            Session["id"] = newid;
            return View(sections.ToList());
        }


       



        // GET: SectionsAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Section section = db.Sections.Find(id);
            if (section == null)
            {
                return HttpNotFound();
            }
            return View(section);
        }

        // GET: SectionsAdmin/Create
        public ActionResult Create()
        {
            ViewBag.Subject_ID = new SelectList(db.Subjects, "Subject_ID", "Name");
            return View();
        }

        // POST: SectionsAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Section_ID,SectionNumber,Subject_ID,SectionTime,SectionDay")] Section section)
        {
            if (ModelState.IsValid)
            {
                db.Sections.Add(section);
                db.SaveChanges();
                return RedirectToAction("Index","SectionsAdmin", new { newid = Session["id"] });
            }
            
            ViewBag.Subject_ID = new SelectList(db.Subjects, "Subject_ID", "Name", section.Subject_ID);
            return RedirectToAction("Create", "SectionsAdmin", new { newid = Session["id"] });

        }

        // GET: SectionsAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Section section = db.Sections.Find(id);
            if (section == null)
            {
                return HttpNotFound();
            }
            ViewBag.Subject_ID = new SelectList(db.Subjects, "Subject_ID", "Name", section.Subject_ID);
            return View(section);
        }

        // POST: SectionsAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Section_ID,SectionNumber,Subject_ID,SectionTime,SectionDay")] Section section)
        {
            if (ModelState.IsValid)
            {
                db.Entry(section).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","SectionsAdmin", new { newid = Session["id"] } );
            }
            ViewBag.Subject_ID = new SelectList(db.Subjects, "Subject_ID", "Name", section.Subject_ID);
            return View(section);
        }

        // GET: SectionsAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Section section = db.Sections.Find(id);
            if (section == null)
            {
                return HttpNotFound();
            }
            return View(section);
        }

        // POST: SectionsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Section section = db.Sections.Find(id);
                db.Sections.Remove(section);
                db.SaveChanges();
                return RedirectToAction("Index", "SectionsAdmin", new { newid = Session["id"] });
            }
            catch (Exception ex)
            {

                return View("ErrorAdmin");
            }
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
