using System;
using System.ComponentModel.DataAnnotations;

namespace web_api_net5.Models
{
    public class RegisterUserDto
    {
        //TODO walidacja regexami
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int RoleId { get; set; } = 1;
    }
}
