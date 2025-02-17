﻿using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AutoCCG
{
    public class ApplyDamageToArea : CardEffectModel
    {
        public Area area;

        public TargetPlayer target;

        public bool useAttack;

        public int amount;

        public override List<ActionStepModel> CreateSteps(BattlegroundsCardModel battlegroundsCard)
        {
            var effectSteps = new List<ActionStepModel>();

            var damage = useAttack ? battlegroundsCard.cardModel.Attack : amount;

            if (damage <= 0)
            {
                return effectSteps;
            }

            var areaCards = battlegroundsCard.GetArea(area, target);
            var damageStep = new ActionStepModel(DamageToArea(damage, battlegroundsCard, areaCards));
            effectSteps.Add(damageStep);

            return effectSteps;
        }

        IEnumerator DamageToArea(int damage, BattlegroundsCardModel source, List<BattlegroundsCardModel> targets)
        {
            var sequence = DOTween.Sequence();

            var sourceSequence = DOTween.Sequence();

            sourceSequence.Append(source.battlegroundsCardView.transform.DOPunchScale(new Vector3(0.1f, 0.1f), 0.5f));

            sourceSequence.Insert(0,
                source.battlegroundsCardView.cardView.GetComponent<Image>()
                    .DOColor(Color.blue, sourceSequence.Duration() / 2f).SetLoops(2, LoopType.Yoyo));

            sequence.Append(sourceSequence);

            foreach (var target in targets)
            {
                var totalDamage = target.ApplyDamage(damage);
                target.battlegroundsCardView.UpdateView();

                var targetDamageSequence =
                    target.battlegroundsCardView.GetValueIncrementSequence(-totalDamage, sourceSequence.Duration());
                sequence.Insert(0, targetDamageSequence);
            }

            yield return sequence.Play().WaitForCompletion();
        }
    }
}