namespace OOP_ICT.Second.Models;

public static class CasinoConfig
{
    public static decimal WinCoefficient = 1.5M;
    public static decimal ChipsExchangeRate = 2.0M;

    public static void SetParameters(decimal winCoefficient, decimal chipsExchangeRate)
    {
        WinCoefficient = winCoefficient;
        ChipsExchangeRate = chipsExchangeRate;
    }
}
