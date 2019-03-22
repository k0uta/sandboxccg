using System.Collections.Generic;
using UnityEngine;

namespace AutoCCG
{
    public class DeckController : MonoBehaviour
    {
        public DeckModel deck;

        public List<CardModel> cards;

        void Awake()
        {
            foreach (var cardEntry in deck.entries)
            {
                for (int i = 0; i < cardEntry.quantity; i++)
                {
                    cards.Add(cardEntry.card);
                }
            }
            cards.Shuffle();
        }
    }

}