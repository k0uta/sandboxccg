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

        public override List<CardActionModel> CreateSkillActions(BattlegroundsCardModel battlegroundsCard)
        {
            var skillActions = base.CreateSkillActions(battlegroundsCard);

            var countDecreaseAction = new CardActionModel(phase, () => DecreaseCountAction(battlegroundsCard));
            skillActions.Add(countDecreaseAction);

            return skillActions;
        }

        void DecreaseCountAction(BattlegroundsCardModel battlegroundsCard)
        {
            count--;
            if (count <= 0)
            {
                battlegroundsCard.cardModel.cardSkills.Remove(this);
            }
        }
    }
}