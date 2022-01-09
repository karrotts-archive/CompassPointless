using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DeckManager
{
    public Deck PlayingDeck;
    public Deck DiscardPile;

    public DeckManager()
    {
        PlayingDeck = new Deck();
        DiscardPile = new Deck();

        //This will be handled by the game controller eventually for testing purposes cards will be generated here.
        Card pawn = new Card(0, "The Pawn", 1, 1);
        Card rook = new Card(2, "Rookie", 0, 2);
        Card bishop = new Card(1, "Bishop", 0, 2);

        for (int i = 0; i < 20; i++)
        {
            PlayingDeck.Cards.Add(pawn);
        }

        for (int i = 0; i < 7; i++)
        {
            PlayingDeck.Cards.Add(rook);
            PlayingDeck.Cards.Add(bishop);
        }
    }

    public List<Card> DealHand()
    {
        return PlayingDeck.DrawAmount(7).ToList<Card>();
    }
}
