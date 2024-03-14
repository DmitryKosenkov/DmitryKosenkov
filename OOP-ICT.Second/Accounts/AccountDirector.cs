namespace OOP_ICT.Second.Models.Accounts;

public class AccountDirector
{
    private readonly AccountBuilder _builder = new();
    
    public Account BuildBankAccount(Player player)
    {
        return _builder
            .SetId(player.Id)
            .SetCurrency(CurrencyEnum.Money)
            .SetBalance(1000)
            .Build();
    }
    
    public Account BuildCasinoAccount(Player player)
    {
        return _builder
            .SetId(player.Id)
            .SetCurrency(CurrencyEnum.Chips)
            .SetBalance(0)
            .Build();
    }
}