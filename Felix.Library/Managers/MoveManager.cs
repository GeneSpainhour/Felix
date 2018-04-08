using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using Felix.Interfaces;
using Felix.Library.Analyzers;
using Felix.Data;


namespace Felix.Library.Managers
{
    public interface IMoveManager
    {
        IEnumerable<IBarDatum> Bars { get; }
        IEnumerable<IMove> Moves { get; }

        IEnumerable<IBarDatum> Highs { get; }

        IEnumerable<IBarDatum> Lows { get; }

        IObservable<IMove> MoveStream { get; }
        IObservable<IBarDatum> BarStream { get; }

        IMove PreviousMove { get; }

        void Insert(IBarDatum datum);


    }
    public class MoveManager : IMoveManager
    {
        private List<IBarDatum> DataList { get; set; } = new List<IBarDatum>();
        private List<IMove> MoveList { get; set; } = new List<IMove>();

        private List<IBarDatum> HighList { get; set; } = new List<IBarDatum>();

        private List<IBarDatum> LowList { get; set; } = new List<IBarDatum>();

        private Trend.TrendType CurrentTrendType = Trend.TrendType.Unknown;

        private IBarDatum WorkingBar { get; set; }

        public IMove CurrentMove
        {
            get
            {
                IMove move = null;

                if (MoveList.Any())
                {
                    move = MoveList.Last();
                }
                return move;
            }
        }

        public IMove PreviousMove
        {
            get
            {
                IMove retMove = null;

                if (MoveList.Count > 1)
                {
                    retMove = MoveList[MoveList.Count - 2];
                }

                return retMove;
            }
        }

        public IEnumerable<IBarDatum> Bars => DataList;

        public IEnumerable<IMove> Moves => MoveList;


        public IEnumerable<IBarDatum> Highs => HighList;

        public IEnumerable<IBarDatum> Lows => LowList;

        private Subject<IBarDatum> DataListSubject;

        private Subject<IMove> MoveSubject;

        public MoveManager()
        {
            DataListSubject = new Subject<IBarDatum>();

            MoveSubject = new Subject<IMove>();

            WorkingBar = null;
        }

        public IObservable<IMove> MoveStream => MoveSubject.AsObservable();

        public IObservable<IBarDatum> BarStream => DataListSubject.AsObservable();

        public void Insert (IBarDatum bd)
        {
            InsertDataList(bd);

            BarAnalyzerResult result = BarAnalyzer.Analyze(WorkingBar, bd, CurrentTrendType);

            Update(result);
        }

        private void InsertDataList (IBarDatum bd)
        {
            
            DataList.Add(bd);

            DataListSubject.OnNext(bd);
        }

        private void Update(BarAnalyzerResult result)
        {
           
            // set WorkingBar to result.WorkingBar if CurrentTrend is Unknown or result.IsValidTrendBar is true
           // if (!HighList.Any() && !LowList.Any())
           if (CurrentTrendType == Trend.TrendType.Unknown)
            {
                if (result.Trend == Trend.TrendType.Up)
                {
                    LowList.Add(result.WorkingBar);
                }
                else if (result.Trend == Trend.TrendType.Down)
                {
                    HighList.Add(result.WorkingBar);
                }

                CurrentTrendType = result.Trend;

                WorkingBar = result.WorkingBar;

                MoveList.Add(Move.Open(MoveList.Count, result.WorkingBar, (int)result.Trend));

            }
            else
            {
                if (result.IsValidTrendBar)
                {
                    WorkingBar = result.WorkingBar;
                    // currentTrend: undetermined: currentTrend = trend
                    // currentTrend: Up: if( trend.Down) changecurrentTrend to down and add bar Highest high series I {Working ... barDatum}
                    // currentTrend: Down: if (trend.up) changcurrentTrend to up and add bar with Lowest low in series {WorkingBar .. barDatum}
                    switch (CurrentTrendType)
                    {
                        case Trend.TrendType.Up:
                        {
                            if (result.Trend == Trend.TrendType.Down)
                            {
                                TransitionToDownFromUp(result);
                            }
                        }
                        break;
                        case Trend.TrendType.Down:
                        {
                            if (result.Trend == Trend.TrendType.Up)
                            {
                                TransitionToUpFromDown(result);
                            }
                        }

                        break;
                    }
                }
            }
        }

        private void Transition (BarAnalyzerResult result, IBarDatum datum)
        {
            IMove prevMove = CurrentMove.Close(datum);

            var newMove = Move.Open(MoveList.Count, datum, (int)result.Trend);

            MoveList.Add(newMove);

            CurrentTrendType = result.Trend;

            MoveSubject.OnNext(prevMove);
        }

        private void TransitionToDownFromUp(BarAnalyzerResult result)
        {
            // get lowest low
            IBarDatum highest = DataList
                .Where(e => e.Index >= WorkingBar.Index && e.Index <= result.WorkingBar.Index)
                .OrderByDescending(e => e.High)
                .First();
            HighList.Add(highest);

            Transition(result, highest);
        }

        private void TransitionToUpFromDown(BarAnalyzerResult result)
        {
            // Get Highest high
            IBarDatum lowest = DataList
                               .Where(e => e.Index >= WorkingBar.Index && e.Index <= result.WorkingBar.Index)
                               .OrderBy(e => e.Low)
                               .First();
            LowList.Add(lowest);

            Transition(result, lowest);
        }
    }
}
