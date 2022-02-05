using System;
using System.Collections.Generic;

#nullable disable

namespace back.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
