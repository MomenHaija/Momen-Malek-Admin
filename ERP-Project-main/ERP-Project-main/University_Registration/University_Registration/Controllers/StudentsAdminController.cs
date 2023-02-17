using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using University_Registration.Models;

namespace University_Registration.Controllers
{
    public class StudentsAdminController : Controller
    {
        private ERP_SystemEntities db = new ERP_SystemEntities();

        // GET: StudentsAdmin
        public ActionResult Index()
        {
            var students = db.Students.Include(s => s.AspNetUser).Include(s => s.Major);
            return View(students.ToList());
        }
        
        // GET: StudentsAdmin/Details/5
        [ActionName("Index")]
        [HttpPost]
        public ActionResult Index2(string search)
        {
            var students = db.Students.Where(p=>p.Name.Contains(search)).Include(s => s.AspNetUser).Include(s => s.Major);
            return View(students.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Student student = db.Students.Find(id);

            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);

        }

        // GET: StudentsAdmin/Create
        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.Major_ID = new SelectList(db.Majors, "Major_ID", "Name");
            return View();
        }


        // POST: StudentsAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Student_ID,Id,Name,Email,Password,NationalNum,Grad,Pic,Status,PersonalIdFile,CertificateFile,Gender,Major_ID")] Student student)
        {

            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            ViewBag.Id = new SelectList(db.AspNetUsers, "Id", "Email", student.Id);
            ViewBag.Major_ID = new SelectList(db.Majors, "Major_ID", "Name", student.Major_ID);
            return View(student);
        }

        // GET: StudentsAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);

            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.AspNetUsers, "Id", "Email", student.Id);

            ViewBag.Major_ID = new SelectList(db.Majors, "Major_ID", "Name", student.Major_ID);

            return View(student);
        }
        //----------------------------------------------------------------------





        public ActionResult Accept(int? id)
        {

            Student student = db.Students.Find(id);

            student.Status = 1;

            db.SaveChanges();
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(student.Email);
                mail.From = new MailAddress("obidatm68@gmail.com");

                mail.Subject = "congratulations your Accepeted in the Malek University";
                mail.Body = $"hello {student.Name} your email  {student.Email}  your password is {student.Password}";
                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient();
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Host = "smtp.gmail.com";
                smtp.Credentials = new System.Net.NetworkCredential("obidatm68@gmail.com", "zrwtqpllpsbviqws");
                smtp.Send(mail);
            }
            catch
            {
                return View("ErrorAdmin2");
            }

            return View("Index", db.Students.ToList());

        }




        public ActionResult Acceptt()
        {
            var acc = db.Students.Where(a => a.Status == 1).ToList();
            db.SaveChanges();
            return View("Acceptt", acc);

        }

        
        [HttpPost]
        [ActionName("Acceptt")]
        public ActionResult Index3(string search)
        {
            var students = db.Students.Where(p => p.Name.Contains(search) && p.Status == 1).Include(s => s.AspNetUser).Include(s => s.Major);
            return View("Acceptt",students.ToList());
        }


        public ActionResult Reject(int? id)
        {
            Student student = db.Students.Find(id);
            student.Status = 2;

            db.SaveChanges();

            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(student.Email);
                mail.From = new MailAddress("obidatm68@gmail.com");

                mail.Subject = "Sorry  your Rejected in the Malek University";
                mail.Body = $"Don't lose hope, please reapply next time";
                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient();
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Host = "smtp.gmail.com";
                smtp.Credentials = new System.Net.NetworkCredential("obidatm68@gmail.com", "zrwtqpllpsbviqws");
                smtp.Send(mail);
            }
            catch(Exception ex) {
                return View("ErrorAdmin2");
            }
            return View("Index", db.Students.ToList());


        }

        public ActionResult Rejectt()
        {

            var acc = db.Students.Where(a => a.Status == 2).ToList();

            db.SaveChanges();
            return View("Rejectt", acc);
        }
        [HttpPost]
        [ActionName("Rejectt")]
        public ActionResult Index4(string search)
        {
            var students = db.Students.Where(p => p.Name.Contains(search) && p.Status == 2).Include(s => s.AspNetUser).Include(s => s.Major);
            return View("Rejectt", students.ToList());
        }
        

        //-----------------------------------------------------------------------

      // POST: StudentsAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Student_ID,Id,Name,Email,Password,NationalNum,Grad,Pic,Status,PersonalIdFile,CertificateFile,Gender,Major_ID")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.AspNetUsers, "Id", "Email", student.Id);
            ViewBag.Major_ID = new SelectList(db.Majors, "Major_ID", "Name", student.Major_ID);
            return View(student);
        }

        // GET: StudentsAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);

        }




        // POST: StudentsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Student student = db.Students.Find(id);
                db.Students.Remove(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception ex)
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
