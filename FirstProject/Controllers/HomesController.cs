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

namespace FirstProject.Controllers
{
    public class HomesController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        public HomesController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        // GET: Homes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Homes.ToListAsync());
        }

        // GET: Homes/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var home = await _context.Homes
                .FirstOrDefaultAsync(m => m.Homeid == id);
            if (home == null)
            {
                return NotFound();
            }

            return View(home);
        }

        // GET: Homes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Homes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Homeid,Logo,LogoFile,Websitename,Imgslider,ImgFileSlider,Textimg,Openingday,Openinghour")] Home home)
        {
            if (ModelState.IsValid)
            {
                if (home.ImgFileSlider != null)
                {
                    string w3path = webHostEnvironment.WebRootPath;
                    string img = Guid.NewGuid() + "_" + home.ImgFileSlider.FileName;
                    string path = Path.Combine(w3path + "/images/", img);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await home.ImgFileSlider.CopyToAsync(fileStream);
                    }
                    home.Imgslider = img;
                }
                _context.Add(home);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

                if (home.LogoFile != null)
                {
                    string w3path = webHostEnvironment.WebRootPath;
                    string img = Guid.NewGuid() + "_" + home.LogoFile.FileName;
                    string path = Path.Combine(w3path + "/images/", img);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await home.LogoFile.CopyToAsync(fileStream);
                    }
                    home.Logo = img;
                }
                _context.Add(home);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(home);
        }

        // GET: Homes/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var home = await _context.Homes.FindAsync(id);
            if (home == null)
            {
                return NotFound();
            }
            return View(home);
        }

        // POST: Homes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Homeid,Logo,LogoFile,Websitename,Imgslider,ImgFileSlider,Textimg,Openingday,Openinghour")] Home home)
        {
            if (id != home.Homeid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (home.ImgFileSlider != null)
                    {
                        string w3path = webHostEnvironment.WebRootPath;
                        string img = Guid.NewGuid() + "_" + home.ImgFileSlider.FileName;
                        string path = Path.Combine(w3path + "/images/", img);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await home.ImgFileSlider.CopyToAsync(fileStream);
                        }
                        home.Imgslider = img;
                    }
                    _context.Update(home);
                    await _context.SaveChangesAsync();
                    if (home.LogoFile != null)
                    {
                        string w3path = webHostEnvironment.WebRootPath;
                        string img = Guid.NewGuid() + "_" + home.LogoFile.FileName;
                        string path = Path.Combine(w3path + "/images/", img);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await home.LogoFile.CopyToAsync(fileStream);
                        }
                        home.Logo = img;
                    }
                    _context.Add(home);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HomeExists(home.Homeid))
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
            return View(home);
        }

        // GET: Homes/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var home = await _context.Homes
                .FirstOrDefaultAsync(m => m.Homeid == id);
            if (home == null)
            {
                return NotFound();
            }

            return View(home);
        }

        // POST: Homes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var home = await _context.Homes.FindAsync(id);
            _context.Homes.Remove(home);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HomeExists(decimal id)
        {
            return _context.Homes.Any(e => e.Homeid == id);
        }
    }
}
