using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Felix.Interfaces;
using Felix.Data;
using Felix.Library.Managers;

namespace Felix.Library.Analyzers
{
    public interface IMoveAnalyzerStrategy
    {
        IMoveAnalyzerResult Execute(IMoveManager moveMgr, IMove newMove);
    }

    public class MoveAnalyzerStrategyBase
    {
        protected List<double> AFromBar (IBarDatum data)
        {
            if (data == null)
            {
                Debugger.Break();
            }
            var irs = new List<double>()
            {
                data.A0,
                data.A1,
                data.A2,
                data.A3,
                data.A4
            };

            return irs;
        }
    }
    public class ShortMoveAnalyzerStrategy : MoveAnalyzerStrategyBase, IMoveAnalyzerStrategy
    {
        public IMoveAnalyzerResult Execute(IMoveManager moveMgr, IMove newMove)
        {
            IBarDatum barData = newMove.Start;

            List<ProspectiveIR> gamma = GammaAnalyzer.GetValues(false, barData); //gamma

            var irList = moveMgr.Highs
             .Where(b => b.High < barData.Low)
             .Select(h => h.High)
             .Distinct()
             .OrderBy(v => v)
             .ToList();

            var irLowPoints = moveMgr.Lows
                .Where(b => b.Low < barData.Low)
                .Select(l => l.Low)
                .Distinct()
                .OrderBy(v => v)
                .ToList();


            irList.AddRange(irLowPoints.Where(v => !irList.Contains(v)).ToList());

            var tempList = new List<double>();
            if (barData.Low > moveMgr.PreviousMove.Start.High)
            {
                List<double> APrevious = AFromBar(moveMgr.PreviousMove.Start);

                foreach (var a in APrevious)
                {
                    var possibles = irList.Where(v => Math.Abs(a - v) < 5).ToList();

                    tempList.AddRange(possibles);
                }
            }
            else
            {
                tempList = irList;
            }

            List<ProspectiveIR> resultList = new List<ProspectiveIR>();

            foreach (var g in gamma)
            {
                var possible = tempList.Where(t => Math.Abs(g.Price - t) < 5).ToList().Min();

                resultList.Add(new ProspectiveIR(g.Degrees, possible));
            }

            MoveAnalyzerResult result = new MoveAnalyzerResult(barData, resultList);

            return result;
        }
    }
    public class LongMoveAnalyzerStrategy: MoveAnalyzerStrategyBase, IMoveAnalyzerStrategy
    {
        public IMoveAnalyzerResult Execute (IMoveManager moveMgr, IMove newMove)
        {
            // this is for a long break:
            // if start is below A, then group A that matches a previous IR
            // otherwise group A from previous start. Previous start will the the high of preceding downtrend
            // lets see what this looks like without that wrinkle

            //            List<double> AFromStart = AFromBar(newMove.Start);

            IBarDatum barData = newMove.Start;

            List<ProspectiveIR> gamma = GammaAnalyzer.GetValues(true,barData); //gamma

            // now get union of Highs and lows from MoveMgr
            var irList = moveMgr.Highs
                .Where ( b => b.High > barData.High)
                .Select(h=>h.High)
                .Distinct()
                .OrderBy( v => v)
                .ToList();

            var irLowPoints = moveMgr.Lows
                .Where ( b => b.Low > barData.High )
                .Select ( l => l.Low)
                .Distinct()
                .OrderBy( v=> v)
                .ToList();

            irList.AddRange(irLowPoints.Where(v => !irList.Contains(v)).ToList());

            var tempList = new List<double>();

            if (barData.High < moveMgr.PreviousMove.Start.Low)
            {
                List<double> APrevious = AFromBar(moveMgr.PreviousMove.Start);

                foreach (var a in APrevious)
                {
                    var possibles = irList.Where(v => Math.Abs(a - v) < 5).ToList();

                    tempList.AddRange(possibles);
                }
            }
            else
            {
                tempList = irList;
            }

            List<ProspectiveIR> resultList = new List<ProspectiveIR>();

            foreach ( var g in gamma)
            {
                var possible = tempList.Where(t => Math.Abs(g.Price - t) < 5).ToList().Min();

                resultList.Add(new ProspectiveIR(g.Degrees, possible));
            }

            MoveAnalyzerResult result = new MoveAnalyzerResult(barData, resultList);

            return result;
        }
    }

}
