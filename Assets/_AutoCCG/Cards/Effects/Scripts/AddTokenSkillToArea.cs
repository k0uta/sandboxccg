using System.Collections.Generic;
using UnityEngine;

namespace AutoCCG
{
    public class AddTokenSkillToArea : CardEffectModel
    {
        public CardSkillModel tokenSkill;

        public Area area;

        public int tokenAmount;

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
                cardTokenSkill.count+= tokenAmount;
            }
            else
            {
                var newTokenSkill = ScriptableObject.CreateInstance(typeof(CardTokenSkillModel)) as CardTokenSkillModel;
                newTokenSkill.count = tokenAmount;
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
