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
    public class ClinicsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ClinicsController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        // GET: Clinics
        public async Task<IActionResult> Index()
        {
            return View(await _context.Clinics.ToListAsync());
        }

        // GET: Clinics/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clinic = await _context.Clinics
                .FirstOrDefaultAsync(m => m.Clinicid == id);
            if (clinic == null)
            {
                return NotFound();
            }
            ViewBag.EmployeeID = HttpContext.Session.GetInt32("EmployeeID");
            return View(clinic);
        }

        // GET: Clinics/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clinics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Clinicid,ClinicImg,ClinicImgFile,Clincname,Phonenumber")] Clinic clinic)
        {
            if (ModelState.IsValid)
            {
                if (clinic.ClinicImgFile != null)
                {
                    string w3path = webHostEnvironment.WebRootPath;
                    string img = Guid.NewGuid() + "_" + clinic.ClinicImgFile.FileName;
                    string path = Path.Combine(w3path + "/images/", img);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await clinic.ClinicImgFile.CopyToAsync(fileStream);
                    }
                    clinic.ClinicImg = img;
                }
                _context.Add(clinic);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clinic);
        }

        // GET: Clinics/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clinic = await _context.Clinics.FindAsync(id);
            if (clinic == null)
            {
                return NotFound();
            }
            return View(clinic);
        }

        // POST: Clinics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Clinicid,ClinicImg,ClinicImgFile,Clincname,Phonenumber")] Clinic clinic)
        {
            if (id != clinic.Clinicid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (clinic.ClinicImgFile != null)
                    {
                        string w3path = webHostEnvironment.WebRootPath;
                        string img = Guid.NewGuid() + "_" + clinic.ClinicImgFile.FileName;
                        string path = Path.Combine(w3path + "/images/", img);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await clinic.ClinicImgFile.CopyToAsync(fileStream);
                        }
                        clinic.ClinicImg = img;
                    }
                    _context.Update(clinic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClinicExists(clinic.Clinicid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(clinic);
        }

        // GET: Clinics/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clinic = await _context.Clinics
                .FirstOrDefaultAsync(m => m.Clinicid == id);
            if (clinic == null)
            {
                return NotFound();
            }

            return View(clinic);
        }

        // POST: Clinics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var clinic = await _context.Clinics.FindAsync(id);
            _context.Clinics.Remove(clinic);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClinicExists(decimal id)
        {
            return _context.Clinics.Any(e => e.Clinicid == id);
        }
    }
}
