using System;
using System.Collections.Generic;

namespace AutoCCG
{
    public class AddActionFilterToStack : CardEffectModel
    {
        public ActionType typeFilter;

        public override List<ActionStepModel> CreateSteps(BattlegroundsCardModel battlegroundsCard)
        {
            var effectSteps = new List<ActionStepModel>();

            Func<CardActionModel, bool> filter = (cardAction) => ActionFilterValidation(cardAction, battlegroundsCard);

            var filterStep = new ActionStepModel(() => ActionStackModel.GetInstance().actionValidations.Add(filter), () => ActionStackModel.GetInstance().actionValidations.Remove(filter));
            effectSteps.Add(filterStep);

            return effectSteps;
        }

        bool ActionFilterValidation(CardActionModel cardAction, BattlegroundsCardModel target)
        {
            return cardAction.source != target || cardAction.actionType != typeFilter;
        }
    }
}
