using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AutoCCG
{
    [CreateAssetMenuAttribute(fileName = "Skill", menuName = "AutoCCG/Skill")]
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

        public virtual void PerformSkill(BattlegroundsCardModel battlegroundsCard)
        {
            foreach (var effect in effects)
            {
                effect.Perform(battlegroundsCard);
            }
        }
    }
}