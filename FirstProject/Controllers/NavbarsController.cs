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
    public class NavbarsController : Controller
    {
        private readonly ModelContext _context;

        public NavbarsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Navbars
        public async Task<IActionResult> Index()
        {
            return View(await _context.Navbars.ToListAsync());
        }

        // GET: Navbars/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var navbar = await _context.Navbars
                .FirstOrDefaultAsync(m => m.Navbarid == id);
            if (navbar == null)
            {
                return NotFound();
            }

            return View(navbar);
        }

        // GET: Navbars/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Navbars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Navbarid,Entityname")] Navbar navbar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(navbar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(navbar);
        }

        // GET: Navbars/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var navbar = await _context.Navbars.FindAsync(id);
            if (navbar == null)
            {
                return NotFound();
            }
            return View(navbar);
        }

        // POST: Navbars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Navbarid,Entityname")] Navbar navbar)
        {
            if (id != navbar.Navbarid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(navbar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NavbarExists(navbar.Navbarid))
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
            return View(navbar);
        }

        // GET: Navbars/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var navbar = await _context.Navbars
                .FirstOrDefaultAsync(m => m.Navbarid == id);
            if (navbar == null)
            {
                return NotFound();
            }

            return View(navbar);
        }

        // POST: Navbars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var navbar = await _context.Navbars.FindAsync(id);
            _context.Navbars.Remove(navbar);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NavbarExists(decimal id)
        {
            return _context.Navbars.Any(e => e.Navbarid == id);
        }
    }
}
