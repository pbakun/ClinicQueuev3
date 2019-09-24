using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.BackgroundServices.Tasks;
using WebApp.Models;
using WebApp.Utility;

namespace WebApp.Areas.Admin.Controllers
{
    [Authorize(Roles = StaticDetails.AdminUser)]
    [Area("Admin")]
    public class SettingsController : Controller
    {
        private readonly IMapper _mapper;

        [BindProperty]
        public ApplicationSettings ApplicationSettings { get; set; }

        public SettingsController(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var ApplicationSettings = _mapper.Map<ApplicationSettings>(SettingsHandler.ApplicationSettings);
            
            ApplicationSettings.PatientViewNotificationAfterDoctorDisconnectedDelay = FromMiliseconds(ApplicationSettings.PatientViewNotificationAfterDoctorDisconnectedDelay);

            return View(ApplicationSettings);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit(ApplicationSettings settings)
        {
            bool somethingChanged = false;
            if(settings.PatientViewNotificationAfterDoctorDisconnectedDelay < 1000)
            {
                SettingsHandler.ApplicationSettings.PatientViewNotificationAfterDoctorDisconnectedDelay = ToMiliseconds(settings.PatientViewNotificationAfterDoctorDisconnectedDelay);
                somethingChanged = true;
                
            }
            if (!settings.MessageWhenNoDoctorActiveInQueue.Equals(string.Empty))
            {
                SettingsHandler.ApplicationSettings.MessageWhenNoDoctorActiveInQueue = settings.MessageWhenNoDoctorActiveInQueue;
                somethingChanged = true;
            }

            if (somethingChanged)
            {
                SettingsHandler.Settings.WriteSettingsExceptRooms(SettingsHandler.ApplicationSettings);
            }

            ApplicationSettings = _mapper.Map<ApplicationSettings>(SettingsHandler.ApplicationSettings);
            ApplicationSettings.PatientViewNotificationAfterDoctorDisconnectedDelay = FromMiliseconds(ApplicationSettings.PatientViewNotificationAfterDoctorDisconnectedDelay);

            return View("Index", ApplicationSettings);
        }

        private int ToMiliseconds(int minutes)
        {
            return minutes * 60 * 1000;
        }
        
        private int FromMiliseconds(int miliseconds)
        {
            return miliseconds / (60 * 1000);
        }
    }
}