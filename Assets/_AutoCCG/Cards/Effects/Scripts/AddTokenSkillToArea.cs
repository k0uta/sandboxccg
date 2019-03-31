using System.Collections.Generic;
using UnityEngine;

namespace AutoCCG
{
    public class AddTokenSkillToArea : CardEffectModel
    {
        [Expandable]
        public CardSkillModel tokenSkill;

        public Area area;

        public override List<CardActionModel> CreateActions(BattlegroundsCardModel battlegroundsCard, Phase phase)
        {
            var effectActions = new List<CardActionModel>();
            var areaCards = battlegroundsCard.playerBattlegrounds.enemyBattlegrounds.GetArea(area);

            foreach (var card in areaCards)
            {
                var skillAction = new CardActionModel(phase, () => AddCardTokenSkill(card));
                effectActions.Add(skillAction);
            }

            return effectActions;
        }

        void AddCardTokenSkill(BattlegroundsCardModel targetCard)
        {
            var cardTokenSkill = (CardTokenSkillModel)targetCard.cardModel.cardSkills.Find(FindCardTokenSkill);
            if (cardTokenSkill)
            {
                cardTokenSkill.count++;
            }
            else
            {
                var newTokenSkill = ScriptableObject.CreateInstance(typeof(CardTokenSkillModel)) as CardTokenSkillModel;
                newTokenSkill.count += 3;
                newTokenSkill.SetBaseSkill(tokenSkill);
                targetCard.cardModel.cardSkills.Add(newTokenSkill);
            }
        }

        bool FindCardTokenSkill(CardSkillModel cardSkill)
        {
            var cardTokenSkill = cardSkill as CardTokenSkillModel;

            return cardTokenSkill != null && cardTokenSkill.baseSkill == tokenSkill;
        }
    }
}
