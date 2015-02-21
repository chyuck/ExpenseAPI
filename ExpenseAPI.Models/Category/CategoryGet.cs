using System;
using System.Runtime.Serialization;

namespace ExpenseAPI.Models.Category
{
    [DataContract(Name = "category")]
    public class CategoryGet
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "type")]
        public CategoryType Type { get; set; }

        [DataMember(Name = "created")]
        public DateTime Created { get; set; }

        [DataMember(Name = "updated")]
        public DateTime Updated { get; set; }
    }
}
