using System;
using System.Collections.Generic;
using UnityEngine;

namespace AutoCCG
{
    public class AddTokenSkillToArea : CardEffectModel
    {
        public CardSkillModel tokenSkill;

        public Area area;

        public TargetPlayer targetPlayer;

        public int tokenAmount = 1;

        public override List<ActionStepModel> CreateSteps(BattlegroundsCardModel battlegroundsCard)
        {
            var effectSteps = new List<ActionStepModel>();

            var areaCards = battlegroundsCard.playerBattlegrounds.GetArea(area, targetPlayer);

            foreach (var card in areaCards)
            {
                var tokenStep = new ActionStepModel(() => AddCardTokenSkill(card));
                effectSteps.Add(tokenStep);
            }

            return effectSteps;
        }

        void AddCardTokenSkill(BattlegroundsCardModel targetCard)
        {
            var cardTokenSkill = (CardTokenSkillModel)targetCard.cardModel.cardSkills.Find(FindCardTokenSkill);
            if (cardTokenSkill)
            {
                cardTokenSkill.count += tokenAmount;
            }
            else
            {
                var newTokenSkill = ScriptableObject.CreateInstance(typeof(CardTokenSkillModel)) as CardTokenSkillModel;
                newTokenSkill.count = tokenAmount;
                newTokenSkill.SetBaseSkill(tokenSkill);
                targetCard.cardModel.cardSkills.Add(newTokenSkill);

                ActionStackModel actionStack = ActionStackModel.GetInstance();

                if (actionStack.currentPhase == newTokenSkill.phase)
                {
                    actionStack.actionQueue.Add(newTokenSkill.CreateAction(targetCard));
                }
            }
        }

        bool FindCardTokenSkill(CardSkillModel cardSkill)
        {
            var cardTokenSkill = cardSkill as CardTokenSkillModel;

            return cardTokenSkill != null && cardTokenSkill.baseSkill == tokenSkill;
        }
    }
}
