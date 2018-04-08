using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Felix.Interfaces;
using Felix.Data;

namespace Felix.Library.BLL
{
    public enum WaveBuilderState
    {
        S0 = 0,
        S1,
        S2,
        S3,
        S4,
        S5
    }

    public enum WaveBuilderTrendStatus
    {
       Continue = 0,
       Short,
       Even,
       Long

    }

    public class WaveBuilderItem
    {
        public IWave Wave { get; set; }

        public int WaveId { get; set; }

        public int ParentWaveId { get; set; }

        public WaveBuilderState State { get; set; }

        public WaveBuilderItem()
        {
            State = WaveBuilderState.S0;
        }

        public WaveBuilderTrendStatus Update (IWaveIdProvider idProvider, IMove move)
        {
            throw new NotImplementedException();
        }

     


    }

    public interface IWaveIdProvider
    {
        int CurrentId { get; }
    }

    public class WaveBuilder : IWaveIdProvider
    {
        private List<IWave> Waves = new List<IWave>();

        private WaveStack Stack { get; set; }

        public int CurrentId => Waves.Count+1;

        public WaveBuilder()
        {
            Stack = new WaveStack();
        }

        public void Insert(IMove move)
        {
            var item = Stack.Top();

            if (item.State == WaveBuilderState.S0)
            {
                IWave wave = Wave.Open(CurrentId, 0, move);

                item.Wave = wave;

                item.Update(this, move);
            }
            else
            {
                var status = item.Update(this, move);


            }
        }
    }
}
