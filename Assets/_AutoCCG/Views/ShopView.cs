using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace AutoCCG
{
    public class ShopView : MonoBehaviour
    {
        public List<SlotView> slots;

        public TextMeshProUGUI restockCostText;

        public void UpdateSlotsCard(List<CardModel> cards)
        {
            for (int i = 0; i < Mathf.Min(slots.Count, cards.Count); i++)
            {
                var slot = slots[i];
                slot.cardView.SetCard(cards[i]);
                slot.gameObject.SetActive(true);
            }
        }

        void Awake()
        {
            for (int i = 0; i < slots.Count; i++)
            {
                slots[i].slotId = i;
            }
        }

        public void PayForRestock()
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerModel>().CmdPayForRestock();
        }
    }

}