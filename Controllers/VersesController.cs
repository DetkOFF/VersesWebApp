using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VersesWebApp.Data;
using VersesWebApp.Models;

namespace VersesWebApp.Controllers
{
    public class VersesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VersesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Verses
        public async Task<IActionResult> Index()
        {
            return View(await _context.Verse.ToListAsync());
        }

        // GET: Verses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var verse = await _context.Verse
                .FirstOrDefaultAsync(m => m.Id == id);
            if (verse == null)
            {
                return NotFound();
            }

            return View(verse);
        }

        // GET: Verses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Verses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,VerseText,Author")] Verse verse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(verse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(verse);
        }

        // GET: Verses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var verse = await _context.Verse.FindAsync(id);
            if (verse == null)
            {
                return NotFound();
            }
            return View(verse);
        }

        // POST: Verses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VerseText,Author")] Verse verse)
        {
            if (id != verse.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(verse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VerseExists(verse.Id))
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
            return View(verse);
        }

        // GET: Verses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var verse = await _context.Verse
                .FirstOrDefaultAsync(m => m.Id == id);
            if (verse == null)
            {
                return NotFound();
            }

            return View(verse);
        }

        // POST: Verses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var verse = await _context.Verse.FindAsync(id);
            if (verse != null)
            {
                _context.Verse.Remove(verse);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VerseExists(int id)
        {
            return _context.Verse.Any(e => e.Id == id);
        }
    }
}
