using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AutoCCG
{
    public class CardView : MonoBehaviour
    {
        public CardModel cardModel;

        public Image cardImage;

        public TextMeshProUGUI cardTitle;

        public TextMeshProUGUI cardAttack;

        public TextMeshProUGUI cardLife;

        public TextMeshProUGUI cardCost;

        // Start is called before the first frame update
        void Start()
        {
            if (cardModel)
            {
                UpdateView();
            }
        }

        public void SetCard(CardModel _cardModel)
        {
            cardModel = _cardModel;
            UpdateView();
        }

        void UpdateView()
        {
            cardImage.sprite = cardModel.sprite;

            cardTitle.text = cardModel.title;
            cardAttack.text = string.Format("Atk\n{0}", cardModel.attack);
            cardLife.text = string.Format("Life\n{0}", cardModel.life);
            cardCost.text = string.Format("Cost\n{0}", cardModel.cost);
        }
    }

}