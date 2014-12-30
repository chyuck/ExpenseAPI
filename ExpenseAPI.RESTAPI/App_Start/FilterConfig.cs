using System.Web;
using System.Web.Mvc;

namespace ExpenseAPI.RESTAPI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}