using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace dotfool
{

  public class Card
  {
    private int rank;
    private int suit;
    private string code;

    public Card(int suit, int rank, string code)
    {
      this.rank = rank;
      this.suit = suit;
      this.code = code;
    }

    public int Suit {
      get { return suit; }
    }

    public int Rank {
      get { return rank; }
    }

    public string Code {
      get { return code; }
    }

    public void Show()
    {
      Console.Write($" {code} ");
    }
        
  }
    
    
            

  public class Pack
  {
    const int NSuits = 4;
    const int NRanks = 9;
    const int NCards = NSuits * NRanks;
    public Card[] cards = new Card[NCards];
    private List<int> order;
    public Card Trump;

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
      Trump = cards[order.Last()];
    }

    public Card Take()
    {
      int idx = order[0];
      order.RemoveAt(0);
      return cards[idx];
    }

    public int Size()
    {
      return order.Count;
    }
        
  }
}

