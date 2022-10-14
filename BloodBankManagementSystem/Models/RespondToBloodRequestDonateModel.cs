using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodBankManagementSystem.Models
{
    public class RespondToBloodRequestDonateModel
    {
        public int Rid { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public bool BloodAvailability { get; set; }
        public int Cost { get; set; }
        public string Date { get; set; }
        public string Message { get; set; }
    }
}