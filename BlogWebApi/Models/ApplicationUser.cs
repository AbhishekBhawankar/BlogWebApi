﻿using Microsoft.AspNetCore.Identity;

namespace BlogWebApi.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }

        public string MobileNo { get; set; }
    }
}
