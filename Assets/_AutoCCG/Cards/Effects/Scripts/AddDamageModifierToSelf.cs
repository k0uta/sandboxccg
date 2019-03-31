using System;
using System.Collections.Generic;

namespace AutoCCG
{
    public class AddDamageModifierToSelf : CardEffectModel
    {
        public int modifier;

        public override List<CardActionModel> CreateActions(BattlegroundsCardModel battlegroundsCard, Phase phase)
        {
            var effectActions = new List<CardActionModel>();

            Func<int, int> damageReductionFormula = (incomingDamage) => incomingDamage + modifier;

            var addShieldAction = new CardActionModel(phase, () => battlegroundsCard.damageFormulas.Add(damageReductionFormula));
            effectActions.Add(addShieldAction);

            var removeShieldAction = new CardActionModel(Phase.CombatTurnEnd, () => battlegroundsCard.damageFormulas.Remove(damageReductionFormula));
            effectActions.Add(removeShieldAction);

            return effectActions;
        }
    }
}
