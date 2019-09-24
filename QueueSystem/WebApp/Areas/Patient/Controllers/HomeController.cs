using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repository.Interfaces;
using Serilog;
using WebApp.BackgroundServices.Tasks;
using WebApp.Models;

namespace WebApp.Areas.Patient.Controllers
{
    [Area("Patient")]
    public class HomeController : Controller
    {

        private readonly IRepositoryWrapper _repo;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IRepositoryWrapper repo, ILogger<HomeController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogTrace("Home Controller entered");
            var bla = _repo.User.FindAll();
            var queue = _repo.Queue.FindAll();

            var availableRooms = SettingsHandler.ApplicationSettings.AvailableRooms;

            return View(availableRooms);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
