using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodBankManagementSystem.Models
{
    public class StockAvailabilityModel
    {
        public string Bg { get; set; }
        public int? WholeBlood { get; set; }
        public int? PackedCells { get; set; }
        public int? FrozenPlasma { get; set; }
        public int? Platelets { get; set; }
    }
    public class BloodCost
    {
        public string Bg { get; set; }
        public string ProductType { get; set; }
        public int Qty { get; set; }
        public string Units { get; set; }
        public int Cost { get; set; }
    }
    public class BloodStockCostViewModel
    {
        public List<StockAvailabilityModel> LstStockAvailability { get; set; }
        public List<BloodCost> LstBloodCost { get; set; }
    }
}