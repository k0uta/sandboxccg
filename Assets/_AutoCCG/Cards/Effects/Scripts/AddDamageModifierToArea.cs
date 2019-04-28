using System.Collections.Generic;

namespace AutoCCG
{
    public class AddDamageModifierToArea : CardEffectModel
    {
        public int modifier;

        public Area area;

        public TargetPlayer targetPlayer;

        public override List<ActionStepModel> CreateSteps(BattlegroundsCardModel battlegroundsCard)
        {
            var effectSteps = new List<ActionStepModel>();

            var areaCards = battlegroundsCard.GetArea(area, targetPlayer);

            int DamageModifierFormula(int incomingDamage) => incomingDamage + modifier;

            foreach (var target in areaCards)
            {
                var modifierStep = new ActionStepModel(() => target.damageFormulas.Add(DamageModifierFormula),
                    () => target.damageFormulas.Remove(DamageModifierFormula));
                effectSteps.Add(modifierStep);
            }

            return effectSteps;
        }
    }
}