using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodBankManagementSystem.Models
{
    public class UnregisteredUserModel
    {
        public int Pid { get; set; }
        public string PatientName { get; set; }
        public string Bg { get; set; }
        public List<string> BgList = new List<string>();
        public string BloodProduct { get; set; }
        public List<string> BloodProductList = new List<string>();
        public string State { get; set; }
        public List<string> StateList = new List<string>();
        public string City { get; set; }
        public List<string> CityList = new List<string>();
        public string HospitalName { get; set; }
        public string AmountOfBlood { get; set; }
        public string ContactName { get; set; }
        public string Mobile { get; set; }
        public int? Cost { get; set; }
        public int? YourQuotation { get; set; }
        public string Date { get; set; }
    }
    public class UnregisteredSearchValuesModel
    {
        public string City { get; set; }
        public List<string> CityList = new List<string>();
        public string Date { get; set; }
    }
    public class UnregisteredViewModel
    {
        public List<UnregisteredUserModel> LstUnregisteredUserModels { get; set; }
        public UnregisteredSearchValuesModel SearchValuesModel { get; set; }
    }
}