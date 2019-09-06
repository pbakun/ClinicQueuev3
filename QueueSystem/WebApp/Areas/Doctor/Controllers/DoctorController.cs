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
    [Authorize]
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
            if (queue == null)
            {
                queue = new Queue
                {
                    UserId = user.Id,
                    RoomNo = user.RoomNo,
                    Timestamp = DateTime.UtcNow
                };
                await _db.Queue.AddAsync(queue);

            }
            queue.OwnerInitials = String.Concat(user.FirstName.First(), user.LastName.First());
            await _db.SaveChangesAsync();

            return View(queue);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Next()
        {
            
            if (ModelState.IsValid)
            {
                var claimsIdentity = (ClaimsIdentity)this.User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

                var queue = _db.Queue.Where(u => u.UserId == claim.Value).FirstOrDefault();
                queue.QueueNo++;
                _db.Queue.Update(queue);
                await _db.SaveChangesAsync();

                return View("Index", queue);
            }
            //TODO
            return View();
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewRoomNo(Queue queue)
        {

            return View("Index", queue);
        }
    }
}