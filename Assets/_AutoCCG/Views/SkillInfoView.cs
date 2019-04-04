using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AutoCCG
{
    public class SkillInfoView : MonoBehaviour
    {
        public Image icon;

        public TextMeshProUGUI titleText;

        public TextMeshProUGUI descriptionText;

        public TextMeshProUGUI phaseText;

        public GameObject manaCostArea;

        public TextMeshProUGUI manaCostText;

        public void SetSkill(CardSkillModel skillModel)
        {
            icon.sprite = skillModel.sprite;
            icon.color = skillModel.color;

            titleText.text = skillModel.title;
            descriptionText.text = skillModel.description;

            phaseText.text = ToSentenceCase(skillModel.phase.ToString());

            var manaSkill = skillModel as CardManaSkillModel;

            if (manaSkill != null)
            {
                manaCostArea.SetActive(true);
                manaCostText.text = string.Format("{0} mana", manaSkill.manaCost);
            } else
            {
                manaCostArea.SetActive(false);
            }
        }

        string ToSentenceCase(string str)
        {
            return Regex.Replace(str, "[a-z][A-Z]", m => $"{m.Value[0]} {char.ToLower(m.Value[1])}");
        }
    }
}
