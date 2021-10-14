using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class LoginDataDto
    {
        [Required(ErrorMessage = "Please Enter the Email Id")]
        [Display (Name = "UserName")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please Enter the password")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
