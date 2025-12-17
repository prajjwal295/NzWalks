using Employee.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Dal.Entities
{
    public class User
    {
        public int id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string[] Roles { get; set; }
        public DateTime? LastLogin { get; set; } = null;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}
