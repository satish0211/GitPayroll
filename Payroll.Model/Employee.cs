using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Model class containing the Employee attributes and the required validations
/// </summary>

namespace Payroll.Model
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }

        [DisplayName("First Name")]
        [Required(ErrorMessage = "Please enter the First Name")]
        [StringLength(50, ErrorMessage = "First Name should be less than 50 characters")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Please enter the Last Name")]
        [StringLength(50, ErrorMessage = "Last Name should be less than 50 characters")]
        public string LastName { get; set; }

        [DisplayName("Annual Salary")]
        [Required(ErrorMessage = "Please enter the Annual Salary")]
        [Range(0, 1000000000,
            ErrorMessage = "Annual Salary must be between 0 and 1000000000")]
        [DataType(DataType.Currency)]
        public decimal AnnualSalary { get; set; }


        [DisplayName("Super Rate")]
        [Required(ErrorMessage = "Please enter Super Rate")]
        [Range(0, 50,
            ErrorMessage = "Super Rate must be between 0 and 50")]
        public int SuperRate { get; set; }

        [DisplayName("Payment Start Date")]
        [DataType(DataType.DateTime, ErrorMessage = "Please enter a valid date")]
        [Required(ErrorMessage = "Please enter a value for Payment Start Date")]

        public string PaymentStartDate { get; set; }

        public int GrossIncome { get; set; }

        public int IncomeTax { get; set; }

        public int Super { get; set; }

        public int NetIncome { get; set; }
    }



}
