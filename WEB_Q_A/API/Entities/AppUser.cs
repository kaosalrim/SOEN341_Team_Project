using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace API.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateJoined { get; set; } = DateTime.Now;
        public DateTime LastActive { get; set; } = DateTime.Now;
        public ICollection<Question> Questions { get; set; }
        public ICollection<Answer> Answers { get; set; }
        public Photo Photo { get; set; }
        public ICollection<AppUserRole> UserRoles { get; set; }

    }
}
