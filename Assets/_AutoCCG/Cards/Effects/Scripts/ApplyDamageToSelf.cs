using System.Collections.Generic;

namespace AutoCCG
{
    public class ApplyDamageToSelf : CardEffectModel
    {
        public int amount;

        public override List<ActionStepModel> CreateSteps(BattlegroundsCardModel battlegroundsCard)
        {
            var effectSteps = new List<ActionStepModel>();

            var damageStep = new ActionStepModel(() => battlegroundsCard.ApplyDamage(amount));
            effectSteps.Add(damageStep);

            return effectSteps;
        }
    }
}
