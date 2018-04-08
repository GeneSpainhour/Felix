using System.Collections.Generic;



namespace Felix.Interfaces
{
    public interface IWave
    {
        double Characteristic { get; }
        List<IWave> ChildWaves { get; }
        IBarDatum EndBar { get; set; }
        int ParentWaveId { get; set; }
        IBarDatum StartBar { get; set; }
        int Trend { get; set; }
        int WaveId { get; set; }

        IWave Close(IMove endingMove);

        IWave AddChild(IWave childWave);
    }
}