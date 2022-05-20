using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FirstProject.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace FirstProject.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public EmployeesController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            ViewBag.EmployeeID = HttpContext.Session.GetInt32("EmployeeID");
            ViewBag.Emp = _context.Employees.ToList();
            var modelContext = _context.Employees.Include(e => e.Clinic).Include(e => e.Role);
            return View(await modelContext.ToListAsync());
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Clinic)
                .Include(e => e.Role)
                .FirstOrDefaultAsync(m => m.Empid == id);
            if (employee == null)
            {
                return NotFound();
            }
            var auth = _context.Employees.Where(x => x.Empid == id).SingleOrDefault();
            HttpContext.Session.SetInt32("EmployeeIDD", (int)auth.Empid);
            ViewBag.EmployeeID = HttpContext.Session.GetInt32("EmployeeID");
            ViewBag.HomePage = _context.Homes.ToList();
            ViewBag.PatientID = HttpContext.Session.GetInt32("PatientID");
            ViewBag.Appointment = _context.Appointments.Where(x => x.Empid == id && x.Status!="0");
            ViewBag.Emp = _context.Employees.Where(x=>x.Empid==id);
            return View(employee);
        }
        public async Task<IActionResult> Doctor(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Clinic)
                .Include(e => e.Role)
                .FirstOrDefaultAsync(m => m.Empid == id);
            if (employee == null)
            {
                return NotFound();
            }
            var auth = _context.Employees.Where(x => x.Empid == id).SingleOrDefault();
            HttpContext.Session.SetInt32("EmployeeIDD", (int)auth.Empid);
            ViewBag.EmployeeID = HttpContext.Session.GetInt32("EmployeeID");
            ViewBag.Appointment = _context.Appointments.Where(x => x.Empid == id && x.Status != "0");
            ViewBag.Emp = _context.Employees.Where(x => x.Empid == id);
            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewData["Clinicid"] = new SelectList(_context.Clinics, "Clinicid", "Clinicid");
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Roleid");
            return View();
        }
     

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Empid,Fname,Lname,Specialist,Salary,Hiredate,Imgpath,ImgFile,Phonenumber,Email,Roleid,Clinicid,Abouthim")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                if (employee.ImgFile != null)
                {
                    string w3path = webHostEnvironment.WebRootPath;
                    string img = Guid.NewGuid() + "_" + employee.ImgFile.FileName;
                    string path = Path.Combine(w3path + "/images/", img);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await employee.ImgFile.CopyToAsync(fileStream);
                    }
                    employee.Imgpath = img;
                }
                Login login = new Login();
                login.Email = employee.Email;
                login.Passwordd = "123";
                login.Roleid = 23;
                _context.Add(employee);
                _context.Add(login);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Clinicid"] = new SelectList(_context.Clinics, "Clinicid", "Clinicid", employee.Clinicid);
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Roleid", employee.Roleid);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewBag.EmployeeID = HttpContext.Session.GetInt32("EmployeeID");
            ViewBag.Emp = _context.Employees.Where(x => x.Empid == id);
            ViewData["Clinicid"] = new SelectList(_context.Clinics, "Clinicid", "Clinicid", employee.Clinicid);
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Roleid", employee.Roleid);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Empid,Fname,Lname,Specialist,Salary,Hiredate,Imgpath,ImgFile,Phonenumber,Email,Roleid,Clinicid,Abouthim")] Employee employee)
        {
            if (id != employee.Empid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (employee.ImgFile != null)
                    {
                        string w3path = webHostEnvironment.WebRootPath;
                        string img = Guid.NewGuid() + "_" + employee.ImgFile.FileName;
                        string path = Path.Combine(w3path + "/images/", img);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await employee.ImgFile.CopyToAsync(fileStream);
                        }
                        employee.Imgpath = img;
                    }
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Empid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Admin","Home");
            }
            ViewData["Clinicid"] = new SelectList(_context.Clinics, "Clinicid", "Clinicid", employee.Clinicid);
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Roleid", employee.Roleid);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Clinic)
                .Include(e => e.Role)
                .FirstOrDefaultAsync(m => m.Empid == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var employee = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(decimal id)
        {
            return _context.Employees.Any(e => e.Empid == id);
        }
    }
}
