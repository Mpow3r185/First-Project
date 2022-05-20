using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FirstProject.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace FirstProject.Controllers
{
    public class PatientsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public PatientsController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        // GET: Patients
        public async Task<IActionResult> Index()
        {
            ViewBag.Name = HttpContext.Session.GetString("Name");
            ViewBag.PatientID = HttpContext.Session.GetInt32("PatientID");
            ViewBag.HomePage = _context.Homes.ToList();
            return View(await _context.Patients.ToListAsync());
        }
        //public async Task<IActionResult> Index2()
        //{
        //    return View(this);
        //}

        // GET: Patients/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients.FirstOrDefaultAsync(m => m.Patientid == id);
            if (patient == null)
            {
                return NotFound();
            }
            ViewBag.Name = HttpContext.Session.GetString("Name");
            ViewBag.PatientID = HttpContext.Session.GetInt32("PatientID");
            ViewBag.HomePage = _context.Homes.ToList();
            ViewBag.Employees = _context.Employees.ToList();
            ViewBag.ContactUs = _context.Contactus.ToList();
            ViewBag.AboutUs = _context.Aboutus.ToList();
            ViewBag.Services = _context.Services.ToList();
            ViewBag.Subscription = _context.Subscriptions.ToList();
            ViewBag.Testy = _context.Testimonials.ToList();
            ViewBag.NavBar = _context.Navbars.ToList();
            ViewBag.Footer = _context.Footers.ToList();
            ViewBag.Patient = _context.Patients.Where(x=>x.Patientid == id).ToList(); 
            ViewBag.EmployeeID = HttpContext.Session.GetInt32("EmployeeID");

            var id2 = ViewBag.EmployeeID;
            var employee = _context.Employees.Where(x => x.Roleid == 23).ToList();
            var appo = _context.Appointments.ToList().Where(x => x.Patientid == id).Where(x => x.Status != "0");
            var patients = _context.Patients.ToList().Where(x=>x.Patientid == id);

            var multitable = from e in employee
                             join ap in appo on e.Empid equals ap.Empid
                             join p in patients on ap.Patientid equals p.Patientid
                             select new JoinTable { patient = p, employee = e, appointment = ap };

            ViewBag.Join = multitable;
            return View(patient);
        }

        // GET: Patients/Create
        public IActionResult Create()
        {
            return View();
        }


        public IActionResult Pay(string name, string cardnum, string month, string year, string cvv  )
        {
            ViewBag.PatientID = HttpContext.Session.GetInt32("PatientID");
            ViewBag.Name = HttpContext.Session.GetString("Name");
            ViewBag.HomePage = _context.Homes.ToList();
            Payment pay = new Payment();
            //var auth = (pay.Paymentnumber == cardnum && pay.Placeholder == name && pay.Cvv == cvv);
            if (pay.Paymentnumber == cardnum && pay.Placeholder == name && pay.Cvv == cvv)
            {
                pay.Money = pay.Money - 20;
                
            }
             _context.SaveChangesAsync();
            return View();
        }
        public IActionResult Payment()
        {
            ViewBag.PatientID = HttpContext.Session.GetInt32("PatientID");
            ViewBag.Name = HttpContext.Session.GetString("Name");
            ViewBag.Appo = _context.Appointments.ToList();
            ViewBag.HomePage = _context.Homes.ToList();
            ViewBag.Bill = _context.Bills.ToList().Where(p => p.Patientid == ViewBag.PatientID);
            ViewBag.Clinic = _context.Clinics.ToList();
            ViewBag.Appo = _context.Appointments.ToList();

            var employee = _context.Employees.Where(x => x.Roleid == 23).ToList();
            var appo = _context.Appointments.ToList().Where(p => p.Patientid == ViewBag.PatientID);
            var patient = _context.Patients.ToList().Where(p=>p.Patientid == ViewBag.PatientID);
            var bill = _context.Bills.ToList().Where(p => p.Patientid == ViewBag.PatientID);
            var clinic = _context.Clinics.ToList();

            var multitable = from b in bill
                             join ap in appo on b.Appointmentid equals ap.Appointmentid
                             join e in employee on ap.Empid equals e.Empid
                             join c in clinic on e.Clinicid equals c.Clinicid
                             join p in patient on ap.Patientid equals p.Patientid
                             select new JoinTable { employee = e, appointment = ap, bill = b, clinic = c };
            ViewBag.Clin = multitable;
            return View();
        }


        // POST: Patients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Patientid,Fname,Lname,Age,Gender,Phonenumber,Email,Imgpath,ImgFile,Disease")] Patient patient, string pass)
        {
            if (ModelState.IsValid)
            {
                if (patient.ImgFile != null)
                {
                    string w3path = webHostEnvironment.WebRootPath;
                    string img = Guid.NewGuid() + "_" + patient.ImgFile.FileName;
                    string path = Path.Combine(w3path + "/images/", img);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await patient.ImgFile.CopyToAsync(fileStream);
                    }
                    patient.Imgpath = img;
                    _context.Add(patient);
                    await _context.SaveChangesAsync();
                }
                Login userLogin = new Login();

                userLogin.Email = patient.Email;
                userLogin.Passwordd = pass;
                userLogin.Roleid = 24;
                _context.Add(userLogin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(patient);
        }

        // GET: Patients/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            ViewBag.PatientID = HttpContext.Session.GetInt32("PatientID");
            ViewBag.PatientEmail = HttpContext.Session.GetString("PatientEmail");
            //ViewBag.Name = HttpContext.Session.GetString("Name");
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
            return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Patientid,Fname,Lname,Age,Gender,Phonenumber,Email,Imgpath,ImgFile,Disease")] Patient patient)
        {
            if (id != patient.Patientid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (patient.ImgFile != null)
                    {
                        string w3path = webHostEnvironment.WebRootPath;
                        string img = Guid.NewGuid() + "_" + patient.ImgFile.FileName;
                        string path = Path.Combine(w3path + "/images/", img);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await patient.ImgFile.CopyToAsync(fileStream);
                        }
                        patient.Imgpath = img;
                    }
                    _context.Update(patient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientExists(patient.Patientid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                ViewBag.PatientID = HttpContext.Session.GetInt32("PatientID");
                return RedirectToAction("Details",new { id = ViewBag.PatientID });
            }

            return View(patient);
        }

        // GET: Patients/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients
                .FirstOrDefaultAsync(m => m.Patientid == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var patient = await _context.Patients.FindAsync(id);
            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatientExists(decimal id)
        {
            return _context.Patients.Any(e => e.Patientid == id);
        }
    }
}
