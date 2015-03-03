using System.Runtime.Serialization;
using Validator.Attributes;

namespace ExpenseAPI.Models
{
    [DataContract(Name = "category")]
    public class CategoryPut
    {
        [DataMember(Name = "name")]
        [StringValidate("Category name must have 1-20 characters.",
           CanBeEmpty = false, CanBeNull = false, MinLength = 1, MaxLength = 20)]
        public string Name { get; set; }

        [DataMember(Name = "type")]
        [EnumStringValidate("Category type has invalid value.",
            typeof(CategoryType), CanBeNull = false)]
        public string Type { get; set; }
    }
}
