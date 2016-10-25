using System;
using System.Collections.Generic;
using System.Linq;

namespace dotfool
{
  public class Engine : Player
  {
    public override Boolean Defend()
    {
      Card aCard = Game.table.Last();
      Card dCard = LeastRank(aCard.Suit, aCard.Rank);
      
      if (dCard == null)
        dCard = LeastRank(trump, aCard.Rank);
        
      if (dCard != null)
      {
        Pass(dCard);
        return true;
      }
        
      return false;
    }

    public override Boolean Attack()
    {
      var cards = from card in hand 
        where card.Suit != trump
        orderby card.Rank select card;

      if (cards.Any()) {
        Pass(cards.First());
        return true;
      }

      return false;
    }

    public override void Message()
    {
      Console.WriteLine("Sorry but you gotta take it");
    }

  } 
}

