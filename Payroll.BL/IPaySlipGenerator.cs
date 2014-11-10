using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Payroll.Model;

/// <summary>
/// Interface that defines the methods which any PaySlipGenerator class should implement
/// </summary>
 
namespace Payroll.BL
{
    public interface IPaySlipGenerator
    {
        List<Employee> GeneratePaySlips(List<Employee> empList);
        List<Employee> GeneratePaySlip(Employee empData);

    }
}
