using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AutoCCG
{
    public class CardInspectorView : MonoBehaviour
    {
        public CardView cardView;

        public GameObject skillInfoPrefab;

        public static CardInspectorView instance;

        private CardModel currentCard;

        private bool visible;

        private List<GameObject> skills = new List<GameObject>();

        public static CardInspectorView GetInstance()
        {
            return instance;
        }

        private void OnEnable()
        {
            instance = this;
            UpdateInspectorArea();
        }

        public void Inspect(CardModel card)
        {
            visible = true;

            if (card == currentCard)
            {
                UpdateInspectorArea();
                return;
            }

            currentCard = card;

            UpdateInspectorArea();
        }

        public void Uninspect(CardModel card)
        {
            if (card != currentCard)
            {
                return;
            }

            visible = false;

            currentCard = null;

            UpdateInspectorArea();
        }

        void UpdateInspectorArea()
        {
            GetComponent<CanvasGroup>().alpha = visible ? 1f : 0f;

            if (!visible)
            {
                return;
            }

            if (cardView.cardModel != currentCard)
            {
                cardView.SetCard(currentCard);
            }

            cardView.UpdateCardSkills();

            UpdateSkills();
        }

        void UpdateSkills()
        {
            foreach (var skill in skills)
            {
                Destroy(skill);
            }

            skills.Clear();

            foreach (var skillModel in currentCard.cardSkills)
            {
                var skill = Instantiate(skillInfoPrefab, this.transform);
                skills.Add(skill);
                skill.GetComponent<SkillInfoView>().SetSkill(skillModel);
            }

            LayoutRebuilder.ForceRebuildLayoutImmediate(transform.GetComponent<RectTransform>());
        }
    }
}