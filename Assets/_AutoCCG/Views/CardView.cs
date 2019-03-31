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

        public TextMeshProUGUI cardMana;

        public TextMeshProUGUI cardCost;

        public Slider cardLifeSlider;

        public Slider cardManaSlider;

        public Transform skillsArea;

        float skillItemSize = 15f;

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

            UpdateCardSkills();

            SetCardLife(cardModel.life);
            SetCardMana(cardModel.mana);
        }

        public void UpdateCardSkills()
        {
            for (int i = skillsArea.childCount - 1; i >= 0; i--)
            {
                Destroy(skillsArea.GetChild(i).gameObject);
            }

            foreach (var skill in cardModel.cardSkills)
            {
                GameObject skillImageObject = new GameObject();
                var skillImage = skillImageObject.AddComponent<Image>();
                skillImage.sprite = skill.sprite;
                skillImage.preserveAspect = true;
                skillImageObject.GetComponent<RectTransform>().sizeDelta = new Vector2(skillItemSize, skillItemSize);
                skillImageObject.transform.SetParent(skillsArea);
            }
        }

        public void SetCardLife(int currentLife)
        {
            cardLife.text = string.Format("{0}/{1}", currentLife, cardModel.life);
            cardLifeSlider.value = (float)currentLife / (float)cardModel.life;
        }

        public void SetCardMana(int currentMana)
        {
            cardMana.text = string.Format("{0}/{1}", currentMana, cardModel.mana);
            cardManaSlider.value = (float)currentMana / (float)cardModel.mana;
        }
    }

}