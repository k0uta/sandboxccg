using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AutoCCG
{
    public class ActionStackModel
    {
        public Phase currentPhase;

        public List<CardActionModel> actionQueue = new List<CardActionModel>();

        public List<Func<CardActionModel, bool>> actionValidations = new List<Func<CardActionModel, bool>>();

        public static ActionStackModel instance;

        public static ActionStackModel GetInstance()
        {
            if (instance == null)
            {
                instance = new ActionStackModel();
            }

            return instance;
        }

        public IEnumerator PerformPhaseActionQueue(Phase phase)
        {
            currentPhase = phase;

            while (true)
            {
                var actionsBatch = from cardAction in actionQueue
                                   where cardAction.phase == phase
                                   group cardAction by new { cardAction.actionPriority, cardAction.actionType } into actionCategory
                                   let cardActions = actionQueue.FindAll((x) => x.actionPriority == actionCategory.Key.actionPriority && x.actionType == actionCategory.Key.actionType && x.phase == phase)
                                   orderby actionCategory.Key.actionPriority, actionCategory.Key.actionType
                                   select cardActions;

                if (!actionsBatch.Any())
                {
                    break;
                }

                var actions = actionsBatch.First();

                // Purge invalid actions
                var invalidActions = actions.Where((cardAction) => !IsActionValid(cardAction));
                foreach (var invalidAction in invalidActions)
                {
                    actionQueue.Remove(invalidAction);
                }

                var validActions = actions.Except(invalidActions);

                foreach (var cardAction in validActions)
                {
                    yield return cardAction.PerformAction();

                    var reverseAction = cardAction.GetReverseAction();
                    if (reverseAction != null)
                    {
                        actionQueue.Add(reverseAction);
                    }

                    actionQueue.Remove(cardAction);
                }
            }
        }

        bool IsActionValid(CardActionModel cardAction)
        {
            foreach (var validation in actionValidations)
            {
                if (!validation(cardAction))
                {
                    return false;
                }
            }

            return true;
        }
    }
}