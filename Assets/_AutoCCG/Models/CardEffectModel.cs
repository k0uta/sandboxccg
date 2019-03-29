using UnityEngine;

namespace AutoCCG
{
    public abstract class CardEffectModel : ScriptableObject
    {
        public abstract void Perform(BattlegroundsCardModel battlegroundsCard);
    }
}