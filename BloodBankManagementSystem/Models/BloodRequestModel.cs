using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodBankManagementSystem.Models
{
    public class BloodRequestModel
    {
        public int? Rid { get; set; }
        public string Bg { get; set; }
        public List<string> BgList = new List<string>();
        public string BloodProduct { get; set; }
        public List<string> BloodProductList = new List<string>();
        public string Date { get; set; }
        public int? AmountOfBlood { get; set; }
        public int? Cost { get; set; }
        public int? YourQuotation { get; set; }
        public string Message { get; set; }
    }
}