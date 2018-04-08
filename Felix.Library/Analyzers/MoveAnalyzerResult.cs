using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Felix.Interfaces;


namespace Felix.Library.Analyzers
{
    public interface IMoveAnalyzerResult
    {
        IBarDatum Data { get; }
        IEnumerable<ProspectiveIR> IRList { get;  }
    }
    public class MoveAnalyzerResult : IMoveAnalyzerResult
    {
        public IBarDatum Data { get; set; }

        public IEnumerable<ProspectiveIR> IRList { get; set; }

        public MoveAnalyzerResult() { }

        public MoveAnalyzerResult(IBarDatum data, IEnumerable<ProspectiveIR> irList)
        {
            Data = data;

            IRList = irList;
        }
    }
}
