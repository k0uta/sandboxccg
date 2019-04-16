using System.Collections;
using System.Collections.Generic;

namespace AutoCCG
{
    public class SpendGoldToUseSkill : CardEffectModel
    {
        public int amount;

        public CardSkillModel skill;

        public override List<ActionStepModel> CreateSteps(BattlegroundsCardModel battlegroundsCard)
        {
            var effectSteps = new List<ActionStepModel>();
            var player = battlegroundsCard.GetTargetPlayer(TargetPlayer.Player);

            var goldStep = new ActionStepModel(GoldStep(amount, player, battlegroundsCard));
            effectSteps.Add(goldStep);

            return effectSteps;
        }

        IEnumerator GoldStep(int goldCost, PlayerModel player, BattlegroundsCardModel target)
        {
            if (!player.Pay(goldCost))
            {
                yield return null;
            }

            yield return skill.CreateAction(target).PerformAction();
        }
    }
}
