using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoCCG
{
    public class AddGoldToTarget : CardEffectModel
    {
        public int amount;

        public TargetPlayer target;

        public override List<ActionStepModel> CreateSteps(BattlegroundsCardModel battlegroundsCard)
        {
            var effectSteps = new List<ActionStepModel>();

            var player = battlegroundsCard.GetTargetPlayer(target);

            var goldStep = new ActionStepModel(GoldToTarget(amount, player, battlegroundsCard));
            effectSteps.Add(goldStep);

            return effectSteps;
        }

        IEnumerator GoldToTarget(int gold, PlayerModel player, BattlegroundsCardModel target)
        {
            player.GiveGold(gold);

            var goldIncrementSequence = target.battlegroundsCardView.GetValueIncrementSequence(gold, 0.5f, Color.yellow);

            yield return goldIncrementSequence.Play().WaitForCompletion();
        }
    }
}
