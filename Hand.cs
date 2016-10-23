using System;
using System.Collections.Generic;
using System.Linq;

namespace dotfool
{
  public class Hand : List<Card>
  {
    const int Minimum = 6;
    private Pack pack;

    public Hand(Pack pack)
      : base()
    {
      this.pack = pack;
    }

    public void Fill()
    {
      int size = Minimum - Count;
      for (int i = 0; i < size; i++)
        {
          Add(pack.Take());
        }
    }

    public void Show()
    {
      Console.Write("Your hand: ");
      for (int i = 0; i < Count; i++)
        Console.Write($"{i}: {this[i].Code}   ");
      Console.WriteLine();
    }

    public Card GetLeastRank(int suit, int rank)
    {
      var cards = from card in this
                         where card.Suit == suit && card.Rank > rank
                         orderby card.Rank
                         select card;

      return cards.Count() > 0 ? cards.First() : null;
    }
    
  }
}

