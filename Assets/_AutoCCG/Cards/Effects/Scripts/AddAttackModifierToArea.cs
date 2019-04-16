using System;
using System.Collections.Generic;

namespace AutoCCG
{
    public class AddAttackModifierToArea : CardEffectModel
    {
        public int modifier;

        public bool permanent;

        public Area area;

        public TargetPlayer targetPlayer;

        public override List<ActionStepModel> CreateSteps(BattlegroundsCardModel battlegroundsCard)
        {
            var effectSteps = new List<ActionStepModel>();

            var areaCards = battlegroundsCard.GetArea(area, targetPlayer);

            foreach (var card in areaCards)
            {
                Action reverseStep = () => card.cardModel.attack -= modifier;
                var modifierStep = new ActionStepModel(() => card.cardModel.attack += modifier, !permanent ? reverseStep : null);
                effectSteps.Add(modifierStep);
            }

            return effectSteps;
        }
    }
}
