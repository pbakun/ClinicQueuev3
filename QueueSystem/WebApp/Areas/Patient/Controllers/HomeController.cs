using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using WebApp.BackgroundServices.Tasks;
using WebApp.Models;

namespace WebApp.Areas.Patient.Controllers
{
    [Area("Patient")]
    public class HomeController : Controller
    {

        private readonly IRepositoryWrapper _repo;

        public HomeController(IRepositoryWrapper repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
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
