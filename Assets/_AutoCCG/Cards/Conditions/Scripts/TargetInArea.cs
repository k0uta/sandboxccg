using System.Linq;

namespace AutoCCG
{
    public class TargetInArea : CardConditionModel
    {
        public Target target;

        public Area area;

        public override bool IsMet(BattlegroundsCardModel battlegroundsCard)
        {
            var targetCards = battlegroundsCard.GetTargets(target);
            var areaCards = battlegroundsCard.GetArea(area);

            return areaCards.Intersect(targetCards).Count() == targetCards.Count;
        }
    }
}
