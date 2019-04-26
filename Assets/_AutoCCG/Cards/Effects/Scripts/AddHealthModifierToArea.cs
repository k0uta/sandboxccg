using System;
using System.Collections.Generic;

namespace AutoCCG
{
    public class AddHealthModifierToArea : CardEffectModel
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
                void ReverseStep() => card.cardModel.life -= modifier;
                var modifierStep = new ActionStepModel(() => card.cardModel.life += modifier, !permanent ? (Action) ReverseStep : null);
                effectSteps.Add(modifierStep);
            }

            return effectSteps;
        }
    }
}
