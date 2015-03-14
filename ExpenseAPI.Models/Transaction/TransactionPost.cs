using System;
using System.Runtime.Serialization;
using Validator;
using Validator.Attributes;

namespace ExpenseAPI.Models
{
    [DataContract(Name = "transaction")]
    public class TransactionPost
    {
        [DataMember(Name = "usd")]
        public decimal Usd { get; set; }

        [StringValidate("Comment must have 1-100 characters.",
            CanBeEmpty = false, CanBeNull = true, MinLength = 1, MaxLength = 100)]
        [DataMember(Name = "comment")]
        public string Comment { get; set; }

        [DataMember(Name = "date")]
        public DateTime Date { get; set; }

        [MethodValidate]
        internal ValidationError ValidateUsd()
        {
            if (Usd == default(decimal))
                return new ValidationError("Usd", "USD must be specified.");

            return null;
        }

        [MethodValidate]
        internal ValidationError ValidateDate()
        {
            if (Date == default(DateTime))
                return new ValidationError("Date", "Date must be specified.");

            return null;
        }
    }
}
