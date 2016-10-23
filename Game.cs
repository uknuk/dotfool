using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace dotfool
{


  class Game
  {
   
    static Pack pack = new Pack();
    static Hand hand = new Hand(pack);
    static Engine engine = new Engine(pack);
    static List<Card> table = new List<Card>();
    static Boolean playerTurn;

    public static void Main(string[] args)
    {
      Boolean over = false;
      while (!over)
        {
          Play();
        }
    }

    static void Play()
    {

      Boolean done = false;
      string choice;
      Card attack;
      Card resp;

      hand.Fill();
      engine.hand.Fill();
      
      if (!table.Any())
        SetTurn();
      else
        table.Clear();

      Console.WriteLine($"New act: {pack.Trump.Code} {pack.Size()} cards left");
      
      while (!done)
        {  
          hand.Show();
          Console.Write("Your move: ");
          choice = Console.ReadLine();
          if (choice == "b")
            {
              if (table.Count > 0)
                playerTurn = !playerTurn;
              // future version will store table for engine 
              continue;
            }
            
          attack = hand[Convert.ToInt32(choice)];
          if (!Verify(attack))
            {
              Console.WriteLine("Don't cheat!!!");
              continue;
            }
          hand.Remove(attack);
          table.Add(attack);
          attack.Show();
          resp = engine.Defend(attack);
          if (resp == null)
            {
              done = true;
              engine.hand.AddRange(table);
              Console.WriteLine("Congrats! Engine is beaten ane takes the table");
            } else
            {
              table.Add(resp);
              Console.WriteLine($" {resp.Code}"); 
            }
        }
    }

    static void SetTurn()
    {
      Card human = hand.GetLeastRank(pack.Trump.Suit, -1);
      if (human == null)
        playerTurn = false;
      else
        {
          Card comp = engine.hand.GetLeastRank(pack.Trump.Suit, -1);
          if (comp != null)
            playerTurn = human.Rank > comp.Rank;
          else
            playerTurn = true;
        }
    }

    static Boolean Verify(Card attack)
    {
      if (table.Count == 0)
        return true;
            
      var cards = 
        from card in table
              where card.Rank == attack.Rank
              select card;
      return cards.Count() > 0;
    }
  
  }
      
}
