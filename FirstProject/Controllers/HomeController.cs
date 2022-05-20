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
    public class HomeController : Controller
    {
     
            private readonly ModelContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;
            private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            ViewBag.HomePage = _context.Homes.ToList();
            ViewBag.Employees = _context.Employees.ToList().Where(x=>x.Roleid==23);
            ViewBag.ContactUs = _context.Contactus.ToList();
            ViewBag.AboutUs = _context.Aboutus.ToList();
            ViewBag.Services = _context.Services.ToList();
            ViewBag.Subscription = _context.Subscriptions.ToList();
            ViewBag.Testy = _context.Testimonials.ToList();
            ViewBag.NavBar = _context.Navbars.ToList();
            ViewBag.Footer = _context.Footers.ToList();
            ViewBag.Name = HttpContext.Session.GetString("Name");
            ViewBag.PatientID = HttpContext.Session.GetInt32("PatientID");
            ViewBag.EmployeeID = HttpContext.Session.GetInt32("EmployeeID");
            ViewBag.finance = _context.Bills.Sum(x => x.Cost);
            ViewBag.Appoi = _context.Appointments.Select(x => x.Appointmentid).Count();
            ViewBag.nopatient = _context.Patients.Select(x => x.Patientid).Count();
            ViewBag.nodoc = _context.Employees.Select(x => x.Empid).Count() - 1;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Admin()
        {
            ViewBag.finance = _context.Bills.Sum(x => x.Cost);
            ViewBag.Appoi = _context.Appointments.Select(x => x.Appointmentid).Count();
            ViewBag.nopatient = _context.Patients.Select(x => x.Patientid).Count();
            ViewBag.nodoc = _context.Employees.Select(x => x.Empid).Count() - 1;
            ViewBag.EmployeeID = HttpContext.Session.GetInt32("EmployeeID");
            return View();
        }
        public IActionResult Financial()

        {
            ViewBag.finance = _context.Bills.Sum(x => x.Cost);
            ViewBag.Appoi = _context.Appointments.Select(x => x.Appointmentid).Count();
            ViewBag.nopatient = _context.Patients.Select(x => x.Patientid).Count();
            ViewBag.nodoc = _context.Employees.Select(x => x.Empid).Count()-1;
            ViewBag.EmployeeID = HttpContext.Session.GetInt32("EmployeeID");
            return View();
        }
        public IActionResult Doctor()
        {
            ViewBag.finance = _context.Bills.Sum(x => x.Cost);
            ViewBag.Appoi = _context.Appointments.Select(x => x.Appointmentid).Count();
            ViewBag.nopatient = _context.Patients.Select(x => x.Patientid).Count();
            ViewBag.nodoc = _context.Employees.Select(x => x.Empid).Count() - 1;
            ViewBag.EmployeeID = HttpContext.Session.GetInt32("EmployeeID");
            return View();
        }
        public IActionResult AppointmentPending()
        {
            ViewBag.EmployeeID = HttpContext.Session.GetInt32("EmployeeID");
            var id = ViewBag.EmployeeID;
            var employee = _context.Employees.Where(x => x.Roleid == 23).ToList();
            var appo = _context.Appointments.ToList().Where(x => x.Empid == id).Where(x=>x.Status=="0");
            var patient = _context.Patients.ToList();

            var multitable = from e in employee
                             join ap in appo on e.Empid equals ap.Empid
                             join p in patient on ap.Patientid equals p.Patientid
                             select new JoinTable { patient = p, employee = e, appointment = ap };
            return View(multitable);
        }
        public IActionResult Appointment()
        {
            ViewBag.EmployeeID = HttpContext.Session.GetInt32("EmployeeID");
            var id = ViewBag.EmployeeID;
            var employee = _context.Employees.Where(x => x.Roleid == 23).ToList();
            var appo = _context.Appointments.ToList().Where(x => x.Empid == id).Where(x => x.Status != "0");
            var patient = _context.Patients.ToList();

            var multitable = from e in employee
                             join ap in appo on e.Empid equals ap.Empid
                             join p in patient on ap.Patientid equals p.Patientid
                             select new JoinTable { patient = p, employee = e, appointment = ap };
            return View(multitable);
        }
        public IActionResult Check()
        {
  
            return View();
        }
        [HttpGet]
        public IActionResult Search()
        {
            //  var modelcontext = _context.Appointments.Where(x=>x.Status=="0"); 

            ViewBag.EmployeeID = HttpContext.Session.GetInt32("EmployeeID");
            var id = ViewBag.EmployeeID;
            var employee = _context.Employees.Where(x => x.Roleid == 23).ToList();
            var appo = _context.Appointments.ToList().Where(x => x.Empid == id).Where(x => x.Status == "0");
            var patient = _context.Patients.ToList();

            var multitable = from e in employee
                             join ap in appo on e.Empid equals ap.Empid
                             join p in patient on ap.Patientid equals p.Patientid
                             select new JoinTable { patient = p, employee = e, appointment = ap };

            return View(multitable);
        }
        [HttpPost]
        public async Task<IActionResult> Search(DateTime? startDate, DateTime? endDate)
        {



            ViewBag.EmployeeID = HttpContext.Session.GetInt32("EmployeeID");
            var id = ViewBag.EmployeeID;
            var employee = _context.Employees.Where(x => x.Roleid == 23).ToList();
            var appo = _context.Appointments.ToList().Where(x => x.Empid == id).Where(x => x.Status == "0");
            var patient = _context.Patients.ToList();

            var multitable = from e in employee
                             join ap in appo on e.Empid equals ap.Empid
                             join p in patient on ap.Patientid equals p.Patientid
                             select new JoinTable { patient = p, employee = e, appointment = ap };
            if (startDate == null && endDate == null)
            {
                return View(multitable);
            }
            else if (startDate != null && endDate == null)
            {
                var result =  multitable.Where(x => x.appointment.Appointmenttime.Value.Date == startDate);
                return View(result);
            }
            else if (startDate == null && endDate != null)
            {
                var result =  multitable.Where(x => x.appointment.Appointmenttime.Value.Date == endDate);
                return View(result);
            }
            else
            {
                var result = multitable.Where(x => x.appointment.Appointmenttime.Value.Date >= startDate && x.appointment.Appointmenttime.Value.Date <= endDate);
                return View(result);
            }

        }

        [HttpGet]
        public IActionResult Searrch()
        {
            //  var modelcontext = _context.Appointments.Where(x=>x.Status=="0"); 

            ViewBag.EmployeeID = HttpContext.Session.GetInt32("EmployeeID");
            var id = ViewBag.EmployeeID;
            var employee = _context.Employees.Where(x => x.Roleid == 23).ToList();
            var appo = _context.Appointments.ToList().Where(x => x.Empid == id).Where(x => x.Status != "0");
            var patient = _context.Patients.ToList();

            var multitable = from e in employee
                             join ap in appo on e.Empid equals ap.Empid
                             join p in patient on ap.Patientid equals p.Patientid
                             select new JoinTable { patient = p, employee = e, appointment = ap };

            return View(multitable);
        }
        [HttpPost]
        public async Task<IActionResult> Searrch(DateTime? startDate, DateTime? endDate)
        {



            ViewBag.EmployeeID = HttpContext.Session.GetInt32("EmployeeID");
            var id = ViewBag.EmployeeID;
            var employee = _context.Employees.Where(x => x.Roleid == 23).ToList();
            var appo = _context.Appointments.ToList().Where(x => x.Empid == id).Where(x => x.Status != "0");
            var patient = _context.Patients.ToList();

            var multitable = from e in employee
                             join ap in appo on e.Empid equals ap.Empid
                             join p in patient on ap.Patientid equals p.Patientid
                             select new JoinTable { patient = p, employee = e, appointment = ap };
            if (startDate == null && endDate == null)
            {
                return View(multitable);
            }
            else if (startDate != null && endDate == null)
            {
                var result = multitable.Where(x => x.appointment.Appointmenttime.Value.Date == startDate);
                return View(result);
            }
            else if (startDate == null && endDate != null)
            {
                var result = multitable.Where(x => x.appointment.Appointmenttime.Value.Date == endDate);
                return View(result);
            }
            else
            {
                var result = multitable.Where(x => x.appointment.Appointmenttime.Value.Date >= startDate && x.appointment.Appointmenttime.Value.Date <= endDate);
                return View(result);
            }

        }

        public IActionResult AppointmentData()
        {
    
            var employee = _context.Employees.Where(x => x.Roleid == 23).ToList();
            var appo = _context.Appointments.ToList();
            var patient = _context.Patients.ToList();

            var multitable = from e in employee
                             join ap in appo on e.Empid equals ap.Empid
                             join p in patient on ap.Patientid equals p.Patientid
                             select new JoinTable { patient = p, employee = e, appointment = ap };
            return View(multitable);
        }
        public IActionResult JoinTable()
        {
            var employee = _context.Employees.Where(x => x.Roleid == 23).ToList();
            var appo = _context.Appointments.ToList();
            var patient = _context.Patients.ToList();

            var multitable = from e in employee
                             join ap in appo on e.Empid equals ap.Empid
                             join p in patient on ap.Patientid equals p.Patientid
                             select new JoinTable { patient = p, employee = e, appointment = ap};
            return View(multitable);
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
