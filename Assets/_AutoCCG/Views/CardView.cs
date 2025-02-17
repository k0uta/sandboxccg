﻿using TMPro;
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

        float skillItemSize = 20f;

        public void SetCard(CardModel _cardModel)
        {
            if (cardModel != null)
            {
                cardModel.UpdateEvent -= UpdateView;
            }
            cardModel = _cardModel;

            cardModel.UpdateEvent += UpdateView;
            UpdateView();
        }

        public void UpdateView()
        {
            cardImage.sprite = cardModel.sprite;

            cardTitle.text = cardModel.title;
            cardAttack.text = string.Format("{0}", cardModel.Attack);
            cardCost.text = string.Format("{0}g", cardModel.cost);

            UpdateCardSkills();

            SetCardLife(cardModel.Life);
            SetCardMana(cardModel.mana);
        }

        public void UpdateCardSkills()
        {
            if (skillsArea == null)
            {
                return;
            }

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
                skillImage.color = skill.color;
            }
        }

        public void SetCardLife(int currentLife)
        {
            cardLife.text = string.Format("{0}/{1}", currentLife, cardModel.Life);
            cardLifeSlider.value = (float)currentLife / (float)cardModel.Life;
        }

        public void SetCardMana(int currentMana)
        {
            cardMana.text = string.Format("{0}/{1}", currentMana, cardModel.mana);
            cardManaSlider.value = cardModel.mana == 0 ? 1.0f : (float)currentMana / (float)cardModel.mana;
        }

        public void Inspect()
        {
            CardInspectorView.GetInstance().Inspect(cardModel);
        }

        public void Uninspect()
        {
            CardInspectorView.GetInstance().Uninspect(cardModel);
        }
    }

}