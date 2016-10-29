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
    static Boolean surrender;

    public static void Main(string[] args)
    {
      Console.WriteLine("New Game");
      Boolean play = true;
      
      while (play)
      {
        play = Play();
      }

      ShowEngine();
      Console.ReadLine();
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

      surrender = false;

      do {  
        play = Act();
        foreach (Player player in players)
          if (player.HasWon())
            return false;
      } while (play);

      return true;
    }
    static Boolean Act() 
    { 
      if (!surrender)
        players[0].Show();

      Boolean res = players[turn].Attack();
      if (!res) 
      {
        Console.WriteLine("No more cards");
        if (!surrender)
          turn = 1 - turn;
      }
        
      if (res)
        ShowTable();
 
      if (res && !surrender) 
      {
        if (!players[1 - turn].Defend())
        {
          surrender = true;
          players[turn].Message();
        }
        else
        {
          ShowTable();
        }     
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
        if (!surrender && i + 1 < table.Count)
          table[i+1].Show();
        Console.Write("   ");
      }
      Console.WriteLine();
    } 

    static public void ShowEngine()
    {
      players[1].Show();
    } 
  
  }    
}
