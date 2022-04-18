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
    public class VirusGroupsController : Controller
    {
        private readonly VirusBaseContext _context;

        public VirusGroupsController(VirusBaseContext context)
        {
            _context = context;
        }

        // GET: VirusGroups
        public async Task<IActionResult> Index()
        {
            return View(await _context.VirusGroups.ToListAsync());
        }

        // GET: VirusGroups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var virusGroup = await _context.VirusGroups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (virusGroup == null)
            {
                return NotFound();
            }

            //return View(virusGroup);
            return RedirectToAction("Index", "Viruses", new { id = virusGroup.Id, name = virusGroup.GroupName });

        }

        // GET: VirusGroups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VirusGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GroupName,GroupInfo,GroupDateDiscovered")] VirusGroup virusGroup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(virusGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(virusGroup);
        }

        // GET: VirusGroups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var virusGroup = await _context.VirusGroups.FindAsync(id);
            if (virusGroup == null)
            {
                return NotFound();
            }
            return View(virusGroup);
        }

        // POST: VirusGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GroupName,GroupInfo,GroupDateDiscovered")] VirusGroup virusGroup)
        {
            if (id != virusGroup.Id)
                return NotFound();

            if (ModelState.IsValid) {
                try {
                    _context.Update(virusGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) {
                    if (!VirusGroupExists(virusGroup.Id))
                        return NotFound();
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(virusGroup);
        }

        // GET: VirusGroups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var virusGroup = await _context.VirusGroups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (virusGroup == null)
                return NotFound();

            return View(virusGroup);
        }

        // POST: VirusGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var virusGroup = await _context.VirusGroups.FindAsync(id);
            _context.VirusGroups.Remove(virusGroup);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VirusGroupExists(int id)
        {
            return _context.VirusGroups.Any(e => e.Id == id);
        }
    }
}
