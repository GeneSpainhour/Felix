﻿using System;
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
using Felix.Library.Technicals.Indicators;

namespace FelixTests
{
    [TestClass]
    public class IndicatorTests
    {
        private List<IBar> bars = BarData.Bars("YMM18", DateTime.Parse("2018-03-27"), DateTime.Parse("2018-03-28"))
        .ToList();

        [TestMethod]
        public void InstaTrendTest()
        {
            double alpha = 0.5;

            List<IInstaTrendValue> values = bars.InstaTrend(alpha).ToList();

            Assert.IsTrue(true);

            var result = ( from b in bars
                           join v in values on b.Time equals v.Time
                           select (b, v) ).ToList();

            string fileSpec = @"E:\Felix\FelixTests\Data\InstaTrendValues.txt";

            CDSWriter.Write(fileSpec, result);
        }
    }
}
