using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Payroll.Model;
using System.IO;
using System.Data;
/// <summary>
/// Implements IPaySlipGenerator, this is passed to the constructor of the Employee controller 
/// at runtime using dependency injection.
/// </summary>
 
namespace Payroll.BL
{
    public sealed class PaySlipGenerator : IPaySlipGenerator
    {

        #region Local Variables

        decimal decAnnualSalary;
        decimal decSuperRate;
        decimal decExcess;
        decimal decCents;
        decimal decCents2;
        decimal decCents3;
        decimal decCents4;
        decimal decGrossIncome;
        decimal decIncomeTax;

        #endregion

        #region Public Methods

        /// <summary>
        /// Called from ProcessCsv method of the Employee Controller 
        /// in response to a CSV bulk upload request
        /// </summary>
        /// <param name="empList">object having the list of employee data 
        /// contained in the CSV input file>/param>
        /// <returns>Updated employee list object to be passed to the View</returns>
        public List<Employee> GeneratePaySlips(List<Employee> empList)
        {
            List<Employee> employeeList = new List<Employee>();

            try
            {

                //Process each item in the employee list for calculating the salary details 
                foreach (Employee item in empList)
                {
                    InitVariables(item);

                    employeeList.Add(CalculateSalaryDetails(item)[0]);
                    //employeeList will contain the updated salary details of each employee

                }

                return employeeList;

            }
            catch (Exception ex)
            {
                employeeList = null;
                return employeeList;
            }

        }

        /// <summary>
        /// Called from Index method of the Employee Controller 
        /// in response to a POST request (click of "GeneratePaySlip" button)
        /// </summary>
        /// <param name="empList">object having the an employee data 
        /// from the Data entry screen>/param>
        /// <returns>Updated employee object to be passed to the View</returns>
 
        //Generate a single Payslip
        public List<Employee> GeneratePaySlip(Employee empData)
        {
            List<Employee> empList = new List<Employee>();

            try
            {
                //Validate if Annual salary & Super Rate are positive integers and Super Rate is not greater thn 50
                //This is added in case certain browsers doesn't support the required client side validators  
                if ((empData.AnnualSalary < 0) || (empData.SuperRate < 0) || (empData.SuperRate > 50))
                    throw new FormatException("Enter valid Employee information");

                InitVariables(empData);

                empList = CalculateSalaryDetails(empData);

                return empList;
            }
            catch (Exception ex)
            {

                empList = null;
                return empList;
            }

        }
        #endregion

        #region Private Methods
        //Initialise Payslip variables
        private void InitVariables(Employee empData)
        {
            decAnnualSalary = empData.AnnualSalary;
            decSuperRate = empData.SuperRate;
            decExcess = 0;
            decIncomeTax = 0;

            decCents = 0.19M;
            decCents2 = 0.325M;
            decCents3 = 0.37M;
            decCents4 = 0.45M;

            decGrossIncome = (decAnnualSalary / 12);
            decSuperRate = decSuperRate / 100;

        }

        //Recieves an employee object. Returns updated employee object(s) (payslip attributes )
        //(Not Done yet!!) This logic needs to be moved to an XML file so that user can setup the values dynamically
        private List<Employee> CalculateSalaryDetails(Employee emp)
        {
            List<Employee> empList = new List<Employee>();

            if ((decAnnualSalary >= 0) && (decAnnualSalary <= 18200))
            {
                decIncomeTax = 0;
            }
            else if ((decAnnualSalary >= 18201) && (decAnnualSalary < 37000))
            {
                decExcess = decAnnualSalary - 18200;
                decIncomeTax = ((decCents) * (decExcess));
            }
            else if ((decAnnualSalary >= 37001) && (decAnnualSalary < 80000))
            {
                decExcess = decAnnualSalary - 37000;
                decIncomeTax = 3572 + ((decCents2) * (decExcess));
            }
            else if ((decAnnualSalary >= 80001) && (decAnnualSalary < 180000))
            {
                decExcess = decAnnualSalary - 80000;
                decIncomeTax = 17547 + ((decCents3) * (decExcess));
            }
            else if (decAnnualSalary >= 180001)
            {
                decExcess = decAnnualSalary - 180000;
                decIncomeTax = 54547 + ((decCents4) * (decExcess));
            }
            decIncomeTax = decIncomeTax / 12;

            emp.GrossIncome = Convert.ToInt32(decGrossIncome);
            emp.IncomeTax = Convert.ToInt32(decIncomeTax);
            emp.Super = Convert.ToInt32(decGrossIncome * decSuperRate);
            emp.NetIncome = Convert.ToInt32(decGrossIncome) - Convert.ToInt32(decIncomeTax);
            empList.Add(emp);

            return empList;

        }

        #endregion
    }
}
    