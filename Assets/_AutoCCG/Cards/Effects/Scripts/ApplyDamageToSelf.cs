﻿using System.Collections.Generic;

namespace AutoCCG
{
    public class ApplyDamageToSelf : CardEffectModel
    {
        public int amount;

        public override List<CardActionModel> CreateActions(BattlegroundsCardModel battlegroundsCard, Phase phase)
        {
            var effectActions = new List<CardActionModel>();

            var selfDamageAction = new CardActionModel(phase, () => battlegroundsCard.ApplyDamage(amount));
            effectActions.Add(selfDamageAction);

            return effectActions;
        }
    }
}
