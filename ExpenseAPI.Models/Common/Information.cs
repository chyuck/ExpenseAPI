using System.Runtime.Serialization;

namespace ExpenseAPI.Models
{
    [DataContract(Name = "information")]
    public class Information
    {
        [DataMember(Name = "message")]
        public string Message { get; set; }
    }
}
