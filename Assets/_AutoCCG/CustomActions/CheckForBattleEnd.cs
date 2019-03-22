using AutoCCG;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("AutoCCG")]
    public class CheckForBattleEnd : FsmStateAction
    {
        [Tooltip("Event to send if the Bool variable is True.")]
        public FsmEvent isTrue;

        [Tooltip("Event to send if the Bool variable is False.")]
        public FsmEvent isFalse;

        // Code that runs on entering the state.
        public override void OnEnter()
        {
            var boardController = Fsm.GameObject.GetComponent<BoardController>();
            var playerBattlegrounds = boardController.player.GetComponentInChildren<BattlegroundsModel>();
            var enemyBattlegrounds = boardController.enemy.GetComponentInChildren<BattlegroundsModel>();

            FsmEvent result;

            if (playerBattlegrounds.battlegroundsCards.Count <= 0 || enemyBattlegrounds.battlegroundsCards.Count <= 0)
            {
                result = isTrue;
            }
            else
            {
                result = isFalse;
            }

            Fsm.Event(result);

            Finish();
        }
    }

}
