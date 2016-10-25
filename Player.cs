using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace dotfool
{
  public class Player
  {
    const int MinHand = 6;
    protected int trump;
    protected List<Card> hand = new List<Card>();

    public Player()
    {
      trump = Game.pack.Trump();
    }

    public void Fill()
    {
      int size = MinHand - hand.Count;
      for (int i = 0; i < size; i++)
      {
        hand.Add(Game.pack.Take());
      }
    }

    protected void Pass(Card card)
    {
      hand.Remove(card);
      Game.table.Add(card);
    }

    protected Card LeastRank(int suit, int rank)
    {
      var cards = from card in hand
                         where card.Suit == suit && card.Rank > rank
                         orderby card.Rank
                         select card;

      return cards.Count() > 0 ? cards.First() : null;
    }

    public Card LeastTrump()
    {
      return LeastRank(trump, -1);
    }

    public Boolean Response() 
    {
      Boolean resp = Defend();
      if (!resp)
        hand.AddRange(Game.table);

      return resp; 
    }
    
    public virtual Boolean Attack()
    {
      return true;
    }

    public virtual Boolean Defend()
    {
      return true;
    }

    public virtual void Show()
    {
      Console.WriteLine("Not implemented");
    }

    public virtual void Message()
    {
      Console.WriteLine();
    }
  }
}