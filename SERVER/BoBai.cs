using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace SERVER
{
    class BoBai
    {
        public Card[] cards = new Card[52];
        public void CreateCards()
        {

            for (int i = 0; i < 13; i++)
            {
                cards[i] = new Card(i, 1, i + 1);
            }
            for (int i = 13; i < 26; i++)
            {
                cards[i] = new Card(i - 13, 2, i + 1);
            }
            for (int i = 26; i < 39; i++)
            {
                cards[i] = new Card(i - 26, 3, i + 1);
            }
            for (int i = 39; i < 52; i++)
            {
                cards[i] = new Card(i - 39, 4, i + 1);
            }
        }
        private void Swap(ref Card card1, ref Card card2)
        {
            Card card;
            card = card1;
            card1 = card2;
            card2 = card;
        }
        public void RandomCards()
        {
            Random random = new Random();
            for (int i = 0; i < 1000; i++)
            {
                Swap(ref cards[random.Next(0, 52)], ref cards[random.Next(0, 52)]);
            }
        }

        public class Card
        {
            public int Type;
            public int Cards;
            public int Id;
            public Card(int cards, int type, int id)
            {
                Type = type;
                Cards = cards;
                Id = id;
            }
        }
    }
}
