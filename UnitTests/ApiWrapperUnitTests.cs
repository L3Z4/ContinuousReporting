using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VSO.RestAPI;
using VSO.RestAPI.Model;
using VSO.RestAPI.ServiceHooks.HttpModel;

namespace ContinuousReporting.UnitTests
{
    [TestClass]
    public class ApiWrapperUnitTests
    {
        [TestMethod]
        public void ApiWrapper_ReturnAllProjectWithCoverage_ExepctUnitTests()
        {
            var dataSource = new BuildDetails
            {
                information = new[]
                {
                    new Information
                    {
                        coverageData = new Coveragedata
                        {
                            modules = new[]
                            {
                                new Module {blocksCovered = 1, blocksNotCovered = 1, name = "newBlock"},
                                new Module {blocksCovered = 1, blocksNotCovered = 1, name = "newBlockUnitTest"},
                            }
                        }
                    }
                }
            };

            var result = ApiWrapper.ComputeCoverage(dataSource);


            Assert.AreEqual(1, result.Length);
        }
    }
}
