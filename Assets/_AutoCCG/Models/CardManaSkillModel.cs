using System.Collections.Generic;
using UnityEngine;

namespace AutoCCG
{
    //[CreateAssetMenuAttribute(fileName = "Mana Skill", menuName = "AutoCCG/Mana Skill")]
    public class CardManaSkillModel : CardSkillModel
    {
        public int manaCost;

        public override bool CanBePerformed(BattlegroundsCardModel battlegroundsCard)
        {
            return battlegroundsCard.currentMana >= manaCost && base.CanBePerformed(battlegroundsCard);
        }

        public override List<CardActionModel> CreateSkillActions(BattlegroundsCardModel battlegroundsCard)
        {
            var skillActions = base.CreateSkillActions(battlegroundsCard);

            var manaCostAction = new CardActionModel(phase, () => battlegroundsCard.currentMana -= manaCost);
            skillActions.Add(manaCostAction);

            return skillActions;
        }
    }
}