﻿
namespace EConsult_T.Api.Models
{
    public class UserRegistrationDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
    }
}
