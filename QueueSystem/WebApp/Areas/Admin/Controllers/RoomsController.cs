using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using WebApp.BackgroundServices.Tasks;
using WebApp.Models;
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
        private readonly ApplicationSettings _appSettings = SettingsHandler.ApplicationSettings;
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

            var availableRooms = SettingsHandler.ApplicationSettings.AvailableRooms;
            
            RoomsViewModel roomVMElement = new RoomsViewModel();
            foreach (var room in availableRooms)
            {
                int usersQuantity = queues.Where(q => q.RoomNo == room).ToList().Count();
                var queue = queues.Where(q => q.RoomNo == room).OrderByDescending(t => t.Timestamp).FirstOrDefault();
                if (queue != null)
                {
                    var user = _repo.User.FindByCondition(u => u.Id == queue.UserId).FirstOrDefault();

                    roomVMElement.Queue = queue;
                    roomVMElement.RoomNo = room;
                    roomVMElement.UserName = user.UserName;
                    roomVMElement.QuantityOfAssignedUsers = usersQuantity;
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
            if (!_appSettings.AvailableRooms.Contains(roomNo))
            {
                if(roomNo > 0)
                {
                    _appSettings.AvailableRooms.Add(roomNo);
                    SettingsHandler.Settings.WriteAllSettings(_appSettings);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int roomNo)
        {
            var queues = _queueService.FindAll();

            queues = queues.Where(q => q.RoomNo == roomNo).OrderByDescending(q => q.Timestamp).ToList();
            if (queues != null)
            {
                foreach (var queue in queues)
                {
                    queue.Timestamp = queue.Timestamp.ToLocalTime();
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

        public async Task<IActionResult> Delete(int roomNo)
        {
            _appSettings.AvailableRooms.Remove(roomNo);
            SettingsHandler.Settings.WriteAllSettings(_appSettings);

            return RedirectToAction(nameof(Index));
        }
    }
}