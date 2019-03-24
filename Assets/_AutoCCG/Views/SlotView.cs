using UnityEngine;

namespace AutoCCG
{
    public class SlotView : MonoBehaviour
    {
        public CardView cardView;

        public int slotId;

        public void Buy()
        {
            var playerModel = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerModel>();
            var cardId = playerModel.shopController.cards.IndexOf(cardView.cardModel);
            playerModel.CmdBuyCardFromId(cardId);
        }
    }
}
