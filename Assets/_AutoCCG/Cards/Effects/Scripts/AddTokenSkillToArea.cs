using System.Collections.Generic;
using UnityEngine;

namespace AutoCCG
{
    public class AddTokenSkillToArea : CardEffectModel
    {
        public CardSkillModel tokenSkill;

        public Area area;

        public TargetPlayer targetPlayer;

        public int tokenAmount;

        public override List<CardActionModel> CreateActions(BattlegroundsCardModel battlegroundsCard, Phase phase)
        {
            var effectActions = new List<CardActionModel>();

            var areaCards = battlegroundsCard.playerBattlegrounds.GetArea(area, targetPlayer);

            foreach (var card in areaCards)
            {
                var skillAction = new CardActionModel(phase, () => AddCardTokenSkill(card, phase));
                effectActions.Add(skillAction);
            }

            return effectActions;
        }

        void AddCardTokenSkill(BattlegroundsCardModel targetCard, Phase phase)
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
                if (phase == newTokenSkill.phase)
                {
                    ActionStackModel.GetInstance().actionQueue.AddRange(newTokenSkill.CreateSkillActions(targetCard));
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
