using System.Runtime.Serialization;

namespace ExpenseAPI.Models.Category
{
    [DataContract]
    public enum CategoryType
    {
        [EnumMember]
        Income,

        [EnumMember]
        Expense
    }
}
