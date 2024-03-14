namespace OOP_ICT.Second.Models;

public static class ChipsHelper
{
    public static uint ChipsToMoney(uint chipsAmount)
    {
        var moneyAmountDecimal = decimal.Floor(chipsAmount * CasinoConfig.ChipsExchangeRate);
        return (uint)moneyAmountDecimal;
    }

    public static uint MoneyToChips(uint moneyAmount)
    {
        var chipsAmountDecimal = decimal.Floor(moneyAmount / CasinoConfig.ChipsExchangeRate);
        return (uint)chipsAmountDecimal;
    }
}
