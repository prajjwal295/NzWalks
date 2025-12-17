using Employee.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.BLL.BOs.Requests
{
    public class SignupRequestBO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string[] Roles { get; set; }
    }
}
