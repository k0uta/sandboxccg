using System.Collections.Generic;

namespace AutoCCG
{
    public class HealArea : CardEffectModel
    {
        public int amount;

        public Area area;

        public override List<CardActionModel> CreateActions(BattlegroundsCardModel battlegroundsCard, Phase phase)
        {
            var areaCards = battlegroundsCard.playerBattlegrounds.GetArea(area);
            var effectActions = new List<CardActionModel>();

            foreach (var card in areaCards)
            {
                var healAction = new CardActionModel(phase, battlegroundsCard, () => card.HealDamage(amount));
                effectActions.Add(healAction);
            }

            return effectActions;
        }
    }
}
