using System;
using System.Collections.Generic;
using System.Linq;

namespace dotfool
{

    class Game
  {
    const int HUMAN = 0;
    const int ENGINE = 1;
    public static Pack pack = new Pack();
    public static List<Card> table = new List<Card>();
    static Player[] players = { new Human(), new Engine() };
    static int turn;

    public static void Main(string[] args)
    {
      Console.WriteLine("New Game");
      Boolean play = true;
      
      while (play)
      {
        play = Play();
      }

      players[1].Show();
    }

    static Boolean Play()
    {

      Boolean play = true;

      foreach (Player p in players) 
        p.Fill();
      
      if (!table.Any())
        SetTurn();
      else
        table.Clear();

      
      Console.Write($"New act: {pack.TrumpCode()} ");
      if (pack.Size() > 0)
        Console.WriteLine($"{pack.Size()} cards left");
      else
        Console.WriteLine();
      
      do {  
        play = Act();
      } while (play);

      foreach (Player player in players)
        if (player.HasWon())
          return false;

      return true;
    }

    static Boolean Act() 
    { 
      players[0].Show();
      Boolean res = players[turn].Attack();
      if (!res) 
        turn = 1 - turn;
       
      ShowTable();
 
      if (res) 
      {
        res = players[1 - turn].Defend();
        if (!res)
          players[turn].Message();
        
        ShowTable();
      }
    
      return res;
    }
      
    static void SetTurn()
    {

      var cards = players.Select(p => p.LeastTrump());
      if (cards.First() == null)
        turn = ENGINE;
      else
      {
        if (cards.Last() != null)
        {
          var ranks = cards.Select(c => c.Rank);
          turn = ranks.First() > ranks.Last() ? HUMAN : ENGINE;
        }
        else
          turn = HUMAN;
      }
    } 

    static void ShowTable()
    {
      for (int i = 0; i < table.Count; i+= 2)
      {
        table[i].Show();
        if (i + 1 < table.Count)
          table[i+1].Show();
        Console.Write("   ");
      }
      Console.WriteLine();
    }  
  
  }    
}
