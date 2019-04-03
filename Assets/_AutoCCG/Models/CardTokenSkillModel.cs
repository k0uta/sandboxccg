namespace AutoCCG
{
    public class CardTokenSkillModel : CardSkillModel
    {
        public CardSkillModel baseSkill;

        public void SetBaseSkill(CardSkillModel skill)
        {
            sprite = skill.sprite;
            color = skill.color;
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

        public override CardActionModel CreateAction(BattlegroundsCardModel battlegroundsCard)
        {
            var skillAction = base.CreateAction(battlegroundsCard);

            var decreaseCountStep = new ActionStepModel(() => count--);
            skillAction.steps.Add(decreaseCountStep);

            return skillAction;
        }
    }
}