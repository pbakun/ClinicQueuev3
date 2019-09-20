using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.BackgroundServices.Tasks
{
    public class SettingsHandler
    {
        private static string AppDirectory = Environment.CurrentDirectory;
        private static string directory = Path.Combine(AppDirectory, "AppData");
        private static string path = Path.Combine(directory, "AppSettings.json");

        public static ApplicationSettings ApplicationSettings { get; private set; }
        

        private static readonly SettingsHandler _settingsHandler = new SettingsHandler();

        public static SettingsHandler Settings => _settingsHandler;


        private SettingsHandler()
        {
        }


        public ApplicationSettings ReadSettings()
        {
            CheckFileExists();

            var json = File.ReadAllText(path);
           
            ApplicationSettings = JsonConvert.DeserializeObject<ApplicationSettings>(json);

            return ApplicationSettings;
            
        }

        public void WriteAllSettings(ApplicationSettings settings)
        {
            CheckFileExists();

            ApplicationSettings = settings;

            string json = JsonConvert.SerializeObject(settings, Formatting.Indented);

            File.WriteAllText(path, json);

        }

        public void WriteSettingsExceptRooms(ApplicationSettings settings)
        {
            var rooms = ApplicationSettings.AvailableRooms;
            ApplicationSettings = settings;
            ApplicationSettings.AvailableRooms = rooms;

            WriteAllSettings(ApplicationSettings);
        }

        public void WriteNewRoom(int roomNo)
        {

        }


        private void CheckFileExists()
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (!File.Exists(path))
            {
                File.Create(path).Dispose();
                WriteAllSettings(new ApplicationSettings()
                {
                    AvailableRooms = new List<int>(),
                    QueueOccupiedMessage = string.Empty
                });
            }
        }

        public ApplicationSettings GetSettings()
        {
            return ApplicationSettings;
        }


    }
}
