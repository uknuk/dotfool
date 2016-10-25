using System;
using System.Collections.Generic;
using System.Linq;

namespace dotfool
{

  public class Human : Player 
  { 
    public override Boolean Attack()
    { 
      return Get("move", this.Verify);
    }

    public override Boolean Defend()
    {
      return Get("response", this.CanBeat);
    }

    private Boolean Get(string action, Func<Card, Boolean> check)
    {
      Card card;
      do 
      {
        card = Input(action);
        if (card == null)
          return false;  // pass
      } while (!check(card));
      
      Pass(card);
      return true;
    }

    private Card Input(string action)
    {
      Console.Write($"Your {action}: ");
      string choice = Console.ReadLine();
      if (choice == "p")
        return null;
      else
      {
        try
          {
            int n = int.Parse(choice);

          if (n < hand.Count)
            return hand[n];
          else
          {
            Console.WriteLine("Use number of a hand card");
            return null;
          }
        }
        catch
        {
          Console.WriteLine($"Invalid input {choice}");
          return null;
        }
      } 
    }

    Boolean Verify(Card attack)
    {
      if (!Game.table.Any())
        return true;
            
      var cards = 
        from card in Game.table
              where card.Rank == attack.Rank
              select card;
      return cards.Any();
    }

    Boolean CanBeat(Card defence)
    {
      Card attack = hand.Last();
      return defence.IsGreater(attack);
    }
    
    public override void Show()
    {
      Console.Write("Your hand: ");
      for (int i = 0; i < hand.Count; i++)
        Console.Write($"{i}: {hand[i].Code}   ");
      Console.WriteLine();
    }

    public override void Message()
    {
      Console.WriteLine("Congrats! Engine is beaten and takes the table");
    }
  }
}

