using System;
using System.Linq;

namespace dotfool
{

    public class Human : Player 
  { 
    public override Card Move()
    { 
      if (!hand.Any())
        return null;
        
      return Get("move", this.Verify);
    }

    public override Card Response()
    {
      return Get("response", this.CanBeat);
    }

    private Card Get(string action, Func<Card, Boolean> check)
    {
      Card card;
      do 
      {
        card = Input(action);
      } while (card != null && !check(card));
    
      return card;
    }

    private Card Input(string action)
    {
      Console.Write($"Your {action}: ");
      string choice = Console.ReadLine();
      if (choice == "p")
        return null;
      else if (choice == "d")
      {
        Game.ShowEngine();
        return null;
      }
      else
      {
        try
          {
            int n = int.Parse(choice);

          if (n < hand.Count)
            return hand[n];
          else
          {
            Console.WriteLine("Use the number of a hand card");
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
      Card attack = Game.table.Last();
      return defence.IsGreater(attack);
    }
    
    public override void Show()
    {
      hand = hand.OrderBy(card => card.Rank).ToList();
      Console.Write("Your hand: ");
      for (int i = 0; i < hand.Count; i++)
        Console.Write($"{i}: {hand[i].Code}   ");
      Console.WriteLine();
    }

    public override void Message()
    {
      Console.WriteLine("Engine surrenders, you can add more cards");
    }

    public override void Finish()
    {
      Console.WriteLine("Congrats! You won");
    }
  }
}

