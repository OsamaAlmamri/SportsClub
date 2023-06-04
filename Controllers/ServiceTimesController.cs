using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportsClub.Models;

namespace SportsClub.Controllers
{
    [Route("mvc/[controller]")]
    public class ServiceTimesController : Controller
    {
        private readonly SportsClubContext _context;

        public ServiceTimesController(SportsClubContext context)
        {
            _context = context;
        }

        // GET: ServiceTimes
        public async Task<IActionResult> Index()
        {
              return _context.ServiceTimes != null ? 
                          View(await _context.ServiceTimes.ToListAsync()) :
                          Problem("Entity set 'SportsClubContext.ServiceTimes'  is null.");
        }

        // GET: ServiceTimes/Details/5
        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.ServiceTimes == null)
            {
                return NotFound();
            }

            var serviceTime = await _context.ServiceTimes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (serviceTime == null)
            {
                return NotFound();
            }

            return View(serviceTime);
        }

        // GET: ServiceTimes/Create
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: ServiceTimes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,FromTime,ToTime")] ServiceTime serviceTime)
        {
            if (ModelState.IsValid)
            {
                _context.Add(serviceTime);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(serviceTime);
        }

        // GET: ServiceTimes/Edit/5
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.ServiceTimes == null)
            {
                return NotFound();
            }

            var serviceTime = await _context.ServiceTimes.FindAsync(id);
            if (serviceTime == null)
            {
                return NotFound();
            }
            return View(serviceTime);
        }

        // POST: ServiceTimes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,FromTime,ToTime")] ServiceTime serviceTime)
        {
            if (id != serviceTime.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(serviceTime);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceTimeExists(serviceTime.Id))
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
            return View(serviceTime);
        }

        // GET: ServiceTimes/Delete/5
        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.ServiceTimes == null)
            {
                return NotFound();
            }

            var serviceTime = await _context.ServiceTimes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (serviceTime == null)
            {
                return NotFound();
            }

            return View(serviceTime);
        }

        // POST: ServiceTimes/Delete/5
        [HttpPost("Delete/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.ServiceTimes == null)
            {
                return Problem("Entity set 'SportsClubContext.ServiceTimes'  is null.");
            }
            var serviceTime = await _context.ServiceTimes.FindAsync(id);
            var child = _context.Services.Where(s => s.ServiceTimeId == id).FirstOrDefault();
            if (serviceTime != null && child==null)
            {
                _context.ServiceTimes.Remove(serviceTime);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceTimeExists(long id)
        {
          return (_context.ServiceTimes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
