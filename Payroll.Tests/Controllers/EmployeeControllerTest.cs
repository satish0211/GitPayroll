using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payroll.Controllers;
using Payroll.BL;
using System.Web.Mvc;
using Payroll.Model;
using Payroll.Models;
using System.Collections.Generic;


namespace Payroll.Tests.Controllers
{
    [TestClass]
    public class EmployeeControllerTest
    {
        private IPaySlipGenerator getTestObject()
        {
            return new PaySlipGenerator();
        }

        private Employee setEmployeeAttributes_PositiveValues()
        {
            Employee empObj = new Employee();
            empObj.FirstName = "David";
            empObj.LastName = "Rudd";
            empObj.AnnualSalary = 60050;
            empObj.PaymentStartDate = "01March to 31March";
            empObj.SuperRate = 9;
            return empObj;
        }
        [TestMethod]
        public void TestEmployeeIndexView()
        {
            //Arrange
            IPaySlipGenerator paySlip = getTestObject();
            Employee emp = setEmployeeAttributes_PositiveValues();

            //Act
            EmployeeController controller = new EmployeeController(paySlip);
            var result = controller.Index() as ViewResult;

            //Assert
            Assert.AreEqual("Index", result.ViewName);

        }
        [TestMethod]
        public void TestEmployeePaySlipView()
        {
            //Arrange
            IPaySlipGenerator paySlip = getTestObject();
            Employee emp = setEmployeeAttributes_PositiveValues();

            //Act
            EmployeeController controller = new EmployeeController(paySlip);
            var result = controller.Index(emp) as ViewResult;

            //Assert
            Assert.AreEqual("EmployeePaySlip", result.ViewName);

        }

        [TestMethod]
        public void TestEmployee_UploadCSVFile()
        {
            //Arrange
            IPaySlipGenerator paySlip = getTestObject();
            Employee emp = setEmployeeAttributes_PositiveValues();

            //Act
            EmployeeController controller = new EmployeeController(paySlip);
            var result = controller.UploadCSVFile() as ViewResult;

            //Assert
            Assert.AreEqual("UploadCSVFile", result.ViewName);

        }

    }
}
