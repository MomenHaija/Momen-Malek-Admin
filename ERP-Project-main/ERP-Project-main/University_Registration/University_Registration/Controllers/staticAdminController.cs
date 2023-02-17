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
    public class staticAdminController : Controller
    {

        private ERP_SystemEntities db = new ERP_SystemEntities();

        // GET: staticAdmin
        public ActionResult Index()
        {
            Student students= new Student(); 
            int count=db.Students.Where(p=>p.Status==1).Count();
            ViewBag.Students = count;
            
            SubjectRegistration revenu=new SubjectRegistration();
            var t = (from b in db.SubjectRegistrations where b.PaymentStatus == true
                  select b.Price).Sum();
            ViewBag.Amount = t;


            var Facilitynumber =db.Facilities.Count();
            ViewBag.Facilitynumber = Facilitynumber;

            var numberofmajor=db.Majors.Count();
            ViewBag.numberofmajor = numberofmajor;  

            var unpayment=(from p in db.SubjectRegistrations 
                  select p).Where(k=>k.PaymentStatus==false).Count();
            var payment=(from f in db.SubjectRegistrations
            select f).Where(k=>k.PaymentStatus==true).Count();

            var list = new int[] {unpayment,payment};

            return View(list);
        }
    }
}