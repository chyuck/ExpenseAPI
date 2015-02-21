using System.Runtime.Serialization;

namespace ExpenseAPI.Models.Category
{
    [DataContract(Name = "category")]
    public class CategoryPost
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }
    }
}
