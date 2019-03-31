using System.Collections.Generic;
using UnityEngine;

namespace AutoCCG
{
    [CreateAssetMenuAttribute(fileName = "Apply Attack to Area", menuName = "AutoCCG/Effects/Apply Attack to Area")]
    public class ApplyAttackToArea : CardEffectModel
    {
        public Area area;

        public override List<CardActionModel> CreateActions(BattlegroundsCardModel battlegroundsCard, Phase phase)
        {
            var areaCards = battlegroundsCard.playerBattlegrounds.enemyBattlegrounds.GetArea(area);
            var damage = battlegroundsCard.cardModel.attack;

            var effectActions = new List<CardActionModel>();

            foreach (var card in areaCards)
            {
                var damageAction = new CardActionModel(phase, () => card.ApplyDamage(damage));
                effectActions.Add(damageAction);
            }

            return effectActions;
        }
    }
}
