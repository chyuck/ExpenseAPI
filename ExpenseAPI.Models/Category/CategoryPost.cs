using System.Runtime.Serialization;
using Validator.Attributes;

namespace ExpenseAPI.Models
{
    [DataContract(Name = "category")]
    public class CategoryPost
    {
        [StringValidate("Category name must have 1-20 characters.", 
            CanBeEmpty = false, CanBeNull = false, MinLength = 1, MaxLength = 20)]
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [EnumStringValidate("Category type has invalid value.", 
            typeof(CategoryType), CanBeNull = false)]
        [DataMember(Name = "type")]
        public string Type { get; set; }
    }
}
