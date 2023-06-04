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
    public class ServicesController : Controller
    {
        private readonly SportsClubContext _context;

        public ServicesController(SportsClubContext context)
        {
            _context = context;
        }

        // GET: Services
        public async Task<IActionResult> Index()
        {
            var sportsClubContext = _context.Services.Include(s => s.ServiceTime).Include(s => s.ServiceType);
            return View(await sportsClubContext.ToListAsync());
        }

        // GET: Services/Details/5
        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Services == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .Include(s => s.ServiceTime)
                .Include(s => s.ServiceType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }
        [HttpGet("Create")]
        // GET: Services/Create
        public IActionResult Create()
        {
            var times = new SelectList(_context.ServiceTimes, "Id", "Name", selectedValue: null);
            times.ToList().Insert(0, new SelectListItem
            {
                Value = "",
                Text = "Select an Option Below"

            });
            ViewData["ServiceTimeId"]= times;
            ViewData["ServiceTypeId"] = new SelectList(_context.ServiceTypes, "Id", "Name");
            return View();
        }

        // POST: Services/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Period,ServiceTypeId,ServiceTimeId,Description,Price,CreatedAt")] Service service)
        {
            if (ModelState.IsValid)
            {
                _context.Add(service);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ServiceTimeId"] = new SelectList(_context.ServiceTimes, "Id", "Id", service.ServiceTimeId);
            ViewData["ServiceTypeId"] = new SelectList(_context.ServiceTypes, "Id", "Id", service.ServiceTypeId);
            return View(service);
        }

        // GET: Services/Edit/5
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Services == null)
            {
                return NotFound();
            }

            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }
            ViewData["ServiceTimeId"] = new SelectList(_context.ServiceTimes, "Id", "Name", service.ServiceTimeId);
            ViewData["ServiceTypeId"] = new SelectList(_context.ServiceTypes, "Id", "Name", service.ServiceTypeId);
            return View(service);
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,Period,ServiceTypeId,ServiceTimeId,Description,Price,CreatedAt")] Service service)
        {
            if (id != service.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(service);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceExists(service.Id))
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
            ViewData["ServiceTimeId"] = new SelectList(_context.ServiceTimes, "Id", "Name", service.ServiceTimeId);
            ViewData["ServiceTypeId"] = new SelectList(_context.ServiceTypes, "Id", "Name", service.ServiceTypeId);
            return View(service);
        }

        // GET: Services/Delete/5
        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Services == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .Include(s => s.ServiceTime)
                .Include(s => s.ServiceType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // POST: Services/Delete/5
        [HttpPost("Delete/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Services == null)
            {
                return Problem("Entity set 'SportsClubContext.Services'  is null.");
            }
            var service = await _context.Services.FindAsync(id);
            if (service != null)
            {
                _context.Services.Remove(service);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceExists(long id)
        {
          return (_context.Services?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
