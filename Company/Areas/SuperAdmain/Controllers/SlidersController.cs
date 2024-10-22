using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Company.Data;
using Company.Models;
using Microsoft.AspNetCore.Authorization;
using System.IO;

namespace Company.Areas.SuperAdmain.Controllers
{
    [Area("SuperAdmain")]
    [Authorize]
    public class SlidersController : Controller
    {
        private readonly CompanyDbContext _context;

        public SlidersController(CompanyDbContext context)
        {
            _context = context;
        }
        [Authorize("SuperAdmain")]
        // GET: SuperAdmain/Sliders
        public async Task<IActionResult> Index()
        {
            return View(await _context.Slider.ToListAsync());
        }
        [Authorize("SuperAdmain")]
        // GET: SuperAdmain/Sliders/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slider = await _context.Slider
                .FirstOrDefaultAsync(m => m.SliderId == id);
            if (slider == null)
            {
                return NotFound();
            }

            return View(slider);
        }
        [Authorize("SuperAdmain")]
        // GET: SuperAdmain/Sliders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SuperAdmain/Sliders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize("SuperAdmain")]
        public async Task<IActionResult> Create([Bind("SliderId,SliderTitle,SubTitle,SliderImg")] Slider slider, IFormFile Image)
        {
            if (Image != null && Image.Length > 0)
            {
                var memorystream = new MemoryStream();
                await Image.CopyToAsync(memorystream);
                memorystream.ToArray();
                slider.SliderImg= memorystream.ToArray();
                ModelState.Remove("SliderImg");

            }

            if (ModelState.IsValid)
            {
               
                slider.SliderId = Guid.NewGuid();
                _context.Add(slider);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(slider);
        }

        // GET: SuperAdmain/Sliders/Edit/5
        [Authorize("SuperAdmain")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slider = await _context.Slider.FindAsync(id);
            if (slider == null)
            {
                return NotFound();
            }
            return View(slider);
        }

        // POST: SuperAdmain/Sliders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize("SuperAdmain")]
        public async Task<IActionResult> Edit(Guid id, [Bind("SliderId,SliderTitle,SubTitle,SliderImg")] Slider slider,IFormFile Image)
        {
            if (id != slider.SliderId)
            {
                return NotFound();
            }
            if (Image != null && Image.Length > 0)
            {
                var memorystream = new MemoryStream();
                await Image.CopyToAsync(memorystream);
                memorystream.ToArray();
                slider.SliderImg = memorystream.ToArray();
                ModelState.Remove("SliderImg");

            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(slider);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SliderExists(slider.SliderId))
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
            return View(slider);
        }

        // GET: SuperAdmain/Sliders/Delete/5
        [Authorize("SuperAdmain")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slider = await _context.Slider
                .FirstOrDefaultAsync(m => m.SliderId == id);
            if (slider == null)
            {
                return NotFound();
            }

            return View(slider);
        }

        // POST: SuperAdmain/Sliders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize("SuperAdmain")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var slider = await _context.Slider.FindAsync(id);
            if (slider != null)
            {
                _context.Slider.Remove(slider);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SliderExists(Guid id)
        {
            return _context.Slider.Any(e => e.SliderId == id);
        }
    }
}
