using UnityEngine;

namespace AutoCCG
{
    public class SlotView : MonoBehaviour
    {
        public CardView cardView;

        public int slotId;

        private ShopController shopController;

        private void Awake()
        {
            shopController = GameObject.FindObjectOfType<ShopController>();
        }

        public void Buy()
        {
            shopController.BuyCardById(slotId);
        }
    }
}
