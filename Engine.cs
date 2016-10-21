using System;
using System.Collections.Generic;

namespace dotfull
{
  public class Engine
  {
    private List<int> hand = new List<int>();
    private List<int> order;
    private Card[] pack;
    private int trump;

    public Engine(Card[] pack, List<int> order)
    {
      this.pack = pack;
      this.order = order;
        for (int n = 0; n < Game.MinHand; n++)
          hand.Add(order[2*n]);
      
      trump = pack[order[Game.NCards - 1]].suit;     
    }
  }
}

