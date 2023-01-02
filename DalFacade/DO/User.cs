using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public struct User
    {
        public string? Name { get; set; }
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
        public string? UserAddress { get; set; }
        public string? Passcode { get; set; }
        public bool AdminAccess { get; set; }
    }
}
