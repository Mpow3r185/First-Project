using FirstProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstProject.Controllers
{
    public class PatientHomeController : Controller
    {
        private readonly ModelContext _context;
        private readonly ILogger<PatientHomeController> _logger;
  
        public PatientHomeController(ModelContext context)
        {
            _context = context;
        
        }
        public IActionResult Index()
        {
            ViewBag.PatientID = HttpContext.Session.GetInt32("PatientID");
            ViewBag.PatientEmail = HttpContext.Session.GetString("PatientEmail");
            //ViewBag.Name = HttpContext.Session.GetString("Name");
            ViewBag.HomePage = _context.Homes.ToList();
            ViewBag.Employees = _context.Employees.ToList().Where(x => x.Roleid == 23);
            ViewBag.ContactUs = _context.Contactus.ToList();
            ViewBag.AboutUs = _context.Aboutus.ToList();
            ViewBag.Services = _context.Services.ToList();
            ViewBag.Subscription = _context.Subscriptions.ToList();
            ViewBag.Testy = _context.Testimonials.ToList();
            ViewBag.NavBar = _context.Navbars.ToList();
            ViewBag.Footer = _context.Footers.ToList();
            ViewBag.Patient = _context.Patients.ToList();
            ViewBag.finance = _context.Bills.Sum(x => x.Cost);
            ViewBag.Appoi = _context.Appointments.Select(x => x.Appointmentid).Count();
            ViewBag.nopatient = _context.Patients.Select(x => x.Patientid).Count();
            ViewBag.nodoc = _context.Employees.Select(x => x.Empid).Count() - 1;

            return View();
        }
        //public async Task<IActionResult> Index()
        //{
           

        //    return View();
        //}
    }
}
