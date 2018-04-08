using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Felix.Interfaces;
using Felix.Library.BLL;
using Felix.Data;
using Felix.MarketData.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoMapper;

namespace FelixTests
{
    [TestClass]
    public class AutomapperTests
    {
        List<MetaMapping> mock = MarketDataMock.MetaMappings.ToList();

        [TestMethod]
        public void MapMetas ()
        {
            var metas = Mapper.Map<List<MetaMapping>, List<IMetaMapping>>(mock);

            Assert.IsTrue(metas.Count > 0);
        }
    }
    [TestClass]
    public class BarCreationTests
    {
        List<MetaMapping> mock = MarketDataMock.MetaMappings.ToList();

        [TestMethod]
        public void CreateBarTest()
        {
            IBar newBar = new Felix.Data.Bar(1, 1, 5, DateTime.Parse("2018-04-15"), 100.00, 20.00, 20.00, 100.00, 100);

            Felix.MarketData.Bar dataBar = Mapper.Map<IBar, Felix.MarketData.Bar>(newBar);

            var repo = new MarketRepository();

            repo.AddBar(dataBar);

            Assert.IsTrue(dataBar.BarId > 0);
        }

        [TestMethod]
        public void CreateMultipleBarTest ()
        {
            var mockedBars = BarDataDoc.MockedBars.ToList();

            Felix.Library.BLL.BarDomainObject domainBar = new Felix.Library.BLL.BarDomainObject();

            foreach (IBar bar in mockedBars)
            {
                int barId = domainBar.Save(bar);

                Assert.IsTrue(barId > 0 && bar.BarId == barId);
            }
        }
    }


    [TestClass]
    public class BarDataExtentionsTests
    {
        
        [TestMethod]
        public void CreateBarDatum()
        {
            BarDataMocks mocks = new BarDataMocks();

            List<MetaMapping> averageMappings = MarketDataMock.MetaMappings.Where (m => m.Property.Contains("A")).ToList();

            int momentumPeriod = averageMappings.FirstOrDefault(m => m.Property == "A1").Value.Value;

            List<IBarDatum> barDatumList = new List<IBarDatum>();

            int i = 0;

            foreach( IBar bar in mocks.Bars)
            {

                BarDatumBuilder bldr = new BarDatumBuilder(bar, averageMappings);

                bldr.Build(momentumPeriod, barDatumList);

                barDatumList.Add(bldr.Datum);

                ++i;
            }

            Assert.IsTrue(barDatumList.Count == mocks.Bars.Count);
        }
    }

    [TestClass]
    public class DomainObjectTests
    {
        private static List<PropertyInfo> ContractProperties = typeof(IContract).GetProperties().ToList();

        private static List<PropertyInfo> MetaProperties = typeof(IMetaMapping).GetProperties().ToList();

        [TestMethod]
        public void MarketTest()
        {
            var repo = new MarketRepository();

            string expectedName = "DowJones";

            string expectedExchange = "CBOT";

            decimal expectedTickSize = (decimal)5.00;

            string expectedMonths = "Mar, Jun, Sep, Dec";

            var mockContracts = MarketDataMock.Contracts;

            var mockMetaMappings = MarketDataMock.MetaMappings;

            Felix.Library.BLL.Market market = new Felix.Library.BLL.Market( "YM");

            Assert.IsTrue(market.Name.Equals(expectedName));
            Assert.IsTrue(market.Exchange.Equals(expectedExchange));
            Assert.IsTrue(market.TickSize == expectedTickSize);
            Assert.IsTrue(market.Months.Equals(expectedMonths));

            foreach (var contract in market.Contracts)
            {
                var testContract = mockContracts.FirstOrDefault(c => c.ContractId == contract.ContractId);

                Assert.IsTrue(TheseContractsMatch(contract, testContract));
            }

            foreach (var meta in market.MetaMappings)
            {
                var testMeta = mockMetaMappings.FirstOrDefault(c => c.MetaMappingId == meta.MetaMappingId);

                if (testMeta == null)
                {
                    Debugger.Break();
                }

                Assert.IsTrue(TheseMetaMappingsMatch(meta, testMeta));
            }

        }


        [TestMethod]
        public void ContractTest()
        {
            var repo = new MarketRepository();

            string expectedName = "YMH18";

            string expectedSymbol = "YMH18";

            DateTime expectedBeginDate = DateTime.Parse("2017-03-17");

            DateTime expectedEndDate = DateTime.Parse("2018-03-16");

            int exprectedContractId = 1;

            int exprectedMarketId = 1;

            var mockContracts = MarketDataMock.Contracts;

            Felix.Library.BLL.Contract contract = new Felix.Library.BLL.Contract( expectedSymbol);
            Assert.IsTrue(contract.Name.Equals(expectedName));
            Assert.IsTrue(contract.Symbol.Equals(expectedSymbol));
            Assert.IsTrue(contract.BeginDate == expectedBeginDate);
            Assert.IsTrue(contract.EndDate==expectedEndDate);
            Assert.IsTrue(contract.ContractId == exprectedContractId);
            Assert.IsTrue(contract.MarketId == exprectedMarketId);


        }

        private bool TheseContractsMatch(IContract real, IContract mock)
        {
            bool bMatch = true;

            foreach ( var prop in ContractProperties)
            {
                bMatch = ObjectEvaluator.IsEqual( prop, prop.GetValue(real, null) , prop.GetValue(mock, null));

                Debug.WriteLine($"Contracts: Property:{prop.Name}, Result: {bMatch}");
            }

            return bMatch;
        }

        private bool TheseMetaMappingsMatch(IMetaMapping real, IMetaMapping mock)
        {
            bool bMatch = true;

            foreach (var prop in MetaProperties)
            {
                object oReal = prop.GetValue(real, null);

                object oMeta = prop.GetValue(mock, null);

                if (oReal == null && oMeta != null || oReal!= null && oMeta == null)
                {
                    Debugger.Break();
                }

                bMatch = ObjectEvaluator.IsEqual(prop, prop.GetValue(real, null), prop.GetValue(mock, null));

                Debug.WriteLine($"Mappings: Property:{prop.Name}, Result: {bMatch}");
            }

            return bMatch;
        }
    }


}
