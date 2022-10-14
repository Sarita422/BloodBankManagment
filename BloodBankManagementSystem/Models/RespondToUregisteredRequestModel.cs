using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodBankManagementSystem.Models
{
    public class RespondToUregisteredRequestModel
    {
        public int Pid { get; set; }
        public string Pname { get; set; }
        public string Cname { get; set; }
        public string Mobile { get; set; }
        public bool BloodAvailability { get; set; }
        public int Cost { get; set; }
        public string Date { get; set; }
        public string Message { get; set; }
    }
}