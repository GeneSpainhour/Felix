using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Felix.Interfaces;
using Felix.Data;

namespace FelixTests
{
    public class UpTrend
    {
        private static int ContractId = 1;

        private static int Volume = 100;

        public static List<double> Opens = new List<double> {
25.00,
45.00,
65.00,
85.00,
105.00,
125.00,
145.00,
165.00,
185.00,
205.00,
225.00,
245.00,
265.00,
285.00,
305.00,
325.00,
345.00,
365.00,
385.00,
400.00,
390.00,
380.00,
370.00,
360.00,
360.00,
360.00,
370.00,
380.00,
390.00,
400.00,
410.00,
420.00,
430.00,
440.00,
450.00,
460.00,
470.00,
480.00,
490.00,
500.00

};

        public static List<double> Highs = new List<double> {
50.00,
70.00,
90.00,
110.00,
130.00,
150.00,
170.00,
190.00,
210.00,
230.00,
250.00,
270.00,
290.00,
310.00,
330.00,
350.00,
370.00,
390.00,
410.00,
400.00,
390.00,
380.00,
370.00,
360.00,
360.00,
370.00,
380.00,
390.00,
400.00,
410.00,
420.00,
430.00,
440.00,
450.00,
460.00,
470.00,
480.00,
490.00,
500.00,
510.00

};

        public static List<double> Lows = new List<double> {
            20.00,
40.00,
60.00,
80.00,
100.00,
120.00,
140.00,
160.00,
180.00,
200.00,
220.00,
240.00,
260.00,
280.00,
300.00,
320.00,
340.00,
360.00,
380.00,
370.00,
360.00,
350.00,
340.00,
330.00,
330.00,
340.00,
350.00,
360.00,
370.00,
380.00,
390.00,
400.00,
410.00,
420.00,
430.00,
440.00,
450.00,
460.00,
470.00,
480.00

 };

        public static List<double> Closes = new List<double> {
45.00,
65.00,
85.00,
105.00,
125.00,
145.00,
165.00,
185.00,
205.00,
225.00,
245.00,
265.00,
285.00,
305.00,
325.00,
345.00,
365.00,
385.00,
405.00,
395.00,
385.00,
375.00,
365.00,
355.00,
355.00,
365.00,
375.00,
385.00,
395.00,
405.00,
415.00,
425.00,
435.00,
445.00,
455.00,
465.00,
475.00,
485.00,
495.00,
505.00



        };

        public static IEnumerable<IBar> Bars( int year, int month, int day, int hour, int period = 5)
        {
            List<IBar> bars = new List<IBar>();

            for (int i = 0, c=Opens.Count; i<c; ++i)
            {
                int ndx = bars.Count + 1;

                var bar = new Bar(ndx, ContractId, period, new DateTime(year, month, day, hour, i, 0), Highs[i], Lows[i], Opens[i], Closes[i], Volume);

                bar.BarId = ndx;

                bars.Add(bar);
            }

            return bars;

        }

       
    }
}

