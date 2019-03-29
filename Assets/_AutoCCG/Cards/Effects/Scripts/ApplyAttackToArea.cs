using UnityEngine;

namespace AutoCCG
{
    [CreateAssetMenuAttribute(fileName = "Apply Attack to Area", menuName = "AutoCCG/Effects/Apply Attack to Area")]
    public class ApplyAttackToArea : CardEffectModel
    {
        public Area area;

        public override void Perform(BattlegroundsCardModel battlegroundsCard)
        {
            var areaCards = battlegroundsCard.playerBattlegrounds.enemyBattlegrounds.GetArea(area);
            var damage = battlegroundsCard.cardModel.attack;

            foreach (var card in areaCards)
            {
                card.ApplyDamage(damage);
            }
        }
    }
}
