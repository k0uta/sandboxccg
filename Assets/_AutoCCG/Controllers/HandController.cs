using System.Collections.Generic;
using UnityEngine;

namespace AutoCCG
{
    public class HandController : MonoBehaviour
    {
        public int battlegroundCardLimit;

        public HandModel handModel;

        // TODO: This should be handled by the HandView (at least the toggle part)
        // Should be a List<CardModel> maybe?
        public List<HandCardView> battlegroundsQueueCards = new List<HandCardView>();

        public BattlegroundsModel battlegroundsModel;

        public void AddCardToBattlegroundsQueue(HandCardView handCard)
        {
            battlegroundsQueueCards.Add(handCard);

            PurgeExcessBattlegroundsQueue();

            UpdateCardSelectionOrder();
        }

        public void RemoveCardFromBattlegroundsQueue(HandCardView handCard)
        {
            battlegroundsQueueCards.Remove(handCard);

            UpdateCardSelectionOrder();
        }

        void CleanBattlegroundsQueue()
        {
            for (int i = battlegroundsQueueCards.Count - 1; i >= 0; i--)
            {
                battlegroundsQueueCards[i].toggle.isOn = false;
            }
            battlegroundsQueueCards.Clear();
        }

        void PurgeExcessBattlegroundsQueue()
        {
            var excessCardCount = battlegroundsQueueCards.Count - battlegroundCardLimit;
            for (int i = 0; i < excessCardCount; i++)
            {
                var handCard = battlegroundsQueueCards[i];
                handCard.toggle.isOn = false;
            }
        }

        void UpdateCardSelectionOrder()
        {
            for (int i = 0; i < battlegroundsQueueCards.Count; i++)
            {
                battlegroundsQueueCards[i].SetSelectionOrder(i);
            }
        }

        void DisableBattlegroundsQueueCards()
        {
            foreach (var handCard in battlegroundsQueueCards)
            {
                handCard.gameObject.SetActive(false);
            }
        }

        public void EnableBattlegroundsQueueCards()
        {
            foreach (var handCard in battlegroundsQueueCards)
            {
                handCard.gameObject.SetActive(true);
                handCard.cardView.UpdateCardSkills();
            }
        }

        public void SendQueueToBattlegrounds()
        {
            List<CardModel> cards = new List<CardModel>();

            foreach (var handCardView in battlegroundsQueueCards)
            {
                cards.Add(handCardView.cardView.cardModel);
            }

            battlegroundsModel.CreateBattlegroundsCards(cards);

            DisableBattlegroundsQueueCards();
        }
    }
}
