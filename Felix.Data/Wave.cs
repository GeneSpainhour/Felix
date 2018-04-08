using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Felix.Interfaces;

namespace Felix.Data
{

    public class Wave : IWave
    {
        public int WaveId { get; set; }

        public int ParentWaveId { get; set; }
        public List<IWave> ChildWaves { get; } = new List<IWave>();
        public int Trend { get; set; } = (int) Data.Trend.TrendType.Unknown;

        public IBarDatum StartBar { get; set; }

        public IBarDatum EndBar { get; set; }

        public Wave() { }

        public Wave(int waveId, int parentWaveId, IMove initialMove)
        {
            WaveId = waveId;

            ParentWaveId = parentWaveId;

            StartBar = initialMove.Start;

            Trend = initialMove.TrendType;
        }

        public IWave AddChild (IWave childWave)
        {
            childWave.ParentWaveId = WaveId;

            ChildWaves.Add(childWave);

            return this;
        }

        public IWave Close(IMove endingMove)
        {
            EndBar = endingMove.End;

            return this;
        }

        private double characteristic = double.MinValue;
        public double Characteristic
        {
            get
            {
                if (characteristic == double.MinValue)
                {
                    characteristic = GetCharacteristic();
                }

                return characteristic;
            }
        }

        private double GetCharacteristic()
        {
            double dSlope = double.MinValue;

            IBarDatum retrace = GetRetraceBar();

            if (retrace != null)
            {
                double dMinutes = (double)( retrace.Time - StartBar.Time ).Minutes;
                if (Trend == (int) Data.Trend.TrendType.Up)
                {
                    dSlope = ( retrace.Low - StartBar.Low ) / dMinutes;
                }
                else
                {
                    dSlope = ( retrace.High - StartBar.High ) / dMinutes;
                }
            }

            return dSlope;
        }

        private IBarDatum GetRetraceBar()
        {
            IBarDatum barDatum = ChildWaves.Where(w => w.Trend != Trend && w.EndBar != null)
                .Select(w => w.EndBar)
                .FirstOrDefault();
          
            return barDatum;
        }

        public static IWave Open (int waveId, int parentId, IMove initialMove)
        {
            Wave wave = new Wave(waveId, parentId, initialMove);

            return wave;
        }
    }// end class Wave
}
