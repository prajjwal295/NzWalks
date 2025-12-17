using Employee.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.BLL.BOs.Response
{
    public class SignupResopnseBO
    {
        public string Message { get; set; }
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string[] Roles { get; set; }
        public bool Succeeded { get; set; } 
    }
}
