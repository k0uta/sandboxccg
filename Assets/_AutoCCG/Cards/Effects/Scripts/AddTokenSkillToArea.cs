using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AutoCCG
{
    public class AddTokenSkillToArea : CardEffectModel
    {
        public CardSkillModel tokenSkill;

        public Area area;

        public TargetPlayer targetPlayer;

        public int tokenAmount = 1;

        public override List<ActionStepModel> CreateSteps(BattlegroundsCardModel battlegroundsCard)
        {
            var effectSteps = new List<ActionStepModel>();

            var areaCards = battlegroundsCard.GetArea(area, targetPlayer);

            var addTokensStep = new ActionStepModel(AddTokens(battlegroundsCard, areaCards));
            effectSteps.Add(addTokensStep);

            return effectSteps;
        }

        void AddCardTokenSkill(BattlegroundsCardModel targetCard)
        {
            var cardTokenSkill = (CardTokenSkillModel)targetCard.cardModel.cardSkills.Find(FindCardTokenSkill);
            if (cardTokenSkill)
            {
                cardTokenSkill.count += tokenAmount;
            }
            else
            {
                var newTokenSkill = ScriptableObject.CreateInstance(typeof(CardTokenSkillModel)) as CardTokenSkillModel;
                newTokenSkill.count = tokenAmount;
                newTokenSkill.SetBaseSkill(tokenSkill);
                targetCard.cardModel.cardSkills.Add(newTokenSkill);

                ActionStackModel actionStack = ActionStackModel.GetInstance();

                if (actionStack.currentPhase == newTokenSkill.phase)
                {
                    actionStack.actionQueue.Add(newTokenSkill.CreateAction(targetCard));
                }
            }
        }

        IEnumerator AddTokens(BattlegroundsCardModel source, List<BattlegroundsCardModel> targets)
        {
            var sequence = DOTween.Sequence();

            var sourceSequence = DOTween.Sequence();

            sourceSequence.Append(source.battlegroundsCardView.transform.DOPunchScale(new Vector3(0.1f, 0.1f), 0.5f));

            sourceSequence.Insert(0, source.battlegroundsCardView.cardView.GetComponent<Image>().DOColor(Color.blue, sourceSequence.Duration() / 2f).SetLoops(2, LoopType.Yoyo));

            sequence.Append(sourceSequence);

            foreach (var target in targets)
            {
                AddCardTokenSkill(target);
                target.battlegroundsCardView.UpdateView();

                var targetTokenSequence = DOTween.Sequence();

                targetTokenSequence.Append(target.battlegroundsCardView.transform.DOPunchPosition(new Vector3(4f, 0f), sequence.Duration()));

                if (target != source)
                {
                    targetTokenSequence.Insert(0, target.battlegroundsCardView.cardView.GetComponent<Image>().DOColor(targetPlayer == TargetPlayer.Enemy ? Color.magenta : Color.green, targetTokenSequence.Duration() / 2f).SetLoops(2, LoopType.Yoyo));
                }

                sequence.Insert(0, targetTokenSequence);
            }

            yield return sequence.Play().WaitForCompletion();
        }

        bool FindCardTokenSkill(CardSkillModel cardSkill)
        {
            var cardTokenSkill = cardSkill as CardTokenSkillModel;

            return cardTokenSkill != null && cardTokenSkill.baseSkill == tokenSkill;
        }
    }
}
