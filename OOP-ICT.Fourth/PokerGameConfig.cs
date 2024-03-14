namespace OOP_ICT.Fourth;

public static class PokerGameConfig
{
    public const int MinimalBet = 1; // минимальная ставка
    public const int BetToJoin = 1; // ставка (блайнд), чтобы участвовать в игре
    public const int PlayerCardsAmount = 2; // количество карт, которые выдает дилер каждому игроку
    public const int FlopCardsAmount = 3; // количество карт, которые вытаскивает дилер во время Flop
    public const int TurnCardsAmount = 1; // количество карт, которые вытаскивает дилер во время Turn
    public const int RiverCardsAmount = 1; // количество карт, которые вытаскивает дилер во время River
}