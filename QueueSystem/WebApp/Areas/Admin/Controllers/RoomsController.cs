using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using WebApp.Models.ViewModel;
using WebApp.ServiceLogic;
using WebApp.Utility;

namespace WebApp.Areas.Admin.Controllers
{
    [Authorize(Roles = StaticDetails.AdminUser)]
    [Area("Admin")]
    public class RoomsController : Controller
    {
        private readonly IQueueService _queueService;
        private readonly IRepositoryWrapper _repo;

        [BindProperty]
        public List<RoomsViewModel> RoomsVM { get; set; }

        public RoomsController(IQueueService queueService, IRepositoryWrapper repo)
        {
            _queueService = queueService;
            _repo = repo;
            RoomsVM = new List<RoomsViewModel>();
        }

        public async Task<IActionResult> Index()
        {
            var queues = _queueService.FindAll();

            var availableRooms = StaticDetails.AvailableRoomNo;
            RoomsViewModel roomVMElement = new RoomsViewModel();
            foreach (var room in availableRooms)
            {
                var queue = queues.Where(q => q.RoomNo == room).FirstOrDefault();
                if (queue != null)
                {
                    var user = _repo.User.FindByCondition(u => u.Id == queue.UserId).FirstOrDefault();

                    roomVMElement.Queue = queue;
                    roomVMElement.RoomNo = room;
                    roomVMElement.UserName = user.UserName;
                }
                else
                {
                    roomVMElement.RoomNo = room;
                }
                RoomsVM.Add(roomVMElement);
                roomVMElement = new RoomsViewModel();
            }

            return View(RoomsVM);
        }

        public async Task<IActionResult> Create()
        {
            return PartialView("_CreateNewRoom");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] int roomNo)
        {
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int roomNo)
        {
            var queues = _queueService.FindAll();

            queues = queues.Where(q => q.RoomNo == roomNo).ToList();
            if (queues != null)
            {
                foreach (var queue in queues)
                {
                    var roomVMElement = new RoomsViewModel()
                    {
                        Queue = queue,
                        RoomNo = roomNo,
                        UserName = _repo.User.FindByCondition(u => u.Id == queue.UserId).Select(u => u.UserName).FirstOrDefault()
                    };
                    RoomsVM.Add(roomVMElement);
                }
            }


            return View(RoomsVM);
        }
    }
}