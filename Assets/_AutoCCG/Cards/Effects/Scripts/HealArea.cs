using System.Collections.Generic;

namespace AutoCCG
{
    public class HealArea : CardEffectModel
    {
        public int amount;

        public Area area;

        public override List<ActionStepModel> CreateSteps(BattlegroundsCardModel battlegroundsCard)
        {
            var effectSteps = new List<ActionStepModel>();

            var areaCards = battlegroundsCard.GetArea(area);

            foreach (var card in areaCards)
            {
                var healStep = new ActionStepModel(() => card.HealDamage(amount));
                effectSteps.Add(healStep);
            }

            return effectSteps;
        }
    }
}
