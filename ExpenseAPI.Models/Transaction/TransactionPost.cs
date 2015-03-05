using System;
using System.Runtime.Serialization;
using Validator.Attributes;

namespace ExpenseAPI.Models
{
    [DataContract(Name = "transaction")]
    public class TransactionPost
    {
        [DataMember(Name = "id")]
        [StringValidate("ID must have 1-20 characters.",
            CanBeEmpty = false, CanBeNull = false, MinLength = 1, MaxLength = 20)]
        public string Id { get; set; }

        [DecimalValidate("Invalid value for USD amount.")]
        [DataMember(Name = "usd")]
        public decimal Usd { get; set; }

        [StringValidate("Comment must have 1-100 characters.",
            CanBeEmpty = false, CanBeNull = false, MinLength = 1, MaxLength = 100)]
        [DataMember(Name = "comment")]
        public string Comment { get; set; }

        [DataMember(Name = "time")]
        public DateTime Time { get; set; }
    }
}
