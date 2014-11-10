using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Payroll.Model;
using Payroll.Models;
using Payroll.BL;
using System.IO;
using Microsoft.VisualBasic;

/// <summary>
/// EmployeeController handles all calls to Employee related operations and 
/// routing to the appropriate View
/// </summary>

namespace Payroll.Controllers
{
    
    public class EmployeeController : Controller
    {
        private Payroll.BL.IPaySlipGenerator paySlipGenerator;
        List<Employee> lstEmp = new List<Employee>();


        /// <summary>
        /// EmployeeController constructor recieves the appropriate 
        /// IPaySlipGenerator type as a parameter using Dependency injection(Unity).
        /// This is so that the actual pay-slip implementation logic
        /// is loosely coupled with the Controller.
        /// </summary>
        /// <param name="paySlipGen">recieves an object of type PaySlipGenerator, 
        ///  at runtime by Unity(DI cont)>/param>
        public EmployeeController(IPaySlipGenerator paySlipGen)
        {
            this.paySlipGenerator = paySlipGen;
        }
        
        /// <summary>
        /// Displays Monthly Payslip Input screen
        /// </summary>
        public ActionResult Index()
        {
            return View("Index");
        }

        /// <summary>
        /// This method is invoked in response to a postback from Index view.
        /// GeneratePaySlip method calculates the monthly payslip information.
        /// Returns a view which displays the payslip data in a grid
        /// </summary>
        /// <param name="empObj">Contains the forms data collection(empObj) via model binding</param>
       [HttpPost]
        public ActionResult Index(Employee empObj)
        {
                return View("EmployeePaySlip", paySlipGenerator.GeneratePaySlip(empObj));
         }

       /// <summary>
       /// Used for bulk upload of CSV files.
       /// This method is invoked when the user clicks on the CSV upload link.
       /// Returns a view which displays the option to select a CSV input data file
       /// </summary>

        public ActionResult UploadCSVFile()
       {
           return View("UploadCSVFile");
       }

        /// <summary>
        /// This method is invoked in response to a postback from UploadCSVFile view.
        /// Uses third party binary for CSV upload.
        /// Returns a view which displays the payslip data in a grid
        /// </summary>
        /// <param name="empObj">Contains the forms data collection(empObj) via model binding</param>

        public ActionResult ProcessCsv(CSVEmployeeEvent[] model)
        {
            if (model == null)
                return RedirectToAction("UploadCSVFile", "Employee");

            //Iterate through each object and create an employee list
            foreach (CSVEmployeeEvent item in model)
            {
                Employee emp = new Employee();
                emp.FirstName = item.FirstName;
                emp.LastName = item.LastName;
                emp.AnnualSalary = item.AnnualSalary;
                emp.SuperRate = item.SuperRate;
                emp.PaymentStartDate = item.PaymentStartDate;
                lstEmp.Add(emp);

            }

            return View("EmployeePaySlip", paySlipGenerator.GeneratePaySlips(lstEmp));
        }

        /// <summary>
        /// Handles all controller exceptions by overriding the OnException method.
        /// </summary>
        protected override void OnException(ExceptionContext filterContext)
        {
            Exception e = filterContext.Exception;
            //Log Exception e
            filterContext.ExceptionHandled = true;
            filterContext.Result = new ViewResult()
            {
                ViewName = "Error"
            };
        }
     }
}
