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
    public class EntitiesController : Controller
    {
        private readonly ModelContext _context;

        public EntitiesController(ModelContext context)
        {
            _context = context;
        }

        // GET: Entities
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Entities.Include(e => e.Navbar);
            return View(await modelContext.ToListAsync());
        }

        // GET: Entities/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _context.Entities
                .Include(e => e.Navbar)
                .FirstOrDefaultAsync(m => m.Entityid == id);
            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);
        }

        // GET: Entities/Create
        public IActionResult Create()
        {
            ViewData["Navbarid"] = new SelectList(_context.Navbars, "Navbarid", "Navbarid");
            return View();
        }

        // POST: Entities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Entityid,Navbarid,Href")] Entity entity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(entity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Navbarid"] = new SelectList(_context.Navbars, "Navbarid", "Navbarid", entity.Navbarid);
            return View(entity);
        }

        // GET: Entities/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _context.Entities.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            ViewData["Navbarid"] = new SelectList(_context.Navbars, "Navbarid", "Navbarid", entity.Navbarid);
            return View(entity);
        }

        // POST: Entities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Entityid,Navbarid,Href")] Entity entity)
        {
            if (id != entity.Entityid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(entity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EntityExists(entity.Entityid))
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
            ViewData["Navbarid"] = new SelectList(_context.Navbars, "Navbarid", "Navbarid", entity.Navbarid);
            return View(entity);
        }

        // GET: Entities/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _context.Entities
                .Include(e => e.Navbar)
                .FirstOrDefaultAsync(m => m.Entityid == id);
            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);
        }

        // POST: Entities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var entity = await _context.Entities.FindAsync(id);
            _context.Entities.Remove(entity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EntityExists(decimal id)
        {
            return _context.Entities.Any(e => e.Entityid == id);
        }
    }
}
