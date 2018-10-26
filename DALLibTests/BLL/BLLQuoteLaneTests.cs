using Microsoft.VisualStudio.TestTools.UnitTesting;
using DALLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace DALLib.Tests
{
    [TestClass()]
    public class BLLQuoteLaneTests
    {
        private Mock<IDALQuoteLane> mockIDALQuoteLane { get; set; }

        [TestInitialize]
        public void TestInit()
        {
            mockIDALQuoteLane = new Mock<IDALQuoteLane>();
        }

        [TestMethod()]
        public void GetAllTest()
        {
#if DEBUG
            IDALQuoteLane dalQuoteLane = new DALQuoteLane();
            IBLLQuoteLane _bllQuoteLane = new BLLQuoteLane(dalQuoteLane);
#else
            IBLLQuoteLane _bllQuoteLane = new BLLQuoteLane(mockIDALQuoteLane.Object);
            mockIDALQuoteLane.Setup(c => c.GetAll()).Returns(new List<Customers>());            
#endif
            var result = _bllQuoteLane.GetAll();
            Assert.IsNotNull(result);
            Assert.AreNotEqual(0, result.Count);
        }
    }
}