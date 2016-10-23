using System;
using System.Collections.Generic;
using System.Linq;

namespace dotfool
{
  public class Engine
  {
    public Hand hand;
    private int trump;

    public Engine(Pack pack)
    {
      hand = new Hand(pack);
      trump = pack.Trump.Suit;
    }

    public Card Defend(Card aCard)
    {
      Card dCard = hand.GetLeastRank(aCard.Suit, aCard.Rank);
      
      if (dCard == null)
        dCard = hand.GetLeastRank(trump, aCard.Rank);
        
      if (dCard != null)
        hand.Remove(dCard);
      return dCard;
    }
   

  
  }
}

