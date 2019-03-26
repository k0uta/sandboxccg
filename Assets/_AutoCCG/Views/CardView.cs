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

        public Slider cardLifeSlider;

        public Sprite rangedIcon;

        public Sprite meleeIcon;

        public Image rangeIcon;

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
            cardAttack.text = string.Format("{0}", cardModel.attack);
            cardCost.text = string.Format("{0}g", cardModel.cost);

            rangeIcon.sprite = cardModel.ranged ? rangedIcon : meleeIcon;

            SetCardLife(cardModel.life);
        }

        public void SetCardLife(int currentLife)
        {
            cardLife.text = string.Format("{0}/{1}", currentLife, cardModel.life);
            cardLifeSlider.value = (float)currentLife / (float)cardModel.life;
        }
    }

}