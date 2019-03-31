using UnityEngine;

namespace AutoCCG
{
    public class HealArea : CardEffectModel
    {
        public int amount;

        public Area area;

        public override void Perform(BattlegroundsCardModel battlegroundsCard)
        {
            var areaCards = battlegroundsCard.playerBattlegrounds.GetArea(area);

            foreach (var card in areaCards)
            {
                card.HealDamage(amount);
            }
        }
    }
}
