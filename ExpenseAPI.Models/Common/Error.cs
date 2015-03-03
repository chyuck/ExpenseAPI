using System.Runtime.Serialization;

namespace ExpenseAPI.Models
{
    [DataContract(Name = "error")]
    public class Error
    {
        [DataMember(Name = "message")]
        public string Message { get; set; }
    }
}
