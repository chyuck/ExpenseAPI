using System.Runtime.Serialization;

namespace ExpenseAPI.Models
{
    [DataContract(Name = "category")]
    public class CategorySummary
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "total")]
        public decimal Total { get; set; }
    }
}
