using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AutoCCG
{
    public class ActionStackModel
    {
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

        public void PerformPhaseActionQueue(Phase phase)
        {
            // TODO: Add max count limit maybe?
            int count = 0;
            while (PerformNextActionBatch(phase))
            {
                count++;
            }
        }

        bool PerformNextActionBatch(Phase phase)
        {
            var actionBatch = from cardAction in actionQueue
                              where cardAction.phase == phase
                              group cardAction by new { cardAction.actionPriority, cardAction.actionType } into actionCategory
                              let actions = actionQueue.FindAll((x) => x.actionPriority == actionCategory.Key.actionPriority && x.actionType == actionCategory.Key.actionType)
                              orderby actionCategory.Key.actionPriority, actionCategory.Key.actionType
                              select actions.First();

            if (actionBatch.Count() <= 0)
            {
                return false;
            }

            // Purge invalid actions
            var invalidActions = actionBatch.Where((cardAction) => !IsActionValid(cardAction));
            foreach (var invalidAction in invalidActions)
            {
                actionQueue.Remove(invalidAction);
            }

            var validActions = actionBatch.Except(invalidActions);

            foreach (var cardAction in validActions)
            {
                cardAction.PerformAction();
                actionQueue.Remove(cardAction);
            }

            return true;
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