using AutoCCG;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("AutoCCG")]
    public class ClearBattlegrounds : FsmStateAction
    {
        // Code that runs on entering the state.
        public override void OnEnter()
        {
            var boardController = Fsm.GameObject.GetComponent<BoardController>();
            var playerBattlegrounds = boardController.player.GetComponentInChildren<BattlegroundsModel>();
            var enemyBattlegrounds = boardController.enemy.GetComponentInChildren<BattlegroundsModel>();

            var playerHandController = boardController.player.GetComponentInChildren<HandController>();
            var enemyHandController = boardController.enemy.GetComponentInChildren<HandController>();

            playerBattlegrounds.ClearBattlegrounds();
            enemyBattlegrounds.ClearBattlegrounds();

            playerHandController.EnableBattlegroundsQueueCards();
            enemyHandController.EnableBattlegroundsQueueCards();

            Finish();
        }
    }

}
