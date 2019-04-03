using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AutoCCG
{
    public class ApplyDamageToSelf : CardEffectModel
    {
        public int amount;

        public override List<ActionStepModel> CreateSteps(BattlegroundsCardModel battlegroundsCard)
        {
            var effectSteps = new List<ActionStepModel>();

            var damageStep = new ActionStepModel(() => battlegroundsCard.ApplyDamage(amount));
            effectSteps.Add(damageStep);

            return effectSteps;
        }

        IEnumerator DamageToSelf(int damage, BattlegroundsCardModel target)
        {
            target.ApplyDamage(damage);
            target.battlegroundsCardView.UpdateView();

            var targetDamageSequence = DOTween.Sequence();

            targetDamageSequence.Append(target.battlegroundsCardView.transform.DOPunchPosition(new Vector3(4f, 0f), 0.5f));

            targetDamageSequence.Insert(0, target.battlegroundsCardView.cardView.GetComponent<Image>().DOColor(Color.red, targetDamageSequence.Duration() / 2f).SetLoops(2, LoopType.Yoyo));

            yield return targetDamageSequence.Play().WaitForCompletion();
        }
    }
}
