using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AutoCCG
{
    public class HandCardView : MonoBehaviour
    {
        public CardView cardView;

        public Toggle toggle;

        public TextMeshProUGUI selectionOrderText;

        public int selectionOrder;

        public HandView handView;

        void Awake()
        {
            toggle = GetComponent<Toggle>();
            toggle.onValueChanged.AddListener(OnToggleValueChanged);

            UpdateVisualState();
        }

        void OnToggleValueChanged(bool isOn)
        {
            if (toggle.isOn) {
                handView.AddCardToBattlegroundsQueue(this);
            } else
            {
                handView.RemoveCardFromBattlegroundsQueue(this);
            }
            UpdateVisualState();
        }

        void UpdateVisualState()
        {
            var colors = toggle.colors;
            var normalColor = colors.normalColor;

            normalColor.a = toggle.isOn ? 1f : 0f;

            colors.normalColor = normalColor;
            toggle.colors = colors;

            selectionOrderText.alpha = toggle.isOn ? 0.75f : 0f;
        }

        public void SetSelectionOrder(int order)
        {
            selectionOrder = order;
            selectionOrderText.text = (order + 1).ToString();
        }
    }
}