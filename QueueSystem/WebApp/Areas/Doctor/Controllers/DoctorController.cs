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
using WebApp.Models.ViewModel;
using WebApp.ServiceLogic;
using WebApp.Utility;

namespace WebApp.Areas.Doctor.Controllers
{
    [Authorize(Roles = StaticDetails.AdminUser + "," + StaticDetails.DoctorUser)]
    [Area("Doctor")]
    public class DoctorController : Controller
    {

        private IRepositoryWrapper _repo;
        private readonly IMapper _mapper;
        private readonly IQueueService _queueService;

        [BindProperty]
        public DoctorViewModel DoctorVM { get; set; }

        public DoctorController(IRepositoryWrapper repo, IMapper mapper, IQueueService queueService)
        {
            _repo = repo;
            _mapper = mapper;
            _queueService = queueService;
        }

        public async Task<IActionResult> Index()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var user = _repo.User.FindByCondition(u => u.Id == claim.Value).FirstOrDefault();
            if (user == null)
                return NotFound();

            var queue = _queueService.FindByUserId(user.Id);

            DoctorVM = new DoctorViewModel()
            {
                Queue = queue
            };

            return View(DoctorVM);
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

                var outputQueue = _mapper.Map<Queue>(queue);

                return View("Index", outputQueue);
            }
            
            return NotFound();
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Doctor/Doctor/NewRoomNo")]
        public async Task<IActionResult> NewRoomNo(DoctorViewModel VM)
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            int roomNo = VM.Queue.RoomNo;
            var user = _repo.User.FindByCondition(u => u.Id == claim.Value).FirstOrDefault();
            
            var queue = _queueService.ChangeUserRoomNo(user.Id, roomNo);
            DoctorVM.Queue = queue;

            return View("Index", DoctorVM);
        }
    }
}