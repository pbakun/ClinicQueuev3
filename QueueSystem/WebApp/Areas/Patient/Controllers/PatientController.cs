using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using WebApp.Data;
using WebApp.Models;


namespace WebApp.Areas.Patient.Controllers
{
    [Area("Patient")]
    public class PatientController : Controller
    {

        private readonly ApplicationDbContext _queueDb;

        public PatientController(ApplicationDbContext queueDb)
        {
            _queueDb = queueDb;
        }

        [Route("patient/{roomNo}")]
        public IActionResult Index(string roomNo)
        {

           int roomNoInt = Convert.ToInt32(roomNo);

            //todo: what in case of few queue with the same room?
            Queue queue = _queueDb.Queue.Where(m => m.RoomNo == roomNoInt).FirstOrDefault();
            if(queue == null)
            {
                queue = new Queue();
                queue.RoomNo = roomNoInt;

            }

            return View(queue);

        }
    }
}