using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodBankManagementSystem.Models
{
    public class DonateBloodRequestModel
    {
        public int? Rid { get; set; }
        public string Bg { get; set; }
        public List<string> BgList = new List<string>();
        public string BloodProduct { get; set; }
        public string Date { get; set; }
        public bool AnyIlln { get; set; }
        public string IllnDesc { get; set; }
        public int? AmountOfBlood { get; set; }
        public int? Cost { get; set; }
        public int? YourQuotation { get; set; }
        public string Message { get; set; }
    }
}