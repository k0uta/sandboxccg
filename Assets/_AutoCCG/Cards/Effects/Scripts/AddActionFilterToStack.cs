using System;
using System.Collections.Generic;

namespace AutoCCG
{
    public class AddActionFilterToStack : CardEffectModel
    {
        ActionType typeFilter;

        public override List<CardActionModel> CreateActions(BattlegroundsCardModel battlegroundsCard, Phase phase)
        {
            var effectActions = new List<CardActionModel>();

            Func<CardActionModel,bool> filter = (cardAction) => ActionFilterValidation(cardAction, battlegroundsCard);

            var addFilterAction = new CardActionModel(phase, battlegroundsCard, () => ActionStackModel.GetInstance().actionValidations.Add(filter), ActionType.Passive, ActionPriority.Immediate);
            effectActions.Add(addFilterAction);

            var removeFilterAction = new CardActionModel(phase, battlegroundsCard, () => ActionStackModel.GetInstance().actionValidations.Remove(filter), ActionType.Passive, ActionPriority.Immediate);
            effectActions.Add(removeFilterAction);

            return effectActions;
        }

        bool ActionFilterValidation(CardActionModel cardAction, BattlegroundsCardModel target)
        {
            return cardAction.source != target || cardAction.actionType != typeFilter;
        }
    }
}
