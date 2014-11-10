using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payroll.Controllers;
using Payroll.BL;
using System.Web.Mvc;
using Payroll.Model;
using System.Collections.Generic;

namespace Payroll.Tests
{
    [TestClass]
    public class TestPaySlipGenerator
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

        private List<Employee> setEmployeeListAttributes_PositiveValues()
        {

            List<Employee> empList = new List<Employee>();

            Employee empObj1 = new Employee();
            empObj1.FirstName = "David";
            empObj1.LastName = "Rudd";
            empObj1.AnnualSalary = 60050;
            empObj1.PaymentStartDate = "01March to 31March";
            empObj1.SuperRate = 9;
            empList.Add(empObj1);

            Employee empObj2 = new Employee();
            empObj2.FirstName = "Ryan";
            empObj2.LastName = "Chen";
            empObj2.AnnualSalary = 120000;
            empObj2.PaymentStartDate = "01March to 31March";
            empObj2.SuperRate = 10;
            empList.Add(empObj2);

            return empList;
        }
        private Employee setEmployeeAttributes_NegativeAnnualSalary()
        {
            Employee empObj = new Employee();
            empObj.FirstName = "David";
            empObj.LastName = "Rudd";
            empObj.AnnualSalary = -60050;
            empObj.PaymentStartDate = "01March to 31March";
            empObj.SuperRate = 9;
            return empObj;
        }

        private Employee setEmployeeAttributes_NegativeSuperRate()
        {
            Employee empObj = new Employee();
            empObj.FirstName = "David";
            empObj.LastName = "Rudd";
            empObj.AnnualSalary = 60050;
            empObj.PaymentStartDate = "01March to 31March";
            empObj.SuperRate = -1;
            return empObj;
        }
        private Employee setEmployeeAttributes_SuperRate_GreaterThan_50()
        {
            Employee empObj = new Employee();
            empObj.FirstName = "David";
            empObj.LastName = "Rudd";
            empObj.AnnualSalary = 60050;
            empObj.PaymentStartDate = "01March to 31March";
            empObj.SuperRate = 51;
            return empObj;
        }
        [TestMethod]
        public void TestPaySlipGeneration()
        {
            //Arrange
            IPaySlipGenerator paySlip = getTestObject();
            Employee emp = setEmployeeAttributes_PositiveValues();

            //Act
            paySlip.GeneratePaySlip(emp);

            //Assert
            Assert.AreEqual(5004, emp.GrossIncome);
            Assert.AreEqual(922, emp.IncomeTax);
            Assert.AreEqual(4082, emp.NetIncome);
            Assert.AreEqual(450, emp.Super);

        }
        //setEmployeeListAttributes_PositiveValues
        [TestMethod]
        public void TestBulkPaySlipGeneration()
        {
            //Arrange
            IPaySlipGenerator paySlip = getTestObject();
            List<Employee> empList = setEmployeeListAttributes_PositiveValues();

            //Act
            paySlip.GeneratePaySlips(empList);

            //Assert
            Assert.AreEqual(5004, empList[0].GrossIncome);
            Assert.AreEqual(922, empList[0].IncomeTax);
            Assert.AreEqual(4082, empList[0].NetIncome);
            Assert.AreEqual(450, empList[0].Super);

            Assert.AreEqual(10000, empList[1].GrossIncome);
            Assert.AreEqual(2696, empList[1].IncomeTax);
            Assert.AreEqual(7304, empList[1].NetIncome);
            Assert.AreEqual(1000, empList[1].Super);

        }
        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestPaySlipGenerator_negativeAnnualSalary()
        {
            //Arrange
            IPaySlipGenerator paySlip = getTestObject();
            Employee emp = setEmployeeAttributes_NegativeAnnualSalary();

            //Act
            paySlip.GeneratePaySlip(emp);

        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestPaySlipGenerator_negativeSuperRate()
        {
            //Arrange
            IPaySlipGenerator paySlip = getTestObject();
            Employee emp = setEmployeeAttributes_NegativeSuperRate();

            //Act
            paySlip.GeneratePaySlip(emp);

        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestPaySlipGenerator_SuperRate_GreaterThan_50()
        {
            //Arrange
            IPaySlipGenerator paySlip = getTestObject();
            Employee emp = setEmployeeAttributes_SuperRate_GreaterThan_50();

            //Act
            paySlip.GeneratePaySlip(emp);

        }
    }
}
