using Microsoft.VisualStudio.TestTools.UnitTesting;
using CampingApp.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampingApp.BusinessLayer.Tests
{
    [TestClass()]
    public class BusinessModelsTests
    {

        [TestMethod()]
        public void ComputeDebitOrCreditTest()
        {
            var computedList = BusinessModels.ComputeDebitOrCredit();
            var actualDecimal = computedList.Select(balance => balance.DebitOrCredit).ToList();
            var actual = actualDecimal.Select(d => Convert.ToDouble(d)).ToList();
            var expected = new List<double>();

            expected.Add(-1.99);
            expected.Add(-8.01);
            expected.Add(10.01);
            expected.Add(0.98);
            expected.Add(-0.98);


            CollectionAssert.AreEquivalent(expected, actual, "At least one pair of values do not match.");

        }
    }
}