using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using FileHelpers;
using System.IO;
using System.Web.Mvc;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Third party code - Classes required by the third party component Filehelper(CSV upload)
/// CSVEmployeeEvent class represents the data in CSV file and CsvModelBinder class is a generic CSV model binder
/// This global.ascx.cs will wire up the Generic CSV model binder with CSVEmployeeEvent
/// </summary>



namespace Payroll.Models
{
    [ExcludeFromCodeCoverage]
    [DelimitedRecord(",")]
    [IgnoreFirst(1)]
    public class CSVEmployeeEvent
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public decimal AnnualSalary { get; set; }
        public int SuperRate { get; set; }
        public string PaymentStartDate { get; set; }

    }

    //Create a generic CSV model binder.
    [ExcludeFromCodeCoverage]
    public class CsvModelBinder<T> : DefaultModelBinder where T : class
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var csv = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            var file = ((csv.RawValue as HttpPostedFileBase[]) ?? Enumerable.Empty<HttpPostedFileBase>()).FirstOrDefault();

            if (file == null || file.ContentLength < 1)
            {
                bindingContext.ModelState.AddModelError(
                    "",
                    "Please select a valid CSV file"
                );
                return null;
            }

            using (var reader = new StreamReader(file.InputStream))
            {
                try
                {
                    var engine = new FileHelperEngine<T>();
                    return engine.ReadStream(reader);
                }
                catch (Exception c)
                {
                    bindingContext.ModelState.AddModelError(bindingContext.ModelName, c.Message);
                    return null;
                }

            }
        }
    }
}