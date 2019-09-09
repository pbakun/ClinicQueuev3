using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using WebApp.Models;

namespace WebApp.Areas.Main.Controllers
{
    [Area("Main")]
    public class HomeController : Controller
    {

        private IRepositoryWrapper _db;

        public HomeController(IRepositoryWrapper db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var bla = _db.User.FindAll();
            var queue = _db.Queue.FindAll();
            //var user = _db.User.FindByCondition(u => u.UserName == "admin").FirstOrDefault();
            //var queue1 = _db.Queue.FindByCondition(q => q.RoomNo == 12).FirstOrDefault();
            //queue1.UserId = user.Id;
            //_db.Queue.Update(queue1);
            //foreach(var element in queue)
            //{
            //    if(element.QueueNo==0)
            //        _db.Queue.Delete(element);
            //}
            //_db.SaveAsync();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
