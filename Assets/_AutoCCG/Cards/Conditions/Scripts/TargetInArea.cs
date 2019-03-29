using System.Linq;
using UnityEngine;

namespace AutoCCG
{
    [CreateAssetMenuAttribute(fileName = "TargetInArea", menuName = "AutoCCG/Conditions/Target in Area")]
    public class TargetInArea : CardConditionModel
    {
        public Target target;

        public Area area;

        public override bool IsMet(BattlegroundsCardModel battlegroundsCard)
        {
            var targetCards = battlegroundsCard.GetTargets(target);
            var areaCards = battlegroundsCard.playerBattlegrounds.GetArea(area);

            return areaCards.Intersect(targetCards).Count() == targetCards.Count;
        }
    }
}
