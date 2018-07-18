using Microsoft.VisualStudio.TestTools.UnitTesting;
using CampingApp.DataAccessLayer;
using System;
using System.IO;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampingApp.DataAccessLayer.Tests
{
    [TestClass()]
    public class DataAccessTests
    {
        string inputFilePath = Path.GetFullPath(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName
         + @"\Input\Input.txt");

        [TestMethod()]
        public void ZeroEndOfFileTest()
        {
            var dataObj = new DataAccess();
            
            int lastLine = dataObj.GetLastLine(inputFilePath);

            Assert.IsTrue(lastLine != 0, "The end of file value is not equal to 0");

        }

        [TestMethod()]
        public void EmptyEndOfFileTest()
        {
            var dataObj = new DataAccess();       
            try
            {
                int lastLine = dataObj.GetLastLine(inputFilePath);
            }
            catch (Exception ex)
            {
                Assert.Fail(string.Format("Unexpected exception of type {0} caught: {1}",
                            ex.GetType(), ex.Message));
            }
        }
    }

}