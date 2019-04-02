using System.Collections;
using System.Collections.Generic;

namespace AutoCCG
{
    public class CardActionModel
    {
        public List<ActionStepModel> steps = new List<ActionStepModel>();

        public Phase phase;

        public BattlegroundsCardModel source;

        public ActionType actionType;

        public ActionPriority actionPriority;

        public CardActionModel(Phase phase, BattlegroundsCardModel source, ActionType actionType = ActionType.Active, ActionPriority actionPriority = ActionPriority.Default)
        {
            this.phase = phase;
            this.source = source;
            this.actionType = actionType;
            this.actionPriority = actionPriority;
        }

        public IEnumerator PerformAction()
        {
            foreach (var step in steps)
            {
                yield return step.RunCoroutine();
            }
            yield return null;
        }

        public virtual CardActionModel GetReverseAction()
        {
            var undoSteps = steps.FindAll((step) => step.undo != null);

            if (undoSteps.Count <= 0)
            {
                return null;
            }

            CardActionModel reverseAction = new CardActionModel(Phase.CombatTurnEnd, source, ActionType.Passive, ActionPriority.Default);
            foreach (var step in undoSteps)
            {
                reverseAction.steps.Add(new ActionStepModel(step.undo));
            }

            return reverseAction;
        }
    }

}