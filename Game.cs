using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace dotfool
{
  public class Player
  {
    const int MinHand = 6;
    protected int trump;
    protected List<Card> hand = new List<Card>();

    public Player()
    {
      trump = Game.pack.Trump();
    }

    public void Fill()
    {
      int size = MinHand - hand.Count;
      for (int i = 0; i < size; i++)
      {
        hand.Add(Game.pack.Take());
      }
    }

    protected void Pass(Card card)
    {
      hand.Remove(card);
      Game.table.Add(card);
    }

    protected Card LeastRank(int suit, int rank)
    {
      var cards = from card in hand
                         where card.Suit == suit && card.Rank > rank
                         orderby card.Rank
                         select card;

      return cards.Count() > 0 ? cards.First() : null;
    }

    public Card LeastTrump()
    {
      return LeastRank(trump, -1);
    }

    public virtual Boolean Attack()
    {
      return true;
    }

    public virtual Boolean Defend()
    {
      return true;
    }

    public virtual void Show()
    {
      Console.WriteLine("Not implemented");
    }

    public virtual void Message()
    {
      Console.WriteLine();
    }
  }

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
      while (true)
      {
        Play();
      }
    }

    static void Play()
    {

      Boolean play = true;

      foreach (Player p in players) 
        p.Fill();
      
      if (!table.Any())
        SetTurn();
      else
        table.Clear();

      Console.WriteLine($"New act: {pack.TrumpCode()} {pack.Size()} cards left");
      
      do {  
        play = Act();
      } while (play);
    }

    static Boolean Act() 
    { 
      if (table.Any())
        ShowTable();

      players[0].Show();
      if (!players[turn].Attack()) 
      {
        turn = 1 - turn;
        // future version will store table for engine
        ShowTable();
        return false;
      }

      if (!players[1 - turn].Defend())
      {
        players[turn].Message();
        ShowTable();
        return false;
      }
      //else
      //  ShowTable();

      return true;
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
