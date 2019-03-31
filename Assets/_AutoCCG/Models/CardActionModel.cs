using System;

namespace AutoCCG
{
    public class CardActionModel
    {
        public Action action;

        public Phase phase;

        public CardActionModel(Phase phase, Action action)
        {
            this.action = action;
            this.phase = phase;
        }

        public virtual void PerformAction()
        {
            action();
        }
    }

}