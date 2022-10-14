using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodBankManagementSystem.Models
{
    public class PendingBloodRequestModel
    {
        public int Rid { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string Bg { get; set; }
        public string ProductType { get; set; }
        public string IllnDesc { get; set; }
        public int AmountOfBlood { get; set; }
        public int YourQuotation { get; set; }
        public string Mobile { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
    }
    public class SearchValuesBloodRequest
    {
        public string HospInd { get; set; }
        public List<string> HospIndList = new List<string>();
        public string Date { get; set; }
        public string City { get; set; }
        public List<string> CityList = new List<string>();
    }
    public class BloodRequestViewModel
    {
        public List<PendingBloodRequestModel> ListBloodRequest { get; set; }
        public SearchValuesBloodRequest ListSearchValues { get; set; }
    }
}