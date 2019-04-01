﻿using System.Collections.Generic;

namespace AutoCCG
{
    public class ApplyAttackToArea : CardEffectModel
    {
        public Area area;

        public override List<ActionStepModel> CreateSteps(BattlegroundsCardModel battlegroundsCard)
        {
            var effectSteps = new List<ActionStepModel>();

            var areaCards = battlegroundsCard.playerBattlegrounds.enemyBattlegrounds.GetArea(area);
            var damage = battlegroundsCard.cardModel.attack;

            foreach (var card in areaCards)
            {
                var damageStep = new ActionStepModel(() => card.ApplyDamage(damage));
                effectSteps.Add(damageStep);
            }

            return effectSteps;
        }
    }
}
