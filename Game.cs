using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace dotfool
{


  class Game
  {
    public const int MinHand = 6;
    enum Turns {Player, Engine};
    static Pack pack = new Pack();
    static List<int> hand = new List<int>();
    static Engine engine;

   
    public static void Main(string[] args)
    {
      Console.OutputEncoding = System.Text.Encoding.UTF8;     
      Init();
      Boolean over = false;
      while (!over)
        {
          Play();
        }
    }

    static void Play()
    {
      pack.ShowTrump();
      pack.ShowHand(hand);
      Boolean done = false;
      int picked;
      while (!done)
        {
          Console.Write("Your move: ");
          picked = hand[Convert.ToInt32(Console.ReadLine())];
          pack.ShowCard(picked);
          // engine.Defend(picked);
          
          
        }
    }


    static void Init()
    {
      for (int i = 0; i < MinHand; i++)
        {
            hand.Add(pack.Take());
        }
      engine = new Engine(pack);
    }
  
  }
      
}
