using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Areas.Doctor.Controllers
{
    [Area("Doctor")]
    public class DoctorController : Controller
    {

        private readonly ApplicationDbContext _queueDb;

        public DoctorController(ApplicationDbContext queue)
        {
            _queueDb = queue;
        }

        public IActionResult Index()
        {
            var queue = new Queue();
            queue.QueueNo = 15;
            queue.OwnerInitials = "AB";

            return View(queue);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Next()
        {
            
            if (ModelState.IsValid)
            {
                var bla = _queueDb.Queue.FirstOrDefault();
                bla.QueueNo = 3;
                bla.OwnerInitials = "PB";
                _queueDb.Queue.Update(bla);
                await _queueDb.SaveChangesAsync();

                return View("Index", bla);
            }
            //TODO
            return View();
            
        }
    }
}