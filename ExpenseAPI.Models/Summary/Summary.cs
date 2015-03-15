using System.Runtime.Serialization;

namespace ExpenseAPI.Models
{
    [DataContract(Name = "summary")]
    public class Summary
    {
        [DataMember(Name = "categories")]
        public CategorySummary[] Categories { get; set; }

        [DataMember(Name = "total")]
        public decimal Total { get; set; }
    }
}
