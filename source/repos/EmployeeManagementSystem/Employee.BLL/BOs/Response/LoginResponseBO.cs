using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.BLL.BOs.Response
{
    public class LoginResponseBO
    {
        public string Message { get; set; }
        public string Token { get; set; }

        public bool Succeeded { get; set; }
    }
}
