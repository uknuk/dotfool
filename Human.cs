using System;
using System.Collections.Generic;
using System.Linq;

namespace dotfool
{

  public class Human : Player 
  { 
    public override Card Attack()
    { 
      Card card;
      do 
      {
        card = Input();
        if (card == null)
          return null;  // pass
      } while (!Verify(card));
      
      Pass(card);
      return card;
    }

    public override Card Defend(Card card)
    {
      Console.Write("Your response:");
    }

    private Card Input()
    {
      Console.Write("Your move: ");
      string choice = Console.ReadLine();
      if (choice == "p")
        return null;
      else
      {
        int n = Convert.ToInt32(choice);
        if (n < hand.Count)
          return hand[n];
        else
        {
          Console.WriteLine("Use number of a hand card");
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
    
    public override void Show()
    {
      Console.Write("Your hand: ");
      for (int i = 0; i < hand.Count; i++)
        Console.Write($"{i}: {hand[i].Code}   ");
      Console.WriteLine();
    }

    public override void Message()
    {
      Console.WriteLine("Congrats! Engine is beaten ane takes the table");
    }
  }
}

