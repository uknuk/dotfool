using System;
using System.Collections.Generic;
using System.Linq;

namespace dotfool
{
  public class Engine : Player
  {
    public override Card Defend(Card aCard)
    {
      Card dCard = LeastRank(aCard.Suit, aCard.Rank);
      
      if (dCard == null)
        dCard = LeastRank(trump, aCard.Rank);
        
      if (dCard != null)
        Pass(dCard);

      return dCard;
    }

    public override Card Attack()
    {
      var cards = from card in hand 
        where card.Suit != trump
        orderby card.Rank select card;

      Card aCard = cards.First();
      Pass(aCard);
      return aCard;
    }

    public override void Message()
    {
      Console.WriteLine("Sorry but you gotta take it");
    }

  } 
}

