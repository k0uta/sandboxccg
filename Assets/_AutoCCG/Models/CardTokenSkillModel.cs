using System.Collections.Generic;

namespace AutoCCG
{
    public class CardTokenSkillModel : CardSkillModel
    {
        public CardSkillModel baseSkill;

        public void SetBaseSkill(CardSkillModel skill)
        {
            sprite = skill.sprite;
            description = skill.description;
            phase = skill.phase;
            conditions = skill.conditions;
            effects = skill.effects;
            count = 1;

            baseSkill = skill;
        }

        public int count;

        public override bool CanBePerformed(BattlegroundsCardModel battlegroundsCard)
        {
            return count > 0 && base.CanBePerformed(battlegroundsCard);
        }

        public override List<CardActionModel> CreateSkillActions(BattlegroundsCardModel battlegroundsCard)
        {
            var skillActions = base.CreateSkillActions(battlegroundsCard);

            var countDecreaseAction = new CardActionModel(phase, () => count--);
            skillActions.Add(countDecreaseAction);

            var checkForRemovalAction = new CardActionModel(Phase.CombatTurnEnd, () => CheckForRemoval(battlegroundsCard));
            skillActions.Add(checkForRemovalAction);

            return skillActions;
        }

        void CheckForRemoval(BattlegroundsCardModel battlegroundsCard)
        {
            if (count <= 0)
            {
                battlegroundsCard.cardModel.cardSkills.Remove(this);
            }
        }
    }
}