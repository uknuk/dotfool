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
      card.Show();
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

    public virtual Card Attack()
    {
      return null;
    }

    public virtual Card Defend(Card card)
    {
      return null;
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
      players[0].Show();
      Card attack = players[turn].Attack();
      if (attack == null) 
      {
        turn = 1 - turn;
        // future version will store table for engine
        return false;
      }

      Card resp = players[1 - turn].Defend(attack);
      if (resp == null)
      {
        players[turn].Message();
        return false;
      }
      else
        Console.WriteLine();

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
  
  }    
}
