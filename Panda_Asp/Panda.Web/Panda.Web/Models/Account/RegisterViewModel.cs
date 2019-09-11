﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Panda.Web.Models.Account
{
    public class RegisterViewModel
    {

        public string Username { get; set; }

       // [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        //[Required, DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

        //[DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
