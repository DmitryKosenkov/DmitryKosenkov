﻿namespace OOP_ICT.Fourth;

public enum PokerGameStateEnum
{
    NotStarted = 0, // Игра не началась, игроки могут присоединиться к игре
    Flop = 1, // Игра началась, дилер вытащил 3 карты, игроки получают карты, игроки могут делать ставки
    Turn = 2, // Дилер вытащил еще одну карту, игроки могут делать ставки
    River = 3, // Дилер вытащил еще одну карту, игроки могут делать ставки
    Finished = 4 // Игроки вскрывают карты, дилер должен быть изменен
}