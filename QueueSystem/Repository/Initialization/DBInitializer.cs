using Entities;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Initialization
{
    public class DBInitializer : IDBInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly RepositoryContext _repo;

        public const string AdminUser = "Admin";
        public const string DoctorUser = "Doctor";
        public const string NurseUser = "Nurse";
        public const string PatientUser = "Patient";

        public DBInitializer(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, RepositoryContext repo)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _repo = repo;
        }

        public async void Initialize()
        {
            //Create roles
            if (!_roleManager.RoleExistsAsync(AdminUser).GetAwaiter().GetResult())
                 _roleManager.CreateAsync(new IdentityRole(AdminUser)).GetAwaiter().GetResult();
            if (! _roleManager.RoleExistsAsync(DoctorUser).GetAwaiter().GetResult())
                 _roleManager.CreateAsync(new IdentityRole(DoctorUser)).GetAwaiter().GetResult();
            if (! _roleManager.RoleExistsAsync(NurseUser).GetAwaiter().GetResult())
                 _roleManager.CreateAsync(new IdentityRole(NurseUser)).GetAwaiter().GetResult();
            if (! _roleManager.RoleExistsAsync(PatientUser).GetAwaiter().GetResult())
                 _roleManager.CreateAsync(new IdentityRole(PatientUser)).GetAwaiter().GetResult();

            //Create admin user
            _userManager.CreateAsync(new User
            {
                UserName="admin",
                Email="admin@gmail.com",
                EmailConfirmed= true,
                FirstName="Piotr",
                LastName="Bakun",
                RoomNo=12
            }, "piotrek").GetAwaiter().GetResult();

            IdentityUser user = await _repo.User.FirstOrDefaultAsync(u => u.UserName == "admin");

            await _userManager.AddToRoleAsync(user, AdminUser);
        }
    }
}
