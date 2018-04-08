using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Felix.Interfaces;

namespace Felix.Data
{
    public class BarDatum : IBarDatum
    {
        public int Period { get; set; }
        public int BarDataId { get; set; }
        public int ContractId { get; set; }
        public DateTime Time { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Open { get; set; }
        public double Close { get; set; }
        public int Index { get; set; }
        public double M { get; set; }
        public double DM { get; set; }
        public double D2M { get; set; }
        public double A0 { get; set; }
        public double DA0 { get; set; }
        public double D2A0 { get; set; }
        public double A1 { get; set; }
        public double DA1 { get; set; }
        public double D2A1 { get; set; }
        public double A2 { get; set; }
        public double DA2 { get; set; }
        public double D2A2 { get; set; }
        public double A3 { get; set; }
        public double DA3 { get; set; }
        public double D2A3 { get; set; }
        public double A4 { get; set; }
        public double DA4 { get; set; }
        public double D2A4 { get; set; }
        public double F { get; set; }
        public double DF { get; set; }
        public double S { get; set; }
        public string Meta { get; set; }

        public BarDatum()
        {
        }

        public BarDatum( IBar bar)
        {
            Time = bar.Time;

            Index = bar.Index;

            High = bar.High;

            Low = bar.Low;

            Close = bar.Close;

            Open = bar.Open;

            Period = 5;

            ContractId = bar.ContractId;

            BarDataId = bar.BarId;
        }

        public BarDatum (
          
            int barDataId,
            int contractId,
            int period,
            DateTime time,
            double high, 
            double low, 
            double open, 
            double close, 
            int index, 
            double m, 
            double dm, 
            double d2m,
            double f,
            double df,
            double s,
            
            double a0,
            double da0,
            double d2a0,
            double a1, 
            double da1,
            double d2a1,
            double a2,
            double da2,
            double d2a2,
            double a3,
            double da3,
            double d2a3,
            double a4,
            double da4,
            double d2a4,
            string meta=""
            )

        {
            Time = time;
            BarDataId = barDataId;
            ContractId = contractId;
            Period = period;
            High = high;
            Low = low;
            Open = open;
            Close = close;
            Index = index;
            M = m;
            DM = dm;
            D2M = d2m;
            F = f;
            DF = df;
            S = s;
            A0 = a0;
            DA0 = da0;
            D2A0 = d2a0;
            A1 = a1;
            DA1 = da1;
            D2A1 = d2a1;
            A2 = a2;
            DA2 = da2;
            D2A2 = d2a2;
            A3 = a3;
            DA3 = da3;
            D2A3 = d2a3;
            A4 = a4;
            DA4 = da4;
            D2A4 = d2a4;
        }

        public override string ToString()
        {
            var props = GetType().GetProperties().ToList();

            StringBuilder bldr = new StringBuilder();

            int i = 0, c =props.Count-1;

            foreach (var prop in props)
            {
                bldr.AppendFormat("{0}: {1} {2} ", prop.Name, prop.GetValue(this), i++<c ? ", ": "\r\n");
            }

            return bldr.ToString();
          

        }
    }
   
}
