using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using WebApp.Models;

namespace WebApp.Areas.Doctor.Controllers
{
    [Authorize]
    [Area("Doctor")]
    public class DoctorController : Controller
    {

        private IRepositoryWrapper _repo;
        private readonly IMapper _mapper;

        public DoctorController(IRepositoryWrapper repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var user = _repo.User.FindByCondition(u => u.Id == claim.Value).FirstOrDefault();

            var queue = _repo.Queue.FindByCondition(i => i.UserId == user.Id).FirstOrDefault();
            if (queue == null)
            {
                queue = new Queue
                {
                    UserId = user.Id,
                    RoomNo = user.RoomNo,
                    Timestamp = DateTime.UtcNow
                };
                await _repo.Queue.AddAsync(queue);

            }
            queue.OwnerInitials = String.Concat(user.FirstName.First(), user.LastName.First());
            _repo.Queue.Update(queue);
            await _repo.SaveAsync();

            var outputQueue = _mapper.Map<Queue>(queue);

            return View(outputQueue);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Next()
        {
            
            if (ModelState.IsValid)
            {
                var claimsIdentity = (ClaimsIdentity)this.User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

                var queue = _repo.Queue.FindByCondition(u => u.UserId == claim.Value).FirstOrDefault();
                queue.QueueNo++;
                _repo.Queue.Update(queue);
                await _repo.SaveAsync();

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