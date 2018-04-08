using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Felix.Analyzers;
using Felix.Data;

namespace Felix.Managers
{
    public class PositionManager
    {
        private IMoveManager MoveMgr { get; set; }

        private MoveAnalyzer MoveAnalyzer { get; set; }

        public PositionManager (IMoveManager mm)
        {
            MoveMgr = mm;

            MoveAnalyzer = new MoveAnalyzer(mm);

            MoveMgr.BarStream.Subscribe(d => OnBarUpdate(d), err => OnBarError(err));

            MoveAnalyzer.ResultStream.Subscribe(r => OnAnalyzerUpdate(r));
        }

        private void OnBarUpdate( BarDatum bd)
        {
            Debug.WriteLine($"OnBarUpdate: {bd.ToString()} ");
        }

        private void OnBarError (Exception err)
        {
            throw ( err );
        }

        private void OnAnalyzerUpdate(MoveAnalyzerResult result)
        {
            // this is a new Move.
            // find prospective Ir for this move
        }

        private void OnMoveError (Exception err)
        {
            throw ( err );
        }
    }
}
