using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AutoCCG
{
    public class ShopController : MonoBehaviour
    {
        public DeckModel deck;

        public List<CardModel> cards;

        public ShopView shopView;

        public int restockPrice;

        private int currentTurn;

        private readonly List<float> cardWeights = new List<float>();

        public int CurrentTurn
        {
            set
            {
                currentTurn = value;
                RecalculateWeights();
            }
        }

        private void RecalculateWeights()
        {
            cardWeights.Clear();

            var baseWeight = 1.0f;
            var maxCost = cards.Max(cardModel => cardModel.cost);
            var maxCostAllowed = Mathf.Min(currentTurn, maxCost);

            for (var i = 1; i <= maxCostAllowed; i++)
            {
                var currentWeight = baseWeight * Mathf.Pow(0.75f, (float) maxCostAllowed - i);
                cardWeights.Add(currentWeight);
                baseWeight -= currentWeight;
            }
        }

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
            cards.WeightedShuffle((cardModel) => cardModel.cost > currentTurn ? 0f : cardWeights[cardModel.cost-1]);

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