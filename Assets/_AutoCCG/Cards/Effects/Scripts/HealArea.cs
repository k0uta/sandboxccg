﻿using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace AutoCCG
{
    public class HealArea : CardEffectModel
    {
        public int amount;

        public Area area;

        public TargetPlayer targetPlayer;

        public override List<ActionStepModel> CreateSteps(BattlegroundsCardModel battlegroundsCard)
        {
            var effectSteps = new List<ActionStepModel>();

            var areaCards = battlegroundsCard.GetArea(area, targetPlayer);

            var healStep = new ActionStepModel(HealCards(amount, battlegroundsCard, areaCards));
            effectSteps.Add(healStep);

            return effectSteps;
        }

        IEnumerator HealCards(int healAmount, BattlegroundsCardModel source, List<BattlegroundsCardModel> targets)
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
                var totalHeal = target.HealDamage(healAmount);
                target.battlegroundsCardView.UpdateView();

                var targetHealSequence =
                    target.battlegroundsCardView.GetValueIncrementSequence(totalHeal, sourceSequence.Duration());
                sequence.Insert(0, targetHealSequence);
            }

            yield return sequence.Play().WaitForCompletion();
        }
    }
}