using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Felix.Interfaces;

namespace Felix.Data
{
    public class Move : IMove
    {
        public int MoveId { get; set; }
        public int StartBarId => Start.BarDataId;
        public int EndBarId {
            get
            {
                return End == null ? 0 : End.BarDataId;
            }
        }
        public IBarDatum Start { get; set; }

        public IBarDatum End { get; set; }

        public int TrendType { get; set; }
    
        public double Range { get; set; }

        public DateTime StartTime
        {
            get
            {
                return Start.Time;
            }
        }

        public DateTime EndTime
        {
            get
            {
                return End != null ? End.Time : DateTime.MinValue;
            }
        }

        public int StartIndex
        {
            get
            {
                return Start.Index;
            }
        }

        public int EndIndex
        {
            get
            {
                return End != null ? End.Index : int.MinValue;
            }
        }

        public double Degrees
        {
            get
            {
                if (Range == double.MinValue)
                {
                    throw new Exception($"Degrees called before closed");
                }

                return  (Math.Sqrt(Range) * 180) % 360;
            }
        }

        public static IMove Open (int id, IBarDatum data, int trendType)
        {
            var move = new Move { MoveId = id, Start = data, TrendType = trendType };

            return move;
        }

        public IMove Close( IBarDatum data)
        {
            End = data;

            if (TrendType == (int)Trend.TrendType.Down)
            {
                Range = Start.High - End.Low;
            }
            else if (TrendType == (int)Trend.TrendType.Up)
            {
                Range = End.High - Start.Low;
            }

            return this;
        }

        

    }
}
