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
            var cards = battlegroundsCard.GetArea(Area.Battlegrounds, targetPlayer);

            foreach (var card in cards)
            {
                if (!card.IsDead()) continue;

                var modifierStep = new ActionStepModel(() => battlegroundsCard.cardModel.attack += modifier);
                effectSteps.Add(modifierStep);
            }

            return effectSteps;
        }
    }
}