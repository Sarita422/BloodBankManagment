using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodBankManagementSystem.Models
{
    public class RegistrationModel
    {
        public string Role { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Cpassword { get; set; }
        public string Gender { get; set; }
        public List<string> GenderList = new List<string>();
        public int? Age { get; set; }
        public int? Weight { get; set; }
        public string RegDesignation { get; set; }
        public string State { get; set; }
        public List<string> StateList = new List<string>();
        public string City { get; set; }
        public List<string> CityList = new List<string>();
        public string Address { get; set; }
        public string Mobile { get; set; }
        public string OfficeNum { get; set; }
        public string Ext { get; set; }
    }
}