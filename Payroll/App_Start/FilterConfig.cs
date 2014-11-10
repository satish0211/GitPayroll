using System.Web;
using System.Web.Mvc;
using System.Diagnostics.CodeAnalysis;
namespace Payroll
{
        [ExcludeFromCodeCoverage]
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //--Added for Global Exception Handling
            filters.Add(new HandleErrorAttribute
            {
                View = "Error"
            }, 1);

            
            filters.Add(new HandleErrorAttribute(),2);
        }
    }
}