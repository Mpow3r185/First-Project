using FirstProject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FirstProject.Controllers
{
    public class DoctorsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ILogger<DoctorsController> _logger;

        public DoctorsController(ILogger<DoctorsController> logger, ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            ViewBag.HomePage = _context.Homes.ToList();
            ViewBag.Employees = _context.Employees.Where(x=>x.Roleid==23).ToList();
            ViewBag.ContactUs = _context.Contactus.ToList();
            ViewBag.AboutUs = _context.Aboutus.ToList();
            ViewBag.Services = _context.Services.ToList();
            ViewBag.Subscription = _context.Subscriptions.ToList();
            ViewBag.Testy = _context.Testimonials.ToList();
            ViewBag.NavBar = _context.Navbars.ToList();
            ViewBag.Footer = _context.Footers.ToList();
            ViewBag.Name = HttpContext.Session.GetString("Name");
            ViewBag.PatientID = HttpContext.Session.GetInt32("PatientID");

            return View();
        }
        [HttpGet]
        public IActionResult Search()
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
            var modelcontext = _context.Employees;
            return View(modelcontext);
        }
        [HttpPost]
        public async Task<IActionResult> Search(string fname, string lname)
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

            var modelcontext = _context.Employees;
            if (fname == null && lname == null)
            {
                return View(modelcontext);
            }
            else if (fname != null && lname == null)
            {
                var result = await modelcontext.Where(x => x.Fname.Equals(fname)).ToListAsync();
                return View(result);
            }
            else if (fname == null && lname != null)
            {
                var result = await modelcontext.Where(x => x.Lname.Equals(lname)).ToListAsync();
                return View(result);
            }
            else
            {
                var result = await modelcontext.Where(x => x.Fname == fname && x.Lname == lname).ToListAsync();
                return View(result);
            }


        }
        public IActionResult Patient()
        {
            //ViewBag.PatientID = HttpContext.Session.GetString("PatientID");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
