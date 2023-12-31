﻿using Microsoft.AspNetCore.Identity;

namespace AnguilarTutorialAPI.Entity
{
    public class AppRole : IdentityRole<int>
    {
        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}
