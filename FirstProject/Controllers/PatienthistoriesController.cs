using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FirstProject.Models;

namespace FirstProject.Controllers
{
    public class PatienthistoriesController : Controller
    {
        private readonly ModelContext _context;

        public PatienthistoriesController(ModelContext context)
        {
            _context = context;
        }

        // GET: Patienthistories
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Patienthistories.Include(p => p.Patient);
            return View(await modelContext.ToListAsync());
        }

        // GET: Patienthistories/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patienthistory = await _context.Patienthistories
                .Include(p => p.Patient)
                .FirstOrDefaultAsync(m => m.Patienthistoryid == id);
            if (patienthistory == null)
            {
                return NotFound();
            }

            return View(patienthistory);
        }

        // GET: Patienthistories/Create
        public IActionResult Create()
        {
            ViewData["Patientid"] = new SelectList(_context.Patients, "Patientid", "Patientid");
            return View();
        }

        // POST: Patienthistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Patienthistoryid,ChronicDiseases,MedicinesOnRegularBasis,PreviousVisitsToTheClinicForTheSameReason,MainComplaint,SensitivityToAnything,PreviousExaminations,Patientid")] Patienthistory patienthistory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(patienthistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Patientid"] = new SelectList(_context.Patients, "Patientid", "Patientid", patienthistory.Patientid);
            return View(patienthistory);
        }

        // GET: Patienthistories/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patienthistory = await _context.Patienthistories.FindAsync(id);
            if (patienthistory == null)
            {
                return NotFound();
            }
            ViewData["Patientid"] = new SelectList(_context.Patients, "Patientid", "Patientid", patienthistory.Patientid);
            return View(patienthistory);
        }

        // POST: Patienthistories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Patienthistoryid,ChronicDiseases,MedicinesOnRegularBasis,PreviousVisitsToTheClinicForTheSameReason,MainComplaint,SensitivityToAnything,PreviousExaminations,Patientid")] Patienthistory patienthistory)
        {
            if (id != patienthistory.Patienthistoryid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patienthistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatienthistoryExists(patienthistory.Patienthistoryid))
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
            ViewData["Patientid"] = new SelectList(_context.Patients, "Patientid", "Patientid", patienthistory.Patientid);
            return View(patienthistory);
        }

        // GET: Patienthistories/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patienthistory = await _context.Patienthistories
                .Include(p => p.Patient)
                .FirstOrDefaultAsync(m => m.Patienthistoryid == id);
            if (patienthistory == null)
            {
                return NotFound();
            }

            return View(patienthistory);
        }

        // POST: Patienthistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var patienthistory = await _context.Patienthistories.FindAsync(id);
            _context.Patienthistories.Remove(patienthistory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatienthistoryExists(decimal id)
        {
            return _context.Patienthistories.Any(e => e.Patienthistoryid == id);
        }
    }
}
