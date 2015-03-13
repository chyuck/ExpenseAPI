using System;
using System.Runtime.Serialization;
using Validator.Attributes;

namespace ExpenseAPI.Models
{
    [DataContract(Name = "transaction")]
    public class TransactionPut
    {
        [StringValidate("Category name must have 1-20 characters.",
            CanBeEmpty = false, CanBeNull = false, MinLength = 1, MaxLength = 20)]
        [DataMember(Name = "category")]
        public string Category { get; set; }

        [DecimalValidate("Invalid value for USD amount.")]
        [DataMember(Name = "usd")]
        public decimal Usd { get; set; }

        [StringValidate("Comment must have 1-100 characters.",
            CanBeEmpty = false, CanBeNull = false, MinLength = 1, MaxLength = 100)]
        [DataMember(Name = "comment")]
        public string Comment { get; set; }

        [DataMember(Name = "utcTime")]
        public DateTime UtcTime { get; set; }
    }
}
