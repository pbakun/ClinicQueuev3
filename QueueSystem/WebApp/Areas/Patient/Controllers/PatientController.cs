using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using WebApp.Models;
using WebApp.Models.ViewModel;
using WebApp.ServiceLogic;
using WebApp.Utility;

namespace WebApp.Areas.Patient.Controllers
{
    [Area("Patient")]
    public class PatientController : Controller
    {

        private readonly IRepositoryWrapper _repo;
        private readonly IMapper _mapper;
        private readonly IQueueService _queueService;

        [BindProperty]
        public PatientViewModel PatientVM { get; set; }

        public PatientController(IRepositoryWrapper repo, IMapper mapper, IQueueService queueService)
        {
            _repo = repo;
            _mapper = mapper;
            _queueService = queueService;
        }

        [Route("patient/{roomNo}")]
        public IActionResult Index(string roomNo)
        {
            int roomNoInt = Convert.ToInt32(roomNo);

            var queue = _queueService.FindByRoomNo(roomNoInt);
            PatientVM = new PatientViewModel();
            
            if (queue == null)
            {
                queue = new Queue();
                queue.RoomNo = roomNoInt;
                PatientVM.DoctorFullName = string.Empty;
            }
            else
            {
                var user = _repo.User.FindByCondition(u => u.Id == queue.UserId).FirstOrDefault();
                PatientVM.DoctorFullName = String.Concat(StaticDetails.DoctorNamePrefix, user.FirstName, " ", user.LastName);
            }
            PatientVM.QueueNoMessage = queue.QueueNoMessage;
            PatientVM.QueueAdditionalInfo = queue.AdditionalMessage;

            return View(queue);

        }
    }
}