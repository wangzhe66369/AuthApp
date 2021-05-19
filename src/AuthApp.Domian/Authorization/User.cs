using Microsoft.AspNetCore.Identity;
using System;

namespace AuthApp.Authorization.Users
{
    public class User : IdentityUser
    {
        public string City { get; set; }
        public DateTimeOffset BirthDate { get; set; }
    }

}