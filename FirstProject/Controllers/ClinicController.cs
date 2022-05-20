using FirstProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstProject.Controllers
{
    public class ClinicController : Controller
    {
        private readonly ModelContext _context;
        private readonly ILogger<ClinicController> _logger;

        public ClinicController(ModelContext context, ILogger<ClinicController> logger)
        {
            _context = context;
            _logger = logger;

        }
        public IActionResult Clinic(int id)
        {
            ViewBag.PatientID = HttpContext.Session.GetInt32("PatientID");
            ViewBag.PatientEmail = HttpContext.Session.GetString("PatientEmail");
            ViewBag.Name = HttpContext.Session.GetString("Name");
            ViewBag.HomePage = _context.Homes.ToList();
            ViewBag.Employees = _context.Employees.ToList();
            ViewBag.ContactUs = _context.Contactus.ToList();
            ViewBag.AboutUs = _context.Aboutus.ToList();
            ViewBag.Services = _context.Services.ToList();
            ViewBag.Subscription = _context.Subscriptions.ToList();
            ViewBag.Testy = _context.Testimonials.ToList();
            ViewBag.NavBar = _context.Navbars.ToList();
            ViewBag.Footer = _context.Footers.ToList();
            ViewBag.Patient = _context.Patients.ToList();
            ViewBag.Clinic = _context.Clinics.ToList();
            ViewBag.ClinicID = HttpContext.Session.GetInt32("ClinicID");
            ViewBag.EmployeeID = HttpContext.Session.GetInt32("EmployeeID");
            var clinicID = _context.Clinics.Where(x => x.Clinicid == id).ToList();
            var docClinic = _context.Employees.Where(x => x.Clinicid == id).ToList();
            ViewBag.docClinic = docClinic;
            ViewBag.clinicId = clinicID;
            return View(clinicID);
        }
        public IActionResult Doc(int id)
        {
            ViewBag.PatientID = HttpContext.Session.GetInt32("PatientID");
            ViewBag.PatientEmail = HttpContext.Session.GetString("PatientEmail");
            ViewBag.Name = HttpContext.Session.GetString("Name");
            ViewBag.HomePage = _context.Homes.ToList();
            ViewBag.Employees = _context.Employees.ToList();
            ViewBag.ContactUs = _context.Contactus.ToList();
            ViewBag.AboutUs = _context.Aboutus.ToList();
            ViewBag.Services = _context.Services.ToList();
            ViewBag.Subscription = _context.Subscriptions.ToList();
            ViewBag.Testy = _context.Testimonials.ToList();
            ViewBag.NavBar = _context.Navbars.ToList();
            ViewBag.Footer = _context.Footers.ToList();
            ViewBag.Patient = _context.Patients.ToList();
            ViewBag.Clinic = _context.Clinics.ToList();
            ViewBag.ClinicID = HttpContext.Session.GetInt32("ClinicID");
            ViewBag.EmployeeID = HttpContext.Session.GetInt32("EmployeeID");
            var clinicID = _context.Clinics.Where(x => x.Clinicid == id).ToList();
            var docClinic = _context.Employees.Where(x => x.Clinicid == id).ToList();
            ViewBag.docClinic = docClinic;
            ViewBag.clinicId = clinicID;
            return View(docClinic);
        }
        public IActionResult Index()
        {
            ViewBag.PatientID = HttpContext.Session.GetInt32("PatientID");
            ViewBag.PatientEmail = HttpContext.Session.GetString("PatientEmail");
            ViewBag.Name = HttpContext.Session.GetString("Name");
            ViewBag.HomePage = _context.Homes.ToList();
            ViewBag.Employees = _context.Employees.ToList();
            ViewBag.ContactUs = _context.Contactus.ToList();
            ViewBag.AboutUs = _context.Aboutus.ToList();
            ViewBag.Services = _context.Services.ToList();
            ViewBag.Subscription = _context.Subscriptions.ToList();
            ViewBag.Testy = _context.Testimonials.ToList();
            ViewBag.NavBar = _context.Navbars.ToList();
            ViewBag.Footer = _context.Footers.ToList();
            ViewBag.Patient = _context.Patients.ToList();
            ViewBag.Clinic = _context.Clinics.ToList();
            ViewBag.ClinicID = HttpContext.Session.GetInt32("ClinicID");

            return View();
        }
        [HttpGet]
        public IActionResult Search()
        {
            ViewBag.HomePage = _context.Homes.ToList();
            ViewBag.Employees = _context.Employees.ToList();
            ViewBag.ContactUs = _context.Contactus.ToList();
            ViewBag.AboutUs = _context.Aboutus.ToList();
            ViewBag.Services = _context.Services.ToList();
            ViewBag.Subscription = _context.Subscriptions.ToList();
            ViewBag.Testy = _context.Testimonials.ToList();
            ViewBag.NavBar = _context.Navbars.ToList();
            ViewBag.Footer = _context.Footers.ToList();
            ViewBag.Name = HttpContext.Session.GetString("Name");
            ViewBag.PatientID = HttpContext.Session.GetInt32("PatientID");
            ViewBag.Clinic = _context.Clinics.ToList();
            var modelcontext = _context.Clinics;
            return View(modelcontext);
        }
        public async Task<IActionResult> Search(string clinicName)
        {
            ViewBag.HomePage = _context.Homes.ToList();
            ViewBag.Employees = _context.Employees.ToList();
            ViewBag.ContactUs = _context.Contactus.ToList();
            ViewBag.AboutUs = _context.Aboutus.ToList();
            ViewBag.Services = _context.Services.ToList();
            ViewBag.Subscription = _context.Subscriptions.ToList();
            ViewBag.Testy = _context.Testimonials.ToList();
            ViewBag.NavBar = _context.Navbars.ToList();
            ViewBag.Footer = _context.Footers.ToList();
            ViewBag.Clinic = _context.Clinics.ToList();
            ViewBag.Name = HttpContext.Session.GetString("Name");
            ViewBag.PatientID = HttpContext.Session.GetInt32("PatientID");

            var modelcontext = _context.Clinics;
            if (clinicName != null)
            {var result = await modelcontext.Where(x => x.Clincname.Equals(clinicName)).ToListAsync();
                return View(result);
               
            }
            else
            {
                 return View(modelcontext);
            }
         


        }
        //public async Task<IActionResult> Index()
        //{


        //    return View();
        //}
    }
}
