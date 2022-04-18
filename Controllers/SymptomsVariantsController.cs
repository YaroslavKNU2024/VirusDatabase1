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
    public class SymptomsVariantsController : Controller
    {
        private readonly VirusBaseContext _context;

        public SymptomsVariantsController(VirusBaseContext context) {
            _context = context;
        }

        // Symptom = symptom
        // GET: SymptomsVariants
        public async Task<IActionResult> Index(int? id, string name)
        {
            if (id == null)
                return RedirectToAction("Index", "VirusGroups");
            ViewBag.VariantId = id;
            ViewBag.VariantName = name;
            var symptomByVariant = _context.SymptomsVariants.Where(s => s.VariantId == id).Include(p => p.Variant).Include(p => p.Symptom);
            return View(await symptomByVariant.ToListAsync());
        }

        // GET: SymptomsVariants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var symptomsVariant = await _context.SymptomsVariants
                .Include(s => s.Symptom)
                .Include(s => s.Variant)
                .FirstOrDefaultAsync(m => m.VariantId == id);
            if (symptomsVariant == null)
                return NotFound();

            return View(symptomsVariant);
        }

        // GET: SymptomsVariants/Create
        public IActionResult Create(int? variantId)
        {
            ViewData["SymptomId"] = new SelectList(_context.Symptoms, "Id", "SymptomName");
            ViewBag.VariantId = variantId;
            ViewBag.VariantName = _context.Variants.Where(v => v.Id == variantId).FirstOrDefault().VariantName;
            return View();
        }

        // POST: SymptomsVariants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int variantId, [Bind("VariantId,SymptomId,Severity")] SymptomsVariant symptomsVariant)
        {
            symptomsVariant.VariantId = variantId;
            symptomsVariant.Variant = await _context.Variants.FindAsync(symptomsVariant.VariantId);
            symptomsVariant.Variant.Virus = await _context.Viruses.FindAsync(symptomsVariant.Variant.VirusId);
            symptomsVariant.Variant.Virus.Group = await _context.VirusGroups.FindAsync(symptomsVariant.Variant.Virus.GroupId);
            symptomsVariant.Symptom = await _context.Symptoms.FindAsync(symptomsVariant.SymptomId);
            ModelState.ClearValidationState(nameof(symptomsVariant.Variant));
            ModelState.ClearValidationState(nameof(symptomsVariant.Variant.Virus));
            ModelState.ClearValidationState(nameof(symptomsVariant.Variant.Virus.Group));
            ModelState.ClearValidationState(nameof(symptomsVariant.Symptom));
            TryValidateModel(symptomsVariant.Variant, nameof(symptomsVariant.Variant));
            TryValidateModel(symptomsVariant.Variant.Virus, nameof(symptomsVariant.Variant.Virus));
            TryValidateModel(symptomsVariant.Variant.Virus.Group, nameof(symptomsVariant.Variant.Virus.Group));
            TryValidateModel(symptomsVariant.Symptom, nameof(symptomsVariant.Symptom));
            if (ModelState.IsValid)
            {
                _context.Add(symptomsVariant);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "SymptomsVariants", new { id = variantId, name = _context.Variants.Where(p => p.Id == variantId).FirstOrDefault().VariantName });
            }
            //ViewData["VariantId"] = new SelectList(_context.Variant, "Id", "VariantName", symptomsVariant.VariantId);
            //ViewData["SymptomId"] = new SelectList(_context.Symptoms, "Id", "Id", symptomsVariant.SymptomId);
            //return View(symptomsVariant);
            return RedirectToAction("Index", "SymptomsVariants", new { id = variantId, name = _context.Variants.Where(p => p.Id == variantId).FirstOrDefault().VariantName });

        }

        // GET: SymptomsVariants/Edit/5
        public async Task<IActionResult> Edit(int variantId, int symptomId)
        {

            var symptomsVariant = await _context.SymptomsVariants.FindAsync(variantId, symptomId);
            if (symptomsVariant == null)
            {
                return NotFound();
            }
            //ViewData["VariantId"] = new SelectList(_context.Variants, "Id", "VariantName", symptomsVariant.VariantId);
            //ViewData["SymptomId"] = new SelectList(_context.Symptoms, "Id", "Id", symptomsVariant.SymptomId);
            ViewBag.VariantId = symptomId;
            ViewBag.SymptomId = symptomId;
            ViewBag.VariantName = _context.Variants.Where(p => p.Id == variantId).FirstOrDefault().VariantName;
            return View(symptomsVariant);
        }

        // POST: SymptomsVariants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VariantId,SymptomId,Severity")] SymptomsVariant symptomsVariant)
        {
            if (id != symptomsVariant.VariantId)
                return NotFound();

            symptomsVariant.Variant = await _context.Variants.FindAsync(symptomsVariant.VariantId);
            symptomsVariant.Variant.Virus = await _context.Viruses.FindAsync(symptomsVariant.Variant.VirusId);
            symptomsVariant.Variant.Virus.Group = await _context.VirusGroups.FindAsync(symptomsVariant.Variant.Virus.GroupId);
            symptomsVariant.Symptom = await _context.Symptoms.FindAsync(symptomsVariant.SymptomId);
            ModelState.ClearValidationState(nameof(symptomsVariant.Variant));
            ModelState.ClearValidationState(nameof(symptomsVariant.Variant.Virus));
            ModelState.ClearValidationState(nameof(symptomsVariant.Variant.Virus.Group));
            ModelState.ClearValidationState(nameof(symptomsVariant.Symptom));
            TryValidateModel(symptomsVariant.Variant, nameof(symptomsVariant.Variant));
            TryValidateModel(symptomsVariant.Variant.Virus, nameof(symptomsVariant.Variant.Virus));
            TryValidateModel(symptomsVariant.Variant.Virus.Group, nameof(symptomsVariant.Variant.Virus.Group));
            TryValidateModel(symptomsVariant.Symptom, nameof(symptomsVariant.Symptom));
            if (ModelState.IsValid) {
                try {
                    _context.Update(symptomsVariant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) {
                    if (!SymptomsVariantExists(symptomsVariant.VariantId))
                        return NotFound();
                    else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["VariantId"] = new SelectList(_context.Variants, "Id", "VariantName", symptomsVariant.VariantId);
            ViewData["SymptomId"] = new SelectList(_context.Symptoms, "Id", "Id", symptomsVariant.SymptomId);
            return View(symptomsVariant);


        }

        // GET: SymptomsVariants/Delete/5
        public async Task<IActionResult> Delete(int variantId, int symptomId)
        {

            var symptomsVariant = await _context.SymptomsVariants
                .Include(s => s.Symptom)
                .Include(s => s.Variant)
                .FirstOrDefaultAsync(m => m.VariantId == variantId && m.SymptomId == symptomId);
            if (symptomsVariant == null)
                return NotFound();

            return View(symptomsVariant);
        }

        // POST: SymptomsVariants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var symptomsVariant = await _context.SymptomsVariants.FindAsync(id);
            _context.SymptomsVariants.Remove(symptomsVariant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SymptomsVariantExists(int id)
        {
            return _context.SymptomsVariants.Any(e => e.VariantId == id);
        }
    }
}
