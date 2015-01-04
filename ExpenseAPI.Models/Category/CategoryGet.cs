using System.Runtime.Serialization;

namespace ExpenseAPI.Models.Category
{
    [DataContract(Name = "category")]
    public class CategoryGet
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}
