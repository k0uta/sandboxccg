namespace AutoCCG
{
    public class ApplyDamageToSelf : CardEffectModel
    {
        public int amount;

        public override void Perform(BattlegroundsCardModel battlegroundsCard)
        {
            battlegroundsCard.actionQueue.Add(() => battlegroundsCard.ApplyDamage(amount));
        }
    }
}
