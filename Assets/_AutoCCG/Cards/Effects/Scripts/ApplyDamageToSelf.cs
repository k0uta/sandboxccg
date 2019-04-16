using DG.Tweening;
using System.Collections;
using System.Collections.Generic;

namespace AutoCCG
{
    public class ApplyDamageToSelf : CardEffectModel
    {
        public int amount;

        public override List<ActionStepModel> CreateSteps(BattlegroundsCardModel battlegroundsCard)
        {
            var effectSteps = new List<ActionStepModel>();

            var damageStep = new ActionStepModel(DamageToSelf(amount, battlegroundsCard));
            effectSteps.Add(damageStep);

            return effectSteps;
        }

        IEnumerator DamageToSelf(int damage, BattlegroundsCardModel target)
        {
            var totalDamage = target.ApplyDamage(damage);
            target.battlegroundsCardView.UpdateView();

            var targetDamageSequence = target.battlegroundsCardView.GetValueIncrementSequence(-totalDamage, 0.5f);

            yield return targetDamageSequence.Play().WaitForCompletion();
        }
    }
}
