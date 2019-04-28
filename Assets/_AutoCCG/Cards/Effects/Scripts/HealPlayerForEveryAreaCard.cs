using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace AutoCCG
{
    public class HealPlayerForEveryAreaCard : CardEffectModel
    {
        public Area area;

        public TargetPlayer targetPlayer;

        public override List<ActionStepModel> CreateSteps(BattlegroundsCardModel battlegroundsCard)
        {
            var effectSteps = new List<ActionStepModel>();

            var areaCards = battlegroundsCard.GetArea(area, targetPlayer);

            var healStep = new ActionStepModel(HealPlayer(areaCards.Count, battlegroundsCard,
                battlegroundsCard.GetTargetPlayer(targetPlayer)));
            effectSteps.Add(healStep);

            return effectSteps;
        }

        IEnumerator HealPlayer(int healAmount, BattlegroundsCardModel source, PlayerModel player)
        {
            var sequence = DOTween.Sequence();

            var sourceSequence = DOTween.Sequence();

            sourceSequence.Append(source.battlegroundsCardView.transform.DOPunchScale(new Vector3(0.1f, 0.1f), 0.5f));

            sourceSequence.Insert(0,
                source.battlegroundsCardView.cardView.GetComponent<Image>()
                    .DOColor(Color.blue, sourceSequence.Duration() / 2f).SetLoops(2, LoopType.Yoyo));

            sequence.Append(sourceSequence);

            player.currentHealth += healAmount;

            yield return sequence.Play().WaitForCompletion();
        }
    }
}