using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using Felix.Interfaces;
using Felix.Data;
using Felix.Library.Managers;

namespace Felix.Library.Analyzers
{
    public interface IMoveAnalyzer
    {
        IObservable<IMoveAnalyzerResult> ResultStream { get; }
    }
    public class MoveAnalyzer: IMoveAnalyzer
    {
        private Subject<IMoveAnalyzerResult> ResultSubject;
        private IMoveManager MoveMgr { get; set; }

        public IObservable<IMoveAnalyzerResult> ResultStream
        {
            get { return ResultSubject.AsObservable(); }
        }

        public MoveAnalyzer(IMoveManager mm)
        {
            ResultSubject = new Subject<IMoveAnalyzerResult>();

            MoveMgr = mm;

            MoveMgr.MoveStream.Subscribe(m => OnPriceBreak(m));
        }

        private void OnPriceBreak(IMove newMove)
        {
            IMoveAnalyzerStrategy strategy = GetAnalyzerStrategy(newMove);

            if (strategy == null)
            {
                Debugger.Break();
            }

            var result = strategy.Execute(MoveMgr, newMove);

            ResultSubject.OnNext(result);
        }

        IMoveAnalyzerStrategy GetAnalyzerStrategy(IMove move)
        {
            IMoveAnalyzerStrategy retStrategy = null;

            switch (move.TrendType)
            {
                case (int)Trend.TrendType.Up:
                    retStrategy = new LongMoveAnalyzerStrategy();
                    break;
                case (int)Trend.TrendType.Down:
                    retStrategy = new ShortMoveAnalyzerStrategy();
                    break;
            }

            return retStrategy;
        }

    }
}
