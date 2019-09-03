using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
            var queue = _queueDb.Queue.Where(i => i.UserId == 1).FirstOrDefault();
            

            return View(queue);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Next()
        {
            
            if (ModelState.IsValid)
            {
                Queue bla = _queueDb.Queue.FirstOrDefault();
                bla.QueueNo = 3;
                bla.OwnerInitials = "PB";
                bla.RoomNo = 12;
                bla.UserId = 1;
                _queueDb.Queue.Update(bla);
                await _queueDb.SaveChangesAsync();

                return View("Index", bla);
            }
            //TODO
            return View();
            
        }
    }
}