using Microsoft.AspNetCore.Identity;
using System;

namespace AuthApp.Domian
{
    public class User : IdentityUser
    {
        public string City { get; set; }
        public DateTimeOffset BirthDate { get; set; }
    }

    public class Role : IdentityRole
    {
    }
}