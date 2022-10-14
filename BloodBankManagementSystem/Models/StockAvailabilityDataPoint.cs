using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BloodBankManagementSystem.Models
{
    [DataContract]
    public class StockAvailabilityDataPoint
    {
        [DataMember(Name = "stocklabel")]
        public string Label { get; set; }
        [DataMember(Name = "stockcost")]
        public double Cost { get; set; }
    }
}