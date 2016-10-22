using System;
using System.Collections.Generic;

namespace dotfool
{
  public class Engine
  {
    private List<int> hand = new List<int>();
    private Card card;
    private Pack pack;
    private int trump;

    public Engine(Pack pack)
    {
      this.pack = pack;
      for (int n = 0; n < Game.MinHand; n++)
          hand.Add(pack.Take());
      trump = pack.trump.suit;
    }

  public Card Defend(int idx)
    {
      Card card = pack.cards[idx];
      // select hand cards from pack by uning linq
      // find the defending card
      return card;
    }
  }
}

