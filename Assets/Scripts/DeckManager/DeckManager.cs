using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DeckManager : MonoBehaviour
{
    public List<Card> DrawPile;
    public List<Card> DiscardPile;
    public List<Card> Hand;
    public List<Card> CardBases;

    public void GenerateDemoDeck()
    {
        foreach (Card card in CardBases)
        {
            int rand = Random.Range(1, 7);
            for (int i = 0; i < rand; i++)
            {
                AddCard(card);
            }
        }
    }

    public void AddCard(Card card)
    {
        DrawPile.Add(card);
    }

    public List<Card> DealHand(int amount)
    {
        List<Card> drawn = new List<Card>();
        int totalcards = DrawPile.Count + DiscardPile.Count;
        amount = amount > totalcards ? totalcards : amount;
        while (drawn.Count != amount)
        {
            if (DrawPile.Count <= 0 && DiscardPile.Count > 0)
                ResetDeck();

            drawn.Add(DrawPile[0]);
            DrawPile.Remove(DrawPile[0]);
        }
        Hand = drawn;
        return drawn;
    }

    public void ResetDeck()
    {
        List<Card> copy = new List<Card>(DiscardPile);
        foreach (Card card in copy)
        {
            DrawPile.Add(card);
            DiscardPile.Remove(card);
        }
    }

    public void ShuffleDraw()
    {
        List<Card> suffled = new List<Card>();
        while(DrawPile.Count > 0)
        {
            int r = Random.Range(0, DrawPile.Count);
            suffled.Add(DrawPile[r]);
            DrawPile.Remove(DrawPile[r]);
        }
        DrawPile = suffled;
    }
}
