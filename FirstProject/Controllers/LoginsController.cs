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
    public class LoginsController : Controller
    {
        private readonly ModelContext _context;

        public LoginsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Logins
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Logins.Include(l => l.Patient).Include(l => l.Role);
            return View(await modelContext.ToListAsync());
        }

        // GET: Logins/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var login = await _context.Logins
                .Include(l => l.Patient)
                .Include(l => l.Role)
                .FirstOrDefaultAsync(m => m.Loginid == id);
            if (login == null)
            {
                return NotFound();
            }

            return View(login);
        }

        // GET: Logins/Create
        public IActionResult Create()
        {
            ViewData["Patientid"] = new SelectList(_context.Patients, "Patientid", "Patientid");
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Roleid");
            return View();
        }

        // POST: Logins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Loginid,Email,Passwordd,Roleid,Patientid")] Login login)
        {
            if (ModelState.IsValid)
            {
                _context.Add(login);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Patientid"] = new SelectList(_context.Patients, "Patientid", "Patientid", login.Patientid);
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Roleid", login.Roleid);
            return View(login);
        }

        // GET: Logins/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var login = await _context.Logins.FindAsync(id);
            if (login == null)
            {
                return NotFound();
            }
            ViewData["Patientid"] = new SelectList(_context.Patients, "Patientid", "Patientid", login.Patientid);
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Roleid", login.Roleid);
            return View(login);
        }

        // POST: Logins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Loginid,Email,Passwordd,Roleid,Patientid")] Login login)
        {
            if (id != login.Loginid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(login);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoginExists(login.Loginid))
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
            ViewData["Patientid"] = new SelectList(_context.Patients, "Patientid", "Patientid", login.Patientid);
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Roleid", login.Roleid);
            return View(login);
        }

        // GET: Logins/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var login = await _context.Logins
                .Include(l => l.Patient)
                .Include(l => l.Role)
                .FirstOrDefaultAsync(m => m.Loginid == id);
            if (login == null)
            {
                return NotFound();
            }

            return View(login);
        }

        // POST: Logins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var login = await _context.Logins.FindAsync(id);
            _context.Logins.Remove(login);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoginExists(decimal id)
        {
            return _context.Logins.Any(e => e.Loginid == id);
        }
    }
}
