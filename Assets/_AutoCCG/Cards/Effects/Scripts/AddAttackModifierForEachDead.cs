using System.Collections.Generic;

namespace AutoCCG
{
    public class AddAttackModifierForEachDead : CardEffectModel
    {
        public TargetPlayer targetPlayer;

        public int modifier;

        public override List<ActionStepModel> CreateSteps(BattlegroundsCardModel battlegroundsCard)
        {
            var effectSteps = new List<ActionStepModel>();
            var cards = battlegroundsCard.playerBattlegrounds.deadCards;

            foreach (var card in cards)
            {
                var modifierStep = new ActionStepModel(() => battlegroundsCard.cardModel.Attack += modifier);
                effectSteps.Add(modifierStep);
            }

            return effectSteps;
        }
    }
}