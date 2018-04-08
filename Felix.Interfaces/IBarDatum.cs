using System;

namespace Felix.Interfaces
{
    public interface IBarDatum
    {
        double A0 { get; set; }
        double A1 { get; set; }
        double A2 { get; set; }
        double A3 { get; set; }
        double A4 { get; set; }
        int BarDataId { get; set; }
        double Close { get; set; }
        int ContractId { get; set; }
        double D2A0 { get; set; }
        double D2A1 { get; set; }
        double D2A2 { get; set; }
        double D2A3 { get; set; }
        double D2A4 { get; set; }
        double D2M { get; set; }
        double DA0 { get; set; }
        double DA1 { get; set; }
        double DA2 { get; set; }
        double DA3 { get; set; }
        double DA4 { get; set; }
        double DF { get; set; }
        double DM { get; set; }
        double F { get; set; }
        double High { get; set; }
        int Index { get; set; }
        double Low { get; set; }
        double M { get; set; }
        string Meta { get; set; }
        double Open { get; set; }
        int Period { get; set; }
        double S { get; set; }
        DateTime Time { get; set; }

        string ToString();
    }
}