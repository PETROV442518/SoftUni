using SIS.MvcFramework.Attributes.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SULS.Web.ViewModels.Users
{
   public  class UserRegisterInputModel
    {
        [RequiredSis]
        [StringLengthSis(5, 20, "Username must be between5 and 20 symbols long.")]
        public string Username { get; set; }


        [RequiredSis]
        [StringLengthSis(6, 20, "Password must be between5 and 20 symbols long.")]
        public string Password { get; set; }

        [EmailSis]
        [RequiredSis]
        public string Email { get; set; }

        [RequiredSis]
        public string ConfirmPassword { get; set; }
    }
}
