using System.Collections.Generic;
using UnityEngine;

namespace AutoCCG
{
    public class HandView : MonoBehaviour
    {
        public List<HandCardView> cards;

        public GameObject cardPrefab;

        public Transform cardArea;

        public HandController handController;

        int internalCounter = 0;

        public void SpawnCard(CardModel cardModel)
        {
            var card = Instantiate(cardPrefab, cardArea);
            card.name = string.Format("Card {0} ({1})", ++internalCounter, cardModel.name);
            var handCardView = card.GetComponent<HandCardView>();

            handCardView.handController = handController;
            handCardView.cardView.SetCard(cardModel);
            cards.Add(handCardView);
        }

        public void RemoveCard(CardModel cardModel)
        {
            var cardView = cards.Find(card => card.cardView.cardModel == cardModel);
            cards.Remove(cardView);
            Destroy(cardView.gameObject);
        }
    }
}