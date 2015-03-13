using System;
using System.Runtime.Serialization;

namespace ExpenseAPI.Models
{
    [DataContract(Name = "transaction")]
    public class TransactionGet
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "category")]
        public string Category { get; set; }

        [DataMember(Name = "usd")]
        public decimal Usd { get; set; }
        
        [DataMember(Name = "comment")]
        public string Comment { get; set; }

        [DataMember(Name = "utcTime")]
        public DateTime UtcTime { get; set; }
    }
}
