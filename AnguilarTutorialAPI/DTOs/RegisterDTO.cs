﻿using System.ComponentModel.DataAnnotations;

namespace AnguilarTutorialAPI.DTOs
{
    public class RegisterDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
