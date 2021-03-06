﻿using System.Runtime.Serialization;

namespace ExpenseAPI.Models
{
    [DataContract(Name = "category")]
    public class CategoryGet
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }
    }
}
