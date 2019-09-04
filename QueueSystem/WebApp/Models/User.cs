﻿using Microsoft.AspNetCore.Identity;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class User : IdentityUser
    {
        //[PrimaryKey, AutoIncrement]
        //public int Id { get; set; }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int RoomNo { get; set; }
        

    }
}