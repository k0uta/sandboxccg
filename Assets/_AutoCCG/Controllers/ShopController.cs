using System.Collections.Generic;
using UnityEngine;

namespace AutoCCG
{
    public class ShopController : MonoBehaviour
    {
        public DeckModel deck;

        public List<CardModel> cards;

        public ShopView shopView;

        public int restockPrice;

        void Awake()
        {
            foreach (var cardEntry in deck.entries)
            {
                for (int i = 0; i < cardEntry.quantity; i++)
                {
                    cards.Add(Instantiate(cardEntry.card));
                }
            }
        }

        public void Restock(int seed)
        {
            Random.InitState(seed);
            cards.Shuffle();

            if (shopView)
            {
                shopView.UpdateSlotsCard(cards);
            }
        }

        public void RemoveCardById(int cardId)
        {
            var card = cards[cardId];

            cards.Remove(card);

            if (shopView)
            {
                shopView.slots.Find(gameObject => gameObject.GetComponent<SlotView>().cardView.cardModel == card).gameObject.SetActive(false);
            }
        }
    }
}