using System.Runtime.Serialization;

namespace ExpenseAPI.Models
{
    [DataContract(Name = "user")]
    public class UserGet
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}
