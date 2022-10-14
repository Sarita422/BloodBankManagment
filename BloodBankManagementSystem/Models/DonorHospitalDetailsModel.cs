using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodBankManagementSystem.Models
{
    public class DonorHospitalDetailsModel
    {
        public int Rid { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
    }
}