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

            Func<int, int> damageModifierFormula = (incomingDamage) => incomingDamage + modifier;

            var addModifierAction = new CardActionModel(phase, battlegroundsCard, () => battlegroundsCard.damageFormulas.Add(damageModifierFormula), ActionType.Passive);
            effectActions.Add(addModifierAction);

            var removeModifierAction = new CardActionModel(Phase.CombatTurnEnd, battlegroundsCard, () => battlegroundsCard.damageFormulas.Remove(damageModifierFormula), ActionType.Passive);
            effectActions.Add(removeModifierAction);

            return effectActions;
        }
    }
}
