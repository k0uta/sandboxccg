namespace AutoCCG
{
    public class CardManaSkillModel : CardSkillModel
    {
        public int manaCost;

        public override bool CanBePerformed(BattlegroundsCardModel battlegroundsCard)
        {
            return battlegroundsCard.currentMana >= manaCost && base.CanBePerformed(battlegroundsCard);
        }

        public override CardActionModel CreateAction(BattlegroundsCardModel battlegroundsCard)
        {
            var skillAction = base.CreateAction(battlegroundsCard);

            var manaCostStep = new ActionStepModel(() => battlegroundsCard.currentMana -= manaCost);
            skillAction.steps.Add(manaCostStep);

            return skillAction;
        }
    }
}