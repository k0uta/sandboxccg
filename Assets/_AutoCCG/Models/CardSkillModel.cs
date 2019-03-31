using System.Collections.Generic;
using UnityEngine;

namespace AutoCCG
{
    //[CreateAssetMenuAttribute(fileName = "Skill", menuName = "AutoCCG/Skill")]
    public class CardSkillModel : ScriptableObject
    {
        public Sprite sprite;

        public string description;

        public Phase phase;

        [Expandable]
        public List<CardConditionModel> conditions;

        [Expandable]
        public List<CardEffectModel> effects;

        public virtual bool CanBePerformed(BattlegroundsCardModel battlegroundsCard)
        {
            foreach (var condition in conditions)
            {
                if (!condition.IsMet(battlegroundsCard))
                {
                    return false;
                }
            }

            return true;
        }

        public virtual List<CardActionModel> CreateSkillActions(BattlegroundsCardModel battlegroundsCard)
        {
            List<CardActionModel> skillActions = new List<CardActionModel>();
            foreach (var effect in effects)
            {
                skillActions.AddRange(effect.CreateActions(battlegroundsCard, phase));
            }

            return skillActions;
        }
    }
}