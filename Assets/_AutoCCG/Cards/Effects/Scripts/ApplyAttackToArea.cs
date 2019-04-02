using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoCCG
{
    public class ApplyAttackToArea : CardEffectModel
    {
        public Area area;

        public TargetPlayer target;

        public override List<ActionStepModel> CreateSteps(BattlegroundsCardModel battlegroundsCard)
        {
            var effectSteps = new List<ActionStepModel>();

            var areaCards = battlegroundsCard.playerBattlegrounds.GetArea(area, target);
            var damage = battlegroundsCard.cardModel.attack;

            foreach (var card in areaCards)
            {
                var damageStep = new ActionStepModel(() => card.ApplyDamage(damage));
                effectSteps.Add(damageStep);
                var visualEffectStep = new ActionStepModel(VisualEffect(card));
                effectSteps.Add(visualEffectStep);
            }

            return effectSteps;
        }

        IEnumerator VisualEffect(BattlegroundsCardModel targetCard)
        {
            iTween.PunchScale(targetCard.battlegroundsCardView.gameObject, new Vector3(0.5f, 0.5f), 0.5f);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
