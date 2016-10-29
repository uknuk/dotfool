using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace dotfool
{

  public class Card
  {    
    public Card(int suit, int rank, string code)
    {
      Rank = rank;
      Suit = suit;
      Code = code;
    }

    public Card() 
    {
      Rank = -1;
    }

    public int Suit { get; set; }
    public int Rank { get; set; }
    public string Code { get; set; }
    public int Value => Suit == Game.pack.Trump() ? 10 + Rank : Rank;
    public Boolean IsGreater(Card card)
    {
      if (Suit == card.Suit)
        return Rank > card.Rank;
      else
        return IsTrump();
    }
    
    public void Show()
    {
      Console.Write($" {Code} ");
    }

    public Boolean IsTrump()
    {
      return Suit == Game.pack.Trump();
    }

    public Boolean IsValid()
    {
      return Rank != -1;
    }
        
  }           

  public class Pack
  {
    const int NSuits = 4;
    const int NRanks = 9;
    const int NCards = NSuits * NRanks;
    public Card[] cards = new Card[NCards];
    private List<int> order;
    private Card trump;

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
          cards[i * NRanks + 8] = 
              new Card(i, 8, Encoding.UTF32.GetString(code));
            
          for (byte j = 5; j < 14; j++)
            {
              if (j == 11)
                continue;

              k = j - (j < 11 ? 5 : 6); 

              code[0] = (byte)(ace + j);
              cards[i * NRanks + k] = 
                  new Card(i, k, Encoding.UTF32.GetString(code)); 
            }
        }

      Random rnd = new Random();
      order = Enumerable.Range(0, NCards).OrderBy(r => rnd.Next()).ToList(); 
      trump = cards[order.Last()];
    }

    public Card Take()
    {
      if (!order.Any())
        return null;
        
      int idx = order[0];
      order.RemoveAt(0);
      return cards[idx];
    }

    public int Size()
    {
      return order.Count;
    }

    public int Trump()
    {
      return trump.Suit;
    }

    public string TrumpCode()
    {
      return trump.Code;
    }
        
  }
}

