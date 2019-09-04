using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        private readonly ApplicationDbContext _db;

        public DoctorController(ApplicationDbContext queue)
        {
            _db = queue;
        }

        public async Task<IActionResult> Index()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var user = _db.User.Where(u => u.Id == claim.Value).FirstOrDefault();

            var queue = _db.Queue.Where(i => i.UserId == user.Id).FirstOrDefault();
            if(queue == null)
            {
                queue = new Queue
                {
                    UserId = user.Id,
                    OwnerInitials = String.Concat(user.FirstName.First(), user.LastName.First()),
                    RoomNo = user.RoomNo,
                    Timestamp = DateTime.UtcNow
                };
                await _db.Queue.AddAsync(queue);
                await _db.SaveChangesAsync();
            }
            

            return View(queue);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Next()
        {
            
            if (ModelState.IsValid)
            {
                Queue bla = _db.Queue.FirstOrDefault();
                bla.QueueNo = 3;
                bla.OwnerInitials = "PB";
                bla.RoomNo = 12;
                bla.UserId = "1";
                _db.Queue.Update(bla);
                await _db.SaveChangesAsync();

                return View("Index", bla);
            }
            //TODO
            return View();
            
        }
    }
}