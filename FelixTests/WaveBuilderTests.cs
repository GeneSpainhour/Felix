using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Felix.Interfaces;
using Felix.Library.BLL;
using Felix.Data;
using Felix.Library.Managers;

namespace FelixTests
{
    [TestClass]
    public class WaveBuilderTests
    {
        private List<IMove> moves = new List<IMove>();

        private List<IBarDatum> bars = new List<IBarDatum>();

        private IMoveManager moveManager = new MoveManager();


        [TestMethod]
        public void DataCreationTests()
        {
            List<IBar> upTrendBars = BarMocks.UpMove(5, 100.00);

            List<IBarDatum> upTrendBarData = upTrendBars.ToBarDatum().ToList();

            var moveAlpha = Move.Open(moves.Count, upTrendBarData.First(), 2)
                .Close(upTrendBarData.Last());

            Assert.IsNotNull(moveAlpha);

            Assert.IsTrue(moveAlpha.TrendType == 2);

            IBarDatum startBar = upTrendBarData.First();

            IBarDatum endBar = upTrendBarData.Last();

            Assert.IsTrue(moveAlpha.Start == startBar);

            Assert.IsTrue(moveAlpha.End == endBar);

            Assert.IsTrue(moveAlpha.StartIndex == startBar.Index);

            Assert.IsTrue(moveAlpha.EndIndex == endBar.Index);

            double expectedRange = endBar.High - startBar.Low ;

            Assert.IsTrue(moveAlpha.Range == expectedRange);
        }

        [TestMethod]
        public void WaveTest()
        {
            InitializeMoves();

            IBarDatum lastTrendBar = moves.Last().End;

            List<IBar> testBar = BarMocks.DownMove(1, lastTrendBar.High - 5);

            List<IBarDatum> testDatum = testBar.ToBarDatum().ToList();

            bars.ForEach(b => moveManager.Insert(b));

            var ir = moveManager.Highs.Where(b => b.High < testDatum.First().Low)
                .Select(b => b.High).ToList();

            var lowBarQuery = moveManager.Lows.Where(b => b.Low < testDatum.First().Low)
                .Select(b => b.Low).ToList();

            ir.AddRange(lowBarQuery);

            Assert.IsTrue(ir.Count == 2);
        }

        private void InitializeMoves()
        {
            List<IBar> upTrendBars = BarMocks.UpMove(5, 100.00);

            List<IBarDatum> upTrendBarData = upTrendBars.ToBarDatum().ToList();

            bars.AddRange(upTrendBarData);

            var moveAlpha = Move.Open(moves.Count, upTrendBarData.First(), 2)
                .Close(upTrendBarData.Last());

            moves.Add(moveAlpha);

            IBarDatum last = moveAlpha.End;

            List<IBar> downTrend = BarMocks.DownMove(3, last.High - 10);

            List<IBarDatum> downData = downTrend.ToBarDatum().ToList();

            bars.AddRange(downData);

            var moveBeta = Move.Open(moves.Count, downData.First(), 3)
                .Close(downData.Last());

            moves.Add(moveBeta);

            last = moveBeta.End;

            upTrendBars = BarMocks.UpMove(5, last.Low + 10);

            upTrendBarData = upTrendBars.ToBarDatum().ToList();

            bars.AddRange(upTrendBarData);

            var moveGamma = Move.Open(moves.Count, upTrendBarData.First(), 2)
                .Close(upTrendBarData.Last());

            moves.Add(moveGamma);
        }

    }
}
