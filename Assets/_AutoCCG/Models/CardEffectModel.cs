using System.Collections.Generic;
using UnityEngine;

namespace AutoCCG
{
    public abstract class CardEffectModel : ScriptableObject
    {
        public abstract List<CardActionModel> CreateActions(BattlegroundsCardModel battlegroundsCard, Phase phase);
    }
}