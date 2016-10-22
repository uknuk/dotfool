using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace dotfool
{

  public class Card : Tuple<int, int>
  {
    public Card(int suit, int rank)
      : base(suit, rank)
    {
    }

    public int suit { 
      get { return this.Item1; }
    }

    public int rank {
      get { return this.Item2; }
    }
    // put compare here
  }

  public class Pack
  {
    const int NSuits = 4;
    const int NRanks = 9;
    const int NCards = NSuits * NRanks;
    public Card[] cards = new Card[NCards];
    private string[,] codes = new string[NSuits, NRanks];
    private List<int> order;
    public Card trump;

    public Pack()
    {
      // unicodes for cards https://en.wikipedia.org/wiki/Playing_cards_in_Unicode

      byte[] suits = new byte[] { 0xA1, 0xB1, 0xC1, 0xD1 };
      byte[] code = { 0x00, 0xF0, 0x01, 0x00 };
      int k;

      for (int i = 0; i < NSuits; i++)
        {
          byte ace = suits[i];
          code[0] = ace;
          codes[i, 8] = Encoding.UTF32.GetString(code);
          cards[i * NRanks + 8] = new Card(i, 8);
          for (byte j = 5; j < 14; j++)
            {
              if (j == 11)
                continue;

              k = j - (j < 11 ? 5 : 6); 

              code[0] = (byte)(ace + j);
              codes[i, k] = Encoding.UTF32.GetString(code);
              cards[i * NRanks + k] = new Card(i, k); 
            }
        }

      Random rnd = new Random();
      order = Enumerable.Range(0, NCards).OrderBy(r => rnd.Next()).ToList(); 
      trump = cards[order.Last()];
    }

    public int Take()
    {
      int idx = order[0];
      order.RemoveAt(0);
      return idx;
    }
        
    public void ShowTrump()
    {
      Console.WriteLine($"{codes[trump.suit, trump.rank]}  {order.Count} cards left");
    }

    public void ShowCard(int idx)
    {
      Card card = cards[idx];
      Console.Write($"You: {codes[card.suit, card.rank]} ");
    }

    public void ShowHand(List<int> hand)
    {
      Console.Write("Your hand: ");
      for (int i = 0; i < hand.Count; i++)
        {
          Card card = cards[hand[i]];
          Console.Write($"{i}: {codes[card.suit, card.rank]}   ");
        }
      Console.WriteLine();
    }

  }
}

