using System.Collections.Generic;
using UnityEngine;

namespace AutoCCG
{
    public class CardSkillModel : ScriptableObject
    {
        public Sprite sprite;

        public string description;

        public Phase phase;

        public ActionPriority actionPriority;

        public ActionType actionType;

        public Color color = Color.white;

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

        public virtual CardActionModel CreateAction(BattlegroundsCardModel battlegroundsCard)
        {
            CardActionModel cardAction = new CardActionModel(phase, battlegroundsCard, actionType, actionPriority);

            foreach (var effect in effects)
            {
                var effectSteps = effect.CreateSteps(battlegroundsCard);
                cardAction.steps.AddRange(effectSteps);
            }

            return cardAction;
        }
    }
}