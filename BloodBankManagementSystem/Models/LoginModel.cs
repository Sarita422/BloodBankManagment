using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodBankManagementSystem.Models
{
    public class LoginModel
    {
        public int Rid { get; set; }
        public string EmailMob { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Name { get; set; }
        public int Isvalid { get; set; }
        public string Msg { get; set; }
    }
}