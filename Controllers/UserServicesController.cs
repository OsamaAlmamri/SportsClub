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
    public class UserServicesController : Controller
    {
        private readonly SportsClubContext _context;

        public UserServicesController(SportsClubContext context)
        {
            _context = context;
        }

        // GET: UserServices
        public async Task<IActionResult> Index()
        {
            var sportsClubContext = _context.UserSubscriptionServices
                 .Include(u => u.Service)
                .Include(u => u.Service.ServiceTime)
                .Include(u => u.Service.ServiceType)
                .Include(u => u.User)
                .Include(u => u.User.UserDetail)
                .Include(u => u.UserSubscription);
            return View(await sportsClubContext.ToListAsync());
        }
        [HttpGet("Details/{id}")]
        // GET: UserServices/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.UserSubscriptionServices == null)
            {
                return NotFound();
            }

            var userSubscriptionService = await _context.UserSubscriptionServices
                .Include(u => u.Service)
                .Include(u => u.Service.ServiceTime)
                .Include(u => u.Service.ServiceType)
                .Include(u => u.User)
                .Include(u => u.User.UserDetail)
                .Include(u => u.UserSubscription)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userSubscriptionService == null)
            {
                return NotFound();
            }

            return View(userSubscriptionService);
        }

        // GET: UserServices/Create
        [HttpGet("Create")]
        public IActionResult Create()
        {
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["UserSubscriptionId"] = new SelectList(_context.UserSubscriptions, "Id", "Id");
            return View();
        }

        // POST: UserServices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ServiceId,UserSubscriptionId,UserId,StartAt,EndAt")] UserSubscriptionService userSubscriptionService)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userSubscriptionService);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Id", userSubscriptionService.ServiceId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userSubscriptionService.UserId);
            ViewData["UserSubscriptionId"] = new SelectList(_context.UserSubscriptions, "Id", "Id", userSubscriptionService.UserSubscriptionId);
            return View(userSubscriptionService);
        }

        // GET: UserServices/Edit/5
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.UserSubscriptionServices == null)
            {
                return NotFound();
            }

            var userSubscriptionService = await _context.UserSubscriptionServices.FindAsync(id);
            if (userSubscriptionService == null)
            {
                return NotFound();
            }
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Id", userSubscriptionService.ServiceId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userSubscriptionService.UserId);
            ViewData["UserSubscriptionId"] = new SelectList(_context.UserSubscriptions, "Id", "Id", userSubscriptionService.UserSubscriptionId);
            return View(userSubscriptionService);
        }

        // POST: UserServices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,ServiceId,UserSubscriptionId,UserId,StartAt,EndAt")] UserSubscriptionService userSubscriptionService)
        {
            if (id != userSubscriptionService.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userSubscriptionService);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserSubscriptionServiceExists(userSubscriptionService.Id))
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
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Id", userSubscriptionService.ServiceId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userSubscriptionService.UserId);
            ViewData["UserSubscriptionId"] = new SelectList(_context.UserSubscriptions, "Id", "Id", userSubscriptionService.UserSubscriptionId);
            return View(userSubscriptionService);
        }

        // GET: UserServices/Delete/5
  /*      [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.UserSubscriptionServices == null)
            {
                return NotFound();
            }

            var userSubscriptionService = await _context.UserSubscriptionServices
                .Include(u => u.Service)
                .Include(u => u.User)
                .Include(u => u.UserSubscription)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userSubscriptionService == null)
            {
                return NotFound();
            }

            return View(userSubscriptionService);
        }

        // POST: UserServices/Delete/5
        [HttpPost("Delete/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.UserSubscriptionServices == null)
            {
                return Problem("Entity set 'SportsClubContext.UserSubscriptionServices'  is null.");
            }
            var userSubscriptionService = await _context.UserSubscriptionServices.FindAsync(id);
            if (userSubscriptionService != null)
            {
                _context.UserSubscriptionServices.Remove(userSubscriptionService);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
*/
        private bool UserSubscriptionServiceExists(long id)
        {
          return (_context.UserSubscriptionServices?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
