using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        [BindProperty]
        public ApplicationSettings ApplicationSettings { get; set; }


        public IActionResult Index()
        {
            ApplicationSettings = SettingsHandler.ApplicationSettings;

            ApplicationSettings.PatientViewNotificationAfterDoctorDisconnectedDelay = FromMiliseconds(ApplicationSettings.PatientViewNotificationAfterDoctorDisconnectedDelay);

            return View(ApplicationSettings);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit(ApplicationSettings settings)
        {
            if(settings.PatientViewNotificationAfterDoctorDisconnectedDelay < 1000)
            {
                ApplicationSettings.PatientViewNotificationAfterDoctorDisconnectedDelay = ToMiliseconds(settings.PatientViewNotificationAfterDoctorDisconnectedDelay);
                SettingsHandler.Settings.WriteSettingsExceptRooms(ApplicationSettings);
                ApplicationSettings.PatientViewNotificationAfterDoctorDisconnectedDelay = FromMiliseconds(ApplicationSettings.PatientViewNotificationAfterDoctorDisconnectedDelay);

            }

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