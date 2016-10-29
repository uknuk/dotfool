using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dotfool
{
    public class Engine : Player
    {
        readonly byte[] CRY = {0x22, 0xF6, 0x01, 0x00};
        public override Card Move()
        {
            IEnumerable<Card> cards;

            if (!Game.table.Any())
            {
                cards =
                  from card in hand
                  orderby card.Value
                  select card;
            }
            else
            {
                var ranks = Game.table.Select(card => card.Rank);
                cards =
                  from card in hand
                  where ranks.Contains(card.Rank)
                  orderby card.Value
                  select card;
            }

            return cards.Any() ? cards.First() : null;
        }

        public override Card Response()
        {
            Card aCard = Game.table.Last();

            var cards =
               from card in hand
               where card.IsGreater(aCard)
               orderby card.Value
               select card;

            return cards.Any() ? cards.First() : null;
        }

        public override void Message()
        {
            Console.WriteLine("Be ready to take more cards");
        }

        public override void Finish()
        {
            Console.WriteLine($"{Encoding.UTF32.GetString(CRY)} Condolences fool, you lost");
        }

        public override void Show()
        {
            if (!hand.Any())
                return;

            Console.Write("Remaining: ");
            foreach (Card card in hand)
                card.Show();
            Console.WriteLine();
        }
    }

}

