using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using WebApp.Models;
using WebApp.Utility;

namespace WebApp.Areas.Admin.Controllers
{
    [Authorize(Roles = StaticDetails.AdminUser)]
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IRepositoryWrapper _repo;
        private readonly IMapper _mapper;

        public UserController(IRepositoryWrapper repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var role = claimsIdentity.FindFirst(ClaimTypes.Role);

            var users = _repo.User.FindAll().ToList();

            var outputUsers = _mapper.Map <List<User>> (users);
            return View(outputUsers);
        }

        public async Task<IActionResult> Lock(string id)
        {
            if (id == null)
                return NotFound();

            var claimIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if(id == claim.Value)
                return RedirectToAction(nameof(Index));

            var user = _repo.User.FindByCondition(u => u.Id == id).FirstOrDefault();

            if (user == null)
                return NotFound();

            user.LockoutEnd = DateTime.Now.AddYears(1000);

            _repo.User.Update(user);
            await _repo.SaveAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> UnLock(string id)
        {
            if (id == null)
                return NotFound();

            var user = _repo.User.FindByCondition(u => u.Id == id).FirstOrDefault();

            if (user == null)
                return NotFound();

            user.LockoutEnd = DateTime.Now;

            _repo.User.Update(user);
            await _repo.SaveAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}