using Microsoft.AspNetCore.Mvc;
using SportsClub.Models;
using SportsClub.Models.Repositores;
using System.Diagnostics;

namespace SportsClub.Controllers
{
    [Route("mvc/[controller]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SportsClubContext context;
        private readonly IRepositoryBase<Service> _repostry;
        public HomeController(ILogger<HomeController> logger, SportsClubContext context)
        {
            _logger = logger;
            this.context = context;
            this._repostry = new ServiceRepostry(context);
        }

        [HttpGet("index")]
        public IActionResult Index()
        {
            var services= context.Services 
            
            .ToList();
            return View(services);
            return View();
        }

        [HttpGet("privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        private List<Service> GetServices()
        {
            // Implementation to retrieve services from the database
            // Replace with your actual data retrieval logic
            return new List<Service>();
        }

    }
}