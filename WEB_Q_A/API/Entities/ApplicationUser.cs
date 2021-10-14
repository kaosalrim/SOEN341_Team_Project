using Microsoft.AspNetCore.Identity;
using System;

namespace API.Entities
{
    public class ApplicationUser:IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime  DateJoined { get; set; }

    }
}
