using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using University_Registration.Models;

namespace University_Registration.Controllers
{
    public class FacilitiesAdminController : Controller
    {
        private ERP_SystemEntities db = new ERP_SystemEntities();

        // GET: FacilitiesAdmin
        public ActionResult Index()
        {
            return View(db.Facilities.ToList());
        }

        [ActionName("Index")]
        [HttpPost]
        public ActionResult Index2(string search)
        {
            var y=db.Facilities.Where(p=>p.Name.Contains(search)).ToList();
            return View(y);
        }

        // GET: FacilitiesAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facility facility = db.Facilities.Find(id);
            if (facility == null)
            {
                return HttpNotFound();
            }
            return View(facility);
        }

        // GET: FacilitiesAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FacilitiesAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Facility_ID,Name,Description,Facility_Image")] Facility facility, HttpPostedFileBase  picture)
        {


            // Do something with the file name
            string fileName = fileName = Path.GetFileName(picture.FileName);
            

            // rest of the code here



            if (ModelState.IsValid)
            {

                string path = "../img" + Path.GetFileName(picture.FileName);
                picture.SaveAs(Server.MapPath(path));
                facility.Facility_Image= path.ToString();
                db.Facilities.Add(facility);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(facility);


        }

        // GET: FacilitiesAdmin/Edit/5
        
        public ActionResult Edit(int? id)
        {
            

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facility facility = db.Facilities.Find(id);
            if (facility == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = id;

            return View(facility);
        }

        // POST: FacilitiesAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id,[Bind(Include = "Facility_ID,Name,Description,Facility_Image")] Facility facility, HttpPostedFileBase picture,string imagepack)
        {
            
          



            if (ModelState.IsValid)
            {
                var existingModel = db.Facilities.AsNoTracking().FirstOrDefault(x => x.Facility_ID == id);

                if (picture != null)
                {
                    string path = "../../img" + picture.FileName;
                    picture.SaveAs(Server.MapPath(path));
                    facility.Facility_Image = path;


                }
                else
                {
                    facility.Facility_Image = existingModel.Facility_Image;
                }



                db.Entry(facility).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(facility);
        }

        // GET: FacilitiesAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facility facility = db.Facilities.Find(id);
            if (facility == null)
            {
                return HttpNotFound();
            }
            return View(facility);
        }

        // POST: FacilitiesAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Facility facility = db.Facilities.Find(id);
                db.Facilities.Remove(facility);
                db.SaveChanges();
                return RedirectToAction("Index");
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
