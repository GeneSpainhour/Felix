using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoMapper;
using Felix.Interfaces;
using Felix.Library.BLL;
using Felix.Data;
using Felix.Library.Technicals;
using Felix.Library.Managers;
using System.Reactive;
using Felix.Library.Analyzers;
using System.Diagnostics;

namespace FelixTests
{
    [TestClass]
    public class FelixWaveBuilderTests
    {
        private List<IBar> bars = BarData.Bars("YMM18", DateTime.Parse("2018-03-27"), DateTime.Parse("2018-03-28"))
                .ToList();
        [TestMethod]
        public void BarDataTest()
        {
            List<IBar> bars = BarData.Bars("YMM18", DateTime.Parse("2018-03-27"), DateTime.Parse("2018-03-28"))
                .ToList();

            Assert.IsTrue(bars.Count == 272);

            List<IBarDatum> barData = bars.ToBarDatum(1, 4).ToList();

            Assert.IsTrue(barData.Count == 272);
        }

        //[TestMethod]
        //public 
    }
}
