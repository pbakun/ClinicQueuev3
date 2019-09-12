using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using WebApp.Models;

namespace WebApp.Areas.Patient.Controllers
{
    [Area("Patient")]
    public class HomeController : Controller
    {

        private readonly IRepositoryWrapper _repo;
        private readonly IHostingEnvironment _hostingEnvironment;

        public HomeController(IRepositoryWrapper repo, IHostingEnvironment hostingEnvironment)
        {
            _repo = repo;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            var bla = _repo.User.FindAll();
            var queue = _repo.Queue.FindAll();

            string filepath = _hostingEnvironment.ContentRootPath;

            return View();
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
