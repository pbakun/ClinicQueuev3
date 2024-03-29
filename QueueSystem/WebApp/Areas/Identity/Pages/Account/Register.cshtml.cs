﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Repository.Interfaces;
using WebApp.BackgroundServices.Tasks;
using WebApp.Models;
using WebApp.Utility;

namespace WebApp.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IRepositoryWrapper _repo;
        private readonly IMapper _mapper;
        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager,
            IRepositoryWrapper repo,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
            _repo = repo;
            _mapper = mapper;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
            [Required]
            public string UserName { get; set; }
            [Required]
            public string FirstName { get; set; }
            [Required]
            public string LastName { get; set; }
            public int RoomNo { get; set; }
            public List<int> AvailableRooms { get; set; }
            public List<string> AvailableDoctors { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            var claimIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.Role);

            ReturnUrl = returnUrl;
            if(claim != null && claim.Value == StaticDetails.AdminUser)
            {
                Input = new InputModel()
                {
                    AvailableRooms = SettingsHandler.ApplicationSettings.AvailableRooms
                };
            }
            else
            {
                var repoUsers = _userManager.GetUsersInRoleAsync(StaticDetails.DoctorUser).Result;
                var users = _mapper.Map<List<User>>(repoUsers);
                var DoctorFullNames = new List<string>();
                foreach(var user in users)
                {
                    DoctorFullNames.Add(String.Concat(StaticDetails.DoctorNamePrefix, user.FirstName, " ", user.LastName));
                }

                Input = new InputModel()
                {
                    AvailableDoctors = DoctorFullNames
                };
            }
            
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            string role = Request.Form["rdUserRole"].ToString();

            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                //var user = new IdentityUser { UserName = Input.Email, Email = Input.Email };
                var user = new User
                {
                    Email = Input.Email,
                    UserName = Input.UserName,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    RoomNo = Input.RoomNo
                };
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    //creates all roles if not created yet
                    if (!await _roleManager.RoleExistsAsync(StaticDetails.AdminUser))
                        await _roleManager.CreateAsync(new IdentityRole(StaticDetails.AdminUser));
                    if (!await _roleManager.RoleExistsAsync(StaticDetails.DoctorUser))
                        await _roleManager.CreateAsync(new IdentityRole(StaticDetails.DoctorUser));
                    if (!await _roleManager.RoleExistsAsync(StaticDetails.NurseUser))
                        await _roleManager.CreateAsync(new IdentityRole(StaticDetails.NurseUser));
                    if (!await _roleManager.RoleExistsAsync(StaticDetails.PatientUser))
                        await _roleManager.CreateAsync(new IdentityRole(StaticDetails.PatientUser));

                    switch (role)
                    {
                        case StaticDetails.AdminUser:
                            await _userManager.AddToRoleAsync(user, StaticDetails.AdminUser);
                            break;
                        case StaticDetails.DoctorUser:
                            await _userManager.AddToRoleAsync(user, StaticDetails.DoctorUser);
                            break;
                        case StaticDetails.NurseUser:
                            await _userManager.AddToRoleAsync(user, StaticDetails.NurseUser);
                            break;
                        default:
                            //await _userManager.AddToRoleAsync(user, StaticDetails.AdminUser);
                            await _userManager.AddToRoleAsync(user, StaticDetails.PatientUser);
                            return LocalRedirect(returnUrl);
                    }

                    _logger.LogInformation("User created a new account with password.");

                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var callbackUrl = Url.Page(
                    //    "/Account/ConfirmEmail",
                    //    pageHandler: null,
                    //    values: new { userId = user.Id, code = code },
                    //    protocol: Request.Scheme);

                    //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                    //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    //await _signInManager.SignInAsync(user, isPersistent: false);

                    //return LocalRedirect(returnUrl);

                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
