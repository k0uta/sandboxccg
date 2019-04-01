using System;
using System.Collections.Generic;

namespace AutoCCG
{
    public class AddDamageModifierToSelf : CardEffectModel
    {
        public int modifier;

        public override List<ActionStepModel> CreateSteps(BattlegroundsCardModel battlegroundsCard)
        {
            var effectSteps = new List<ActionStepModel>();

            Func<int, int> damageModifierFormula = (incomingDamage) => incomingDamage + modifier;

            var modifierStep = new ActionStepModel(() => battlegroundsCard.damageFormulas.Add(damageModifierFormula), () => battlegroundsCard.damageFormulas.Remove(damageModifierFormula));
            effectSteps.Add(modifierStep);

            return effectSteps;
        }
    }
}
