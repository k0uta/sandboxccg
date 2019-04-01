namespace AutoCCG
{
    public class TargetIsDead : CardConditionModel
    {
        public Target target;

        public override bool IsMet(BattlegroundsCardModel battlegroundsCard)
        {
            var targetCards = battlegroundsCard.GetTargets(target);

            foreach (var card in targetCards)
            {
                if (!card.IsDead())
                {
                    return false;
                }
            }

            return true;
        }
    }
}
