using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using WebApp.Models;

namespace WebApp.Areas.Patient.Controllers
{
    [Area("Patient")]
    public class PatientController : Controller
    {

        private readonly IRepositoryWrapper _repo;
        private readonly IMapper _mapper;

        public PatientController(IRepositoryWrapper repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [Route("patient/{roomNo}")]
        public IActionResult Index(string roomNo)
        {

           int roomNoInt = Convert.ToInt32(roomNo);

            //todo: what in case of few queue with the same room?
            var queue = _repo.Queue.FindByCondition(m => m.RoomNo == roomNoInt).FirstOrDefault();
            
            if(queue == null)
            {
                queue = new Queue();
                queue.RoomNo = roomNoInt;
            }

            return View(queue);

        }
    }
}