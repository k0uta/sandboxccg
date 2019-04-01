using System;

namespace AutoCCG
{
    public class CardActionModel
    {
        public Action action;

        public Phase phase;

        public BattlegroundsCardModel source;

        public ActionType actionType;

        public ActionPriority actionPriority;

        public CardActionModel(Phase phase, BattlegroundsCardModel source, Action action, ActionType actionType = ActionType.Active, ActionPriority actionPriority = ActionPriority.Default)
        {
            this.action = action;
            this.phase = phase;
            this.source = source;
            this.actionType = actionType;
            this.actionPriority = actionPriority;
        }

        public virtual void PerformAction()
        {
            action();
        }
    }

}