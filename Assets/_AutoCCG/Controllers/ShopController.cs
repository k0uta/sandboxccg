using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AutoCCG
{
    public class ShopController : MonoBehaviour
    {
        public List<CardModel> cards;

        public ShopView shopView;

        public int restockPrice;

        private int currentTurn;

        private readonly List<float> cardWeights = new List<float>();

        private const int TurnQuotaAmount = 1;

        private const float TurnCardDecay = 0.4f;

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
                var currentWeight = baseWeight * Mathf.Pow(1 - TurnCardDecay, (float) maxCostAllowed - i);
                cardWeights.Add(currentWeight);
                baseWeight -= currentWeight;
            }
        }

        public void SetDeck(DeckModel deck)
        {
            cards = new List<CardModel>();
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

            ShuffleCards();

            if (shopView)
            {
                shopView.UpdateSlotsCard(cards);
            }
        }

        private void ShuffleCards()
        {
            var turnQuotaCards = cards.FindAll((cardModel => cardModel.cost == currentTurn));

            turnQuotaCards.Shuffle();

            if (turnQuotaCards.Count > TurnQuotaAmount)
            {
                turnQuotaCards.RemoveRange(TurnQuotaAmount, turnQuotaCards.Count - TurnQuotaAmount);
            }

            foreach (var quotaCard in turnQuotaCards)
            {
                cards.Remove(quotaCard);
            }

            cards.WeightedShuffle((cardModel) => cardModel.cost > currentTurn ? 0f : cardWeights[cardModel.cost-1]);
            
            foreach (var quotaCard in turnQuotaCards)
            {
                cards.Insert(0, quotaCard);
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