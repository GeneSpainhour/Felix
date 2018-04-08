using System;

namespace Felix.Interfaces
{
    public interface IBarSeriesObject
    {
        int ContractId { get; set; }
        int Period { get; set; }
        DateTime Time { get; set; }
    }
}