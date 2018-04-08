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
using System.Reactive.Linq;
using System.Reactive.Concurrency;
using System.Threading;

namespace FelixTests
{
    [TestClass]
    public class ManagerTests
    {
        private List<IBar> upTrendMocks = BarMocks.Uptrend.ToList();
        private List<MetaMapping> mappings = MarketDataMock.MetaMappings.ToList();


        private List<IMove> moves = new List<IMove>();

        private List<IBarDatum> barDatumEntries = new List<IBarDatum>();

        private IMoveManager moveManager = new MoveManager();

        [TestMethod]
        public void BarStreamTest()
        {
            IBarDatum datum = CreateFirstBar();

            barDatumEntries.Add(datum);

            Assert.IsTrue(barDatumEntries.Count == 1);

            moveManager.Insert(datum);

            moveManager.BarStream.Subscribe(b => BarIsValid(b));
        }

        [TestMethod]
        public void MoveStreamTest()
        {
            moveManager.MoveStream.Subscribe(m => ValidateMoveStream(m));

            foreach ( IBar bar in upTrendMocks)
            {
                IBarDatum datum = CreateBar(bar);

                barDatumEntries.Add(datum);

                moveManager.Insert(datum);
            }

            Assert.IsTrue(moves.Count > 0);
        }


        [TestMethod]
        public void BarAnalyzerInitialTest()
        {
            IBar bar = upTrendMocks.First();

            IBarDatum datum = CreateBar(bar);

            BarAnalyzerResult result = BarAnalyzer.Analyze(null, datum, Felix.Data.Trend.TrendType.Unknown);

            Assert.IsNotNull(result);

            Assert.IsTrue(result.Trend == Felix.Data.Trend.TrendType.Up);

            Assert.IsTrue(result.WorkingBar == datum);

            Assert.IsTrue(result.IsValidTrendBar == true);
           
        }

        [TestMethod]
        public void BarAnalyzerTest()
        {
            List<IBar> bars = upTrendMocks.Take(5).ToList();

            IBarDatum workingBar = null;

            foreach (IBar bar in bars)
            {
                IBarDatum datum = CreateBar(bar);

                BarAnalyzerResult result = BarAnalyzer.Analyze(workingBar, datum, Felix.Data.Trend.TrendType.Unknown);

                VerifyBarAnalyzerResult(result, datum, true, Felix.Data.Trend.TrendType.Up);

                workingBar = result.WorkingBar;
            }
        }

        [TestMethod]
        public void BarAnalyzerBreakTest()
        {
            List<IBar> bars = upTrendMocks.ToList();

            IBarDatum workingBar = null;

            Felix.Data.Trend.TrendType currentTrend = Felix.Data.Trend.TrendType.Unknown;

            for (int i =0, c = bars.Count; i<c; ++i)
            {
                IBarDatum datum = CreateBar(bars[i]);

                BarAnalyzerResult result = BarAnalyzer.Analyze(workingBar, datum, currentTrend);

                Felix.Data.Trend.TrendType expectedType = GetExpectedTrendType(datum.Index);

                IBarDatum expectedBar = datum.Index != 25 ? datum : workingBar;

                VerifyBarAnalyzerResult(result, expectedBar, datum.Index != 25, expectedType);

                workingBar = result.WorkingBar;

                currentTrend = result.Trend;
            }
        }

        [TestMethod]
        public void BarAnalyzerBreaksWithData()
        {
            // get the marketId from the market with this symbol
            string marketSymbol = "YM";

            int momentumPeriod = 9;

            Felix.Library.BLL.Market market = new Felix.Library.BLL.Market(marketSymbol);
            
            List<IBar> bars = BarData.Bars("YMM18", DateTime.Parse("2018-03-27"), DateTime.Parse("2018-03-28"))
                .ToList();
            
            List<IBarDatum> barData = bars.ToBarDatum(market.MarketId, momentumPeriod).ToList();

            // subscribe to MoveStream and just write them to the debug
            moveManager.MoveStream
                .SubscribeOn(Scheduler.NewThread)
                .Subscribe(m => OnMove(m), err => ReportError(err));

            foreach (var data in barData)
            {
                moveManager.Insert(data);
            }

            Thread.Sleep(5000);

            Debug.WriteLine($"found {moves.Count} moves");
            
        }

        private void OnMove (IMove move)
        {
            moves.Add(move);

            TestUtils.Report(move);
        }

        private void ReportError(Exception ex)
        {
            Debug.WriteLine($"Exception thrown: {ex.Message}\nStackTrace: {ex.StackTrace}");
        }

        private Felix.Data.Trend.TrendType GetExpectedTrendType(int ndx)
        {
            Felix.Data.Trend.TrendType retType = ndx < 20 || ndx > 25 ? Felix.Data.Trend.TrendType.Up : Felix.Data.Trend.TrendType.Down;

            return retType;
        }

        private void VerifyBarAnalyzerResult(BarAnalyzerResult result, IBarDatum expectedBar, bool expectedValidity, Felix.Data.Trend.TrendType expectedTrend)
        {
            Assert.IsNotNull(result);

            Assert.IsTrue(result.Trend == expectedTrend);

            Assert.IsTrue(result.WorkingBar == expectedBar);

            Assert.IsTrue(result.IsValidTrendBar == expectedValidity);
        }

        private void ValidateMoveStream ( IMove move)
        {
            Assert.IsNotNull(move);

            moves.Add(move);
        }

        private void BarIsValid (IBarDatum datum)
        {
            Assert.IsNotNull(datum);

            Assert.IsInstanceOfType(datum, typeof(BarDatum));
        }

        private IEnumerable<IMetaMapping> AverageMappings
        {
            get
            {
                var dataMappings = mappings.Where(m => m.Property.Contains("A")).ToList();

                return dataMappings.Cast<IMetaMapping>();
            }
        }

        private IBarDatum CreateFirstBar()
        {
            int momentumPeriod = 9;

            var aveMappings = AverageMappings;

            IBar bar = upTrendMocks[0];

            BarDatumBuilder bldr = new BarDatumBuilder(bar, aveMappings);

            bldr.Build(momentumPeriod, barDatumEntries);

            return bldr.Datum;
        }

        private IBarDatum CreateBar (IBar bar)
        {
            int momentumPeriod = 9;

            var aveMappings = AverageMappings;

            BarDatumBuilder bldr = new BarDatumBuilder(bar, aveMappings);

            bldr.Build(momentumPeriod, barDatumEntries);

            return bldr.Datum;
        }
    }
}
