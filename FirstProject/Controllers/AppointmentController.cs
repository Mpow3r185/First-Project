using FirstProject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstProject.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ILogger<AppointmentController> _logger;

        public AppointmentController(ILogger<AppointmentController> logger, ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            ViewBag.HomePage = _context.Homes.ToList();
            ViewBag.Employees = _context.Employees.Where(x => x.Roleid == 23).ToList();
            ViewBag.ContactUs = _context.Contactus.ToList();
            ViewBag.AboutUs = _context.Aboutus.ToList();
            ViewBag.Services = _context.Services.ToList();
            ViewBag.Subscription = _context.Subscriptions.ToList();
            ViewBag.Testy = _context.Testimonials.ToList();
            ViewBag.NavBar = _context.Navbars.ToList();
            ViewBag.Footer = _context.Footers.ToList();
            ViewBag.Name = HttpContext.Session.GetString("Name");
            ViewBag.PatientID = HttpContext.Session.GetInt32("PatientID");
            ViewBag.EmployeeIDD = HttpContext.Session.GetInt32("EmployeeIDD");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Appointmentid,Healthcase,Status,Appointmenttime,Empid,Patientid")] Appointment appointment, string fname, string lname,string docname, string email)
        {
            if (ModelState.IsValid)
            {
                appointment.Status = "0";
                appointment.Empid = HttpContext.Session.GetInt32("EmployeeIDD");
                appointment.Patientid = _context.Patients.Where(m => m.Email == email).Select(m => m.Patientid).SingleOrDefault();
              


                _context.Add(appointment);
                await _context.SaveChangesAsync();


                return RedirectToAction("Index","Clinic");
            }

            return View(appointment);
        }
    }
}
