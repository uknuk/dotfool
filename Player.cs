using System;
using System.Collections.Generic;
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
      Card card;
      for (int i = 0; i < size; i++)
      {
        card = Game.pack.Take();
        if (card != null)
          hand.Add(card);
      }
    }

    protected void Pass(Card card)
    {
      hand.Remove(card);
      Game.table.Add(card);
    }

    protected Card LeastRank(int suit, int rank)
    {
      var cards = 
        from card in hand
        where card.Suit == suit && card.Rank > rank
        orderby card.Rank
        select card;

      return cards.Any() ? cards.First() : null;
    }

    public Card LeastTrump()
    {
      return LeastRank(trump, -1);
    }

    public Boolean Defend() 
    {
      Card card = Response();
      if (card == null)
      {
        hand.AddRange(Game.table);
        return false;
      }
      else
      {
        Pass(card);
        return true;
      }
    }

    public Boolean Attack()
    {
      Card card = Move();
      if (card != null)
      {
        Pass(card);
        return true;
      }
      return false;
    }
    
    public virtual Card Move()
    {
      return null;
    }

    public virtual Card Response()
    {
      return null;
    }

    public virtual void Show()
    {
      Console.WriteLine("Not implemented");
    }

    public virtual void Message()
    {
      Console.WriteLine();
    }

    public Boolean HasWon()
    {
      if (!hand.Any())
      {
        Finish();
        return true;
      }
      return false;
    }

    public virtual void Finish()
    {
      Console.WriteLine();
    }
  }
}