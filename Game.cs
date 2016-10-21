using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace dotfull
{

  public class Card : Tuple<int, int>
  {
    public Card(int suit, int rank)
      : base(suit, rank)
    {
    }

    public int suit { 
      get { return this.Item1; }
    }

    public int rank {
      get { return this.Item2; }
    }
    // put compare here
  }


  class Game
  {
    const int NSuits = 4;
    const int NRanks = 9;
    public const int NCards = 36;
    public const int MinHand = 6;
   

    static string[,] Codes = new string[NSuits, NRanks];
    static Card[] Pack = new Card[NCards];
    static List<int> Order;
    static List<int> Hand = new List<int>();
    static Card Trump;
    static Engine engine;



    public static void Main(string[] args)
    {
      Console.OutputEncoding = System.Text.Encoding.UTF8;
      Init();
      Shuffle();
      Play();      
    }

    static void Play()
    {
      Console.WriteLine(Codes[Trump.suit, Trump.rank]);
      Console.WriteLine("Your hand:");
      Card card;
      for (int n = 0; n < Hand.Count; n++)
        {
          card = Pack[Order[n]];
          Console.Write($"{n}: {Codes[card.suit, card.rank]} ");
        }
        
      Console.WriteLine();
      Console.Write("Your move: ");
      Console.ReadLine();
    }


    static void Shuffle()
    {
      Random rnd = new Random();
      Order = Enumerable.Range(0, NCards).OrderBy(r => rnd.Next()).ToList(); 

      for (int i = 0; i < MinHand; i++)
        {
          Hand.Add(Order[i]);
        }
        Trump = Pack[Order[NCards - 1]];
      engine = new Engine(Pack, Order);
    }


    static void Init()
    {
      // unicodes for cards https://en.wikipedia.org/wiki/Playing_cards_in_Unicode

      byte[] suits = new byte[] { 0xA1, 0xB1, 0xC1, 0xD1 };
      byte[] code = { 0x00, 0xF0, 0x01, 0x00 };
      int k;

      for (int i = 0; i < NSuits; i++)
        {
          byte ace = suits[i];
          code[0] = ace;
          Codes[i, 8] = Encoding.UTF32.GetString(code);
          Pack[i * NRanks + 8] = new Card(i, 8);
          for (byte j = 5; j < 14; j++)
            {
              if (j == 11)
                continue;

              k = j - (j < 11 ? 5 : 6); 

              code[0] = (byte)(ace + j);
              Codes[i, k] = Encoding.UTF32.GetString(code);
              Pack[i * NRanks + k] = new Card(i, k); 
            }
        }
    }
  }
      
}
