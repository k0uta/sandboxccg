using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace AutoCCG
{
    public class ShopController : MonoBehaviour
    {
        public List<SlotView> slots;

        public DeckController deckController;

        public PlayerModel playerModel;

        public int restockPrice;

        public TextMeshProUGUI restockCostText;

        public HandModel handModel;

        public void Restock()
        {
            var cards = deckController.cards;
            cards.Shuffle();

            for (int i = 0; i < Mathf.Min(slots.Count, cards.Count); i++)
            {
                var slot = slots[i];
                slot.cardView.SetCard(cards[i]);
                slot.gameObject.SetActive(true);
            }
        }

        void Start()
        {
            for (int i = 0; i < slots.Count; i++)
            {
                slots[i].slotId = i;
            }

            Restock();
            restockCostText.text = string.Format("{0}g", restockPrice);
        }

        public void PayForRestock()
        {
            if (!playerModel.Pay(restockPrice))
            {
                return;
            }

            Restock();
        }

        public void BuyCardById(int slotId)
        {
            var slot = slots[slotId];
            var card = slot.cardView.cardModel;
            if (handModel.IsFull() || !playerModel.Pay(card.cost))
            {
                return;
            }

            slot.gameObject.SetActive(false);

            handModel.AddCard(card);

            // Temp
            GameObject.FindGameObjectWithTag("Enemy").GetComponent<PlayerModel>().handModel.AddCard(card);

            deckController.cards.Remove(card);
        }
    }

}