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

            return View(ApplicationSettings);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit(ApplicationSettings settings)
        {
            ApplicationSettings.PatientViewNotificationAfterDoctorDisconnectedDelay = settings.PatientViewNotificationAfterDoctorDisconnectedDelay;
            SettingsHandler.Settings.WriteSettingsExceptRooms(ApplicationSettings);

            return View("Index", ApplicationSettings);
        }
    }
}