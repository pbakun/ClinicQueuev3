using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Areas.Patient.Controllers
{
    [Area("Patient")]
    public class PatientController : Controller
    {
        [Route("patient/{roomNo}")]
        public IActionResult Index(string roomNo)
        {
            return View();
        }
    }
}