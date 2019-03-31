using System.Collections.Generic;

namespace AutoCCG
{
    public class ActionStackModel
    {
        public List<CardActionModel> actionQueue = new List<CardActionModel>();

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
            CardActionModel cardAction;

            while ((cardAction = actionQueue.Find((action) => action.phase == phase)) != null)
            {
                cardAction.PerformAction();
                actionQueue.Remove(cardAction);
            }
        }
    }
}