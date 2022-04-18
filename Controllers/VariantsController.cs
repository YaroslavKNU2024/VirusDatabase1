#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VirusDatabaseNew;

namespace VirusDatabaseNew.Controllers
{
    public class VariantsController : Controller
    {
        private readonly VirusBaseContext _context;

        public VariantsController(VirusBaseContext context)
        {
            _context = context;
        }

        // GET: Variants
        public async Task<IActionResult> Index(int? id, string name)
        {
            if (id == null || name == null)
            {
                var variants = _context.Variants.Include(x => x.Virus);
                ViewBag.VirusName = null;
                return View(await variants.ToListAsync());
            }
            //Знаходження працівників за кафедрами
            ViewBag.VirusId = id;
            ViewBag.VirusName = name;
            var variantsByViruses = _context.Variants.Where(x => x.VirusId == id).Include(x => x.Virus);
            return View(await variantsByViruses.ToListAsync());
        }

        // GET: Variants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var variant = await _context.Variants
                .Include(v => v.Virus)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (variant == null)
                return NotFound();

            return View(variant);
        }


        // GET: Variants/Create
        public IActionResult Create(int? virusId)
        {
            if (virusId == null)
                ViewData["VirusId"] = new SelectList(_context.Viruses, "Id", "VirusName");
            else
            {
                ViewBag.VirusId = virusId;
                ViewBag.VirusName = _context.Viruses.Where(v => v.Id == virusId).FirstOrDefault().VirusName;
            }
            return View();
        }

        // POST: Variants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int virusId, [Bind("Id,VariantName,VariantOrigin,VariantDateDiscovered,VirusId")] Variant variant)
        {
            variant.VirusId = virusId;
            variant.Virus = await _context.Viruses.FindAsync(variant.VirusId);
            variant.Virus.Group = await _context.VirusGroups.FindAsync(variant.Virus.GroupId);
            ModelState.ClearValidationState(nameof(variant.Virus));
            ModelState.ClearValidationState(nameof(variant.Virus.Group));
            TryValidateModel(variant.Virus, nameof(variant.Virus));
            TryValidateModel(variant.Virus.Group, nameof(variant.Virus.Group));
            if (ModelState.IsValid)
            {
                _context.Add(variant);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "Variants", new { id = virusId, name = _context.Viruses.Where(c => c.Id == virusId).FirstOrDefault().VirusName });
            }
            //ViewData["VirusId"] = new SelectList(_context.Viruses, "Id", "VirusName", variant.VirusId);
            //return View(variant);
            return RedirectToAction("Index", "Variants", new { id = virusId, name = _context.Viruses.Where(c => c.Id == virusId).FirstOrDefault().VirusName });

        }

        // GET: Variants/Edit/5
        public async Task<IActionResult> Edit(int? id, int? virusId)
        {
            if (id == null)
                return NotFound();

            var variant = await _context.Variants.FindAsync(id);
            variant.Virus = await _context.Viruses.FindAsync(variant.VirusId);
            variant.Virus.Group = await _context.VirusGroups.FindAsync(variant.Virus.GroupId);
            ViewBag.VirusId = variant.VirusId;
            if (virusId != null)
            {
                ViewBag.VirusName = _context.Viruses.Where(v => v.Id == variant.VirusId).FirstOrDefault().VirusName;
                ViewBag.VirusId = virusId;
            }
            else
            {
                ViewBag.VirusName = null;
                ViewData["VirusId"] = new SelectList(_context.Viruses, "Id", "VirusName", variant.VirusId);
            }

            if (variant == null)
                return NotFound();
            return View(variant);
        }

        // POST: Variants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VariantName,VariantOrigin,VariantDateDiscovered,VirusId")] Variant variant)
        {
            if (id != variant.Id)
                return NotFound();
            variant.Virus = await _context.Viruses.FindAsync(variant.VirusId);
            variant.Virus.Group = await _context.VirusGroups.FindAsync(variant.Virus.GroupId);
            ModelState.ClearValidationState(nameof(variant.Virus));
            ModelState.ClearValidationState(nameof(variant.Virus.Group));
            TryValidateModel(variant.Virus, nameof(variant.Virus));
            TryValidateModel(variant.Virus.Group, nameof(variant.Virus.Group));
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(variant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VariantExists(variant.Id))
                        return NotFound();
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["VirusId"] = new SelectList(_context.Viruses, "Id", "VirusName", variant.VirusId);
            return View(variant);

        }

        // GET: Variants/Delete/5
        public async Task<IActionResult> Delete(int? id, int? virusId)
        {
            if (id == null)
                return NotFound();

            var variant = await _context.Variants
                .Include(v => v.Virus)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (virusId != null)
                ViewBag.VirusName = _context.Viruses.Where(v => v.Id == variant.VirusId).FirstOrDefault().VirusName;
            else
                ViewBag.VirusName = null;

            if (variant == null)
                return NotFound();

            return View(variant);
        }

        // POST: Variants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var variant = await _context.Variants.FindAsync(id);
            _context.Variants.Remove(variant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VariantExists(int id)
        {
            return _context.Variants.Any(e => e.Id == id);
        }
    }
}
