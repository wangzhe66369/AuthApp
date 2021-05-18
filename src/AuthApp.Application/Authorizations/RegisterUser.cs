using System;
using System.ComponentModel.DataAnnotations;

namespace AuthApp.Authorizations
{
    public class RegisterUser
    {
        [Required, MinLength(4)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "邮箱")]
        //[EmailAddress]
        public string Email { get; set; }

        [MinLength(6)]
        public string Password { get; set; }

        public DateTimeOffset BirthDate { get; set; }
    }
}