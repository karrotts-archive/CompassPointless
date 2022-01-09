using System.Collections.Generic;
using UnityEngine;

public class Deck
{
    public List<Card> Cards;

    public Deck()
    {
        Cards = new List<Card>();
    }

    public Card Draw(bool discard = false)
    {
        Card card = Cards[Random.Range(0, Cards.Count)];
        if (discard) Cards.Remove(card);
        return card;
    }

    public Card[] DrawAmount(int amount, bool discard = false)
    {
        Card[] cards = new Card[amount];
        List<Card> cardList = new List<Card>(Cards);
        for (int i = 0; i < amount; i++)
        {
            Card card = cardList[Random.Range(0, cardList.Count)];
            cards[i] = card;
            cardList.Remove(card);
            if (discard) Cards.Remove(card);
        }
        return cards;
    }
}