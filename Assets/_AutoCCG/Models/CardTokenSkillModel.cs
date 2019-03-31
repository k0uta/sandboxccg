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

        public override void PerformSkill(BattlegroundsCardModel battlegroundsCard)
        {
            base.PerformSkill(battlegroundsCard);
            count--;
        }
    }
}