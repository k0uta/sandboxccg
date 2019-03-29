using AutoCCG;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("AutoCCG")]
    public class SendCardsToBattlegrounds : FsmStateAction
    {
        // Code that runs on entering the state.
        public override void OnEnter()
        {
            var boardController = Fsm.GameObject.GetComponent<BoardController>();
            var playerModel = boardController.player.GetComponentInChildren<PlayerModel>();
            var enemyModel = boardController.enemy.GetComponentInChildren<PlayerModel>();

            var playerBattlegrounds = boardController.player.GetComponentInChildren<BattlegroundsModel>();
            var enemyBattlegrounds = boardController.enemy.GetComponentInChildren<BattlegroundsModel>();

            playerModel.RpcSendCardsToBattlegrounds();
            enemyModel.RpcSendCardsToBattlegrounds();

            Finish();
        }
    }

}
