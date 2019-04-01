using System.Collections.Generic;
using UnityEngine;

namespace AutoCCG
{
    public abstract class CardEffectModel : ScriptableObject
    {
        public abstract List<ActionStepModel> CreateSteps(BattlegroundsCardModel battlegroundsCard);
    }
}