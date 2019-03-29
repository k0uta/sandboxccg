using UnityEngine;

namespace AutoCCG
{
    public abstract class CardConditionModel : ScriptableObject
    {
        public abstract bool IsMet(BattlegroundsCardModel battlegroundsCard);
    }
}