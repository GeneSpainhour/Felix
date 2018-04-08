using System;

namespace Felix.Interfaces
{
    public interface IInstaTrendValue
    {
        double Value { get; set; }

        double DValue { get; set; }

        double D2Value { get; set; }

        DateTime Time { get; set; }

        int ContractId { get; set; }

        int Period { get; set; }
    }
}