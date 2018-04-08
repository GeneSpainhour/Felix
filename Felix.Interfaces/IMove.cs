using System;
using System.Collections.Generic;

namespace Felix.Interfaces
{
    public interface IMove
    {
        double Degrees { get; }
        IBarDatum End { get; set; }
        int EndBarId { get; }
        int EndIndex { get; }
        DateTime EndTime { get; }
        int MoveId { get; set; }
        double Range { get; set; }
        IBarDatum Start { get; set; }
        int StartBarId { get;  }
        int StartIndex { get; }
        DateTime StartTime { get; }
       
        int TrendType { get; set; } // 1-Unkown, 2-Up, 3-Down

        IMove Close(IBarDatum data);
        //IMove Open(int id, IBarDatum data, int trendType);
    }
}