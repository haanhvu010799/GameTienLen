using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TienLen
{
    class Player
    {
        public BoBai BoBai = new BoBai();
        public void MixCards()
        {
            BoBai.CreateCards();
            BoBai.RandomCards();
        }
        public BoBai.Card[] PlayerCards(int player)
        {
            if (player == 1)
            {
                BoBai.Card[] _cards = new BoBai.Card[13];
                for (int i = 0; i < 13; i++)
                {
                    _cards[i] = BoBai.cards[i];
                }
                return _cards;
            }
            else if (player == 2)
            {
                BoBai.Card[] _cards = new BoBai.Card[13];
                for (int i = 13; i < 26; i++)
                {
                    _cards[i - 13] = BoBai.cards[i];
                }
                return _cards;
            }
            else if (player == 3)
            {
                BoBai.Card[] _cards = new BoBai.Card[13];
                for (int i = 26; i < 39; i++)
                {
                    _cards[i - 26] = BoBai.cards[i];
                }
                return _cards;
            }
            else
            {
                BoBai.Card[] _cards = new BoBai.Card[13];
                for (int i = 39; i < 52; i++)
                {
                    _cards[i - 39] = BoBai.cards[i];
                }
                return _cards;
            }
        }

        public static String FormatCardsToSend(BoBai.Card[] cards)
        {
            String Formated = "";
            foreach (var item in cards)
            {
                Formated += "{" + item.Id.ToString() + "|" + item.Cards.ToString() + "|" + item.Type.ToString() + "}" + "#";
            }

            return Formated;
        }
        public static String FormatCardsToSend(List<BoBai.Card> cards)
        {
            String Formated = "";
            foreach (var item in cards)
            {
                Formated += "{" + item.Id.ToString() + "|" + item.Cards.ToString() + "|" + item.Type.ToString() + "}" + "#";
            }

            return Formated;
        }
        private static void Swap(ref BoBai.Card card1, ref BoBai.Card card2)
        {
            BoBai.Card card;
            card = card1;
            card1 = card2;
            card2 = card;
        }
        public static List<BoBai.Card> SortCards(BoBai.Card[] cards)
        {

            for (int i = cards.Length - 1; i >= 0; i--)
            {
                for (int j = i; j >= 0; j--)
                {
                    if (cards[i].Cards < cards[j].Cards)
                    {
                        Swap(ref cards[i], ref cards[j]);
                    }
                    if (cards[i].Cards == cards[j].Cards)
                    {
                        if (cards[i].Type < cards[j].Type)
                        {
                            Swap(ref cards[i], ref cards[j]);
                        }

                    }
                }

            }

            List<BoBai.Card> value = new List<BoBai.Card>();
            for (int i = 0; i < cards.Length; i++)
            {
                value.Add(cards[i]);
            }
            return value;
        }
    }
}
